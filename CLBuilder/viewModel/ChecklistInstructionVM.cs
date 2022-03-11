using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.viewModel
{
    class ChecklistInstructionVM : ChecklistInstructionViewModel
    {
        public ChecklistInstructionVM()
        {
            this.CheckedResponse = "Okey Dokey";
            this.Instruction = "Scream like a chicken";
            this.Option = "A burger and fries";
        }
    }
}
