using CLBuilder.Commands;
using CLBuilder.Extentions;
using CLBuilder.model;
using CLBuilder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLBuilder.viewModel
{
    public class ChecklistInstructionViewModel : ViewModelBase, ITestTextToSpeech
    {
        private string itemName;
        private string checkedResponse;
        private string pption;

        public ICommand TestTextToSpeechCommand { get; }

        public ChecklistInstructionViewModel(ChecklistInstructionModel item)
        {
            Instruction = item.Instruction;
            CheckedResponse = item.CheckedResponse;
            Option = item.Option;
            TestTextToSpeechCommand = new TestTextToSpeechCommand(this);
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

        bool ITestTextToSpeech.CanExecute(object prompt)
        {
            if(prompt is string p)
            {
                return !p.IsNullOrEmpty();
            }

            return false;
        }

        void ITestTextToSpeech.TestTTS(object prompt)
        {
            var p = prompt as string;
            TextToSpeechService.Speak(p);
        }
    }
}
