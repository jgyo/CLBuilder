using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.viewModel
{
    class ChecklistVM : ChecklistEditorViewModel
    {
        public ChecklistVM()
        {
            this.Name = "preflight";
            this.Title = "preflight inspection";

            this.Instructions.Add(new ChecklistInstructionViewModel()
            {
                Instruction = "master switch on",
                CheckedResponse = "switch is on",
                Option="0"
            });
            this.Instructions.Add(new ChecklistInstructionViewModel()
            {
                Instruction="avionics switch on"
            });
            this.Instructions.Add(new ChecklistInstructionViewModel()
            {
                Instruction = "avionics fan on"
            });
            this.Instructions.Add(new ChecklistInstructionViewModel()
            {
                Instruction = "fuel on board"
            });
        }
    }
}
