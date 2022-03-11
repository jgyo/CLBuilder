using CLBuilder.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.viewModel
{
    public class ChecklistInstructionViewModel : ViewModelBase
    {
        private string itemName;
        private string checkedResponse;
        private string pption;

        public ChecklistInstructionViewModel(ChecklistInstructionModel item)
        {
            Instruction = item.Instruction;
            CheckedResponse = item.CheckedResponse;
            Option = item.Option;
        }

        public ChecklistInstructionViewModel()
        {
            Instruction = "";
            CheckedResponse = "Checked";
            Option = "0";
        }

        public string Instruction
        {
            get => itemName;
            set { SetProperty(ref itemName, value); }
        }

        public string CheckedResponse
        {
            get => checkedResponse;
            set { SetProperty(ref checkedResponse, value); }
        }

        public string Option
        {
            get => pption;
            set { SetProperty(ref pption, value); }
        }
    }
}
