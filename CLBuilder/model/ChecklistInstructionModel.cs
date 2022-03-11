using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.model
{
    /// <summary>
    /// Class ChecklistInstructionModel.
    /// </summary>
    [DataContract]
    public class ChecklistInstructionModel
    {
        /// <summary>
        /// Gets or sets the instruction.
        /// </summary>
        /// <value>The instruction.</value>
        [DataMember]
        public string Instruction { get; set; }

        /// <summary>
        /// Gets or sets the checked response.
        /// </summary>
        /// <value>The checked response.</value>
        [DataMember]
        public string CheckedResponse { get; set; }

        /// <summary>
        /// Gets or sets the option.
        /// </summary>
        /// <value>The option.</value>
        [DataMember]
        public string Option { get; set; }
    }
}
