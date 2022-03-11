using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.model
{
    [DataContract]
    public class ChecklistInstructionModel
    {
        [DataMember]
        public string Instruction { get; set; }

        [DataMember]
        public string CheckedResponse { get; set; }

        [DataMember]
        public string Option { get; set; }

        public ChecklistInstructionModel()
        {
            Instruction = "<ItemName>";
            CheckedResponse = "checked";
            Option = "0";
        }
    }
}
