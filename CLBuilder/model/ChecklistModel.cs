using CLBuilder.Extentions;
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

        public string WebText(int checklistNum, bool lastChecklist)
        {
            var checklistItems = new StringBuilder();

            // {0} = instructionNum
            // {1} = itemInstruction
            var itemText = @"                <tr>
                    <td style = ""width: 10 %; "" />
      
                          <td style = ""width: 10 %; "">
            
                                   <input id = ""item_{0}"" type = ""checkbox"" style = ""width: 20px; height: 20px; "" onclick = ""itemCheckClick('item_{0}')"">
                         
                                             </td>
                         
                                             <td style = ""width: 80 %; "">
                               
                                                       <label for= ""item_{0}"" id = ""itemLabel_{0}"" style = ""font - size: 24px; ""> {1} </label>
                                
                                                    </td>
                                
                                                </tr>";

            var i = 1;
            foreach (var item in ChecklistItems)
            {
                checklistItems.AppendLine(string.Format(itemText, i++, item.Instruction.Capitalize()));
            }

            var nextChecklistNum = checklistNum+1;
            if (lastChecklist)
            {
                nextChecklistNum = 1;
            }

            // {0} = checklistNum
            // {1} = nextChecklist
            // {2} = Title
            // {3} = AircraftShortName
            // {4} = Name
            // {5} = checklistItems.String()
            // {6} = "{"
            // {7} = ")"
            var page =@"<!DOCTYPE html>
<html>
<head>
  <link rel=""stylesheet"" href=""iconbuttons.css"">
  <title>{2}</title>
</head>
<body>

<script src=""clcontrol.js""></script>

<script>
    currentChecklist = {0};
    
    function bStartClClick() {6}
        Reset();
        sendScript('(CHECKLIST:{3}\\{3}_{0}_{4}_cl.txt)');
    {7}

    function bNextClClick() {6}
        sendScript('{1} (>L:{3}Checklist) 0 (>L:clphase)');
    {7}

</script>
<!-- Just a couple of un-styled buttons -->
<table>
    <tr>
        <th>
            {2}
        </th>
    </tr>
    <tr>
        <td style=""text-align: center; vertical-align: middle;""><input type=""button"" id=""bStartCl"" style=""font-size: 16px;"" class=""btn btn-2"" value=""Start checklist"" onclick=""bStartClClick()""></td>
    </tr>
    <tr>
        <td style=""text-align: center; vertical-align: middle;""><input type=""button"" id=""bStopCl"" style=""font-size: 16px;"" class=""btn btn-3"" value=""Stop Checklist"" onclick=""bStopClClick()""></td>
    </tr>
    <tr>
        <td colspan=""2"">
            <table style=""width:100%;"">
{5}
            </table>
        </td>
    </tr>
    <tr>
        <td style=""text-align: center; vertical-align: middle;""><input type=""button"" style=""font-size: 16px;"" class=""btn btn-1"" value=""CHECKED"" onclick=""sendEvent(1, '(>K:AAO_CL_CHECKED)')""></td>
    </tr>
    <tr>
        <td style=""text-align: center; vertical-align: middle;""><input type=""button"" id=""bNextCl"" style=""font-size: 16px;"" class=""btn btn-4"" value=""Next: Pushback"" onclick=""bNextClClick()""></td>
    </tr>
</table>
</body>
</html>";
            return string.Format(page, checklistNum, lastChecklist, Title.ToUpper(), AircraftShortName, Name, checklistItems.ToString(), "{", "}");
        }
    }
}
