using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.model
{
    [DataContract]
    public class ChecklistModel
    {
        private string name;
        private string title;
        private string aircraftShortName;

        public ChecklistModel(string aircraftName = "K100")
        {
            ChecklistItems = new List<ChecklistInstructionModel>();
            this.aircraftShortName = aircraftName;

        }

        public void SetAircraftShortName(string name)
        {
            aircraftShortName = name;
        }

        [DataMember]
        public string AircraftShortName { get => aircraftShortName; set => aircraftShortName = value; }

        [DataMember]
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(title))
                {
                    return "<checklist title>";
                }

                return title;
            }

            set => title = value;
        }

        [DataMember]
        public string NextChecklistTitle { get; set; }

        [DataMember]

        public List<ChecklistInstructionModel> ChecklistItems { get; private set; }

        [DataMember]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return "<checklist name>";
                }

                return name;
            }

            set => name = value;
        }

        public string ChecklistText
        {
            get
            {
                var text = new StringBuilder();

                // Line 1
                text.AppendLine($"1 (>L:{aircraftShortName}Checklist)");

                // Line 2
                text.AppendLine($"(@{aircraftShortName}CLFOSPEAK:{Title}) (WAIT:3000)");

                // Lines for each item

                var index = 1;
                foreach(var item in ChecklistItems)
                {
                    // First item line
                    text.AppendLine($"{index} (>L:clphase)");

                    // Item to check
                    text.AppendLine($"[](@{aircraftShortName}CLFOSPEAK:{item.Instruction}");

                    // Response
                    text.AppendLine($"[{item.Option}](@{aircraftShortName}CLFOSPEAK:{item.CheckedResponse})");

                    index++;
                }

                // Reset the checklist phase
                text.AppendLine("0 (>L:clphase)");

                // Last line
                text.Append($"(WAIT:1500) (@{aircraftShortName}CLFOSPEAK:{Title} complete");

                if(string.IsNullOrEmpty(NextChecklistTitle))
                {
                    text.AppendLine(")");
                }
                else
                {
                    text.AppendLine($", {NextChecklistTitle} is next");
                }

                return text.ToString();
            }
        }
    }
}
