using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.Commands
{
    public class TestTextToSpeechCommand : BaseCommand
    {
        private readonly ITestTextToSpeech model;

        public TestTextToSpeechCommand(ITestTextToSpeech model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return model.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            model.TestTTS(parameter);
        }
    }

    public interface ITestTextToSpeech : INotifyPropertyChanged
    {
        bool CanExecute(object parmeter = null);
        void TestTTS(object parmeter = null);
    }
}
