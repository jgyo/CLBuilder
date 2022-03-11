using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.model
{
    /// <summary>
    /// Class ChecklistModel.
    /// </summary>
    [DataContract]
    public class ChecklistModel
    {
        private string name;
        private string title;
        private string aircraftShortName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistModel"/> class.
        /// </summary>
        /// <param name="aircraftName">Name of the aircraft.</param>
        public ChecklistModel(string aircraftName = null)
        {
            ChecklistItems = new List<ChecklistInstructionModel>();
            this.aircraftShortName = aircraftName;

        }

        /// <summary>
        /// Gets or sets the short name of the aircraft.
        /// </summary>
        /// <value>The short name of the aircraft.</value>
        [DataMember]
        public string AircraftShortName { get => aircraftShortName; set => aircraftShortName = value; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
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

        /// <summary>
        /// Gets or sets the next checklist title.
        /// </summary>
        /// <value>The next checklist title.</value>
        [DataMember]
        public string NextChecklistTitle { get; set; }

        /// <summary>
        /// Gets the checklist items.
        /// </summary>
        /// <value>The checklist items.</value>
        [DataMember]

        public List<ChecklistInstructionModel> ChecklistItems { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
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

        /// <summary>
        /// Gets the checklist text.
        /// </summary>
        /// <value>The checklist text.</value>
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
