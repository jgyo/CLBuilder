using CLBuilder.Commands;
using CLBuilder.Extentions;
using CLBuilder.model;
using CLBuilder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLBuilder.viewModel
{
    public class ChecklistEditorViewModel : ViewModelBase, ITestTextToSpeech
    {
        private string name;
        private string title;
        private ChecklistInstructionViewModel selectedInstruction;
        private int selectedIndex;

        public ICommand InsertInstructionCommand { get; }
        public ICommand AddInstructionCommand { get; }
        public ICommand DeleteInstructionCommand { get; }
        public ICommand MoveInstructionUpCommand { get; }
        public ICommand MoveInstructionDownCommand { get; }
        public ICommand OpenChecklistCommand { get; }
        public ICommand TestTextToSpeechCommand { get; }

        public ChecklistEditorViewModel(ChecklistModel model) : this()
        {
            Name = model.Name;
            Title = model.Title;

            foreach (var item in model.ChecklistItems)
            {
                var vm = new ChecklistInstructionViewModel(item);
                Instructions.Add(vm);
            }
        }

        public ChecklistEditorViewModel()
        {
            Instructions = new ObservableCollection<ChecklistInstructionViewModel>();
            InsertInstructionCommand = new InsertInstructionCommand(this);
            AddInstructionCommand = new AddInstructionCommand(this);
            DeleteInstructionCommand = new DeleteInstructionCommand(this);
            MoveInstructionDownCommand = new MoveInstructionDownCommand(this);
            MoveInstructionUpCommand = new MoveInstructionUpCommand(this);
            OpenChecklistCommand = new OpenChecklistCommand(this);
            TestTextToSpeechCommand = new TestTextToSpeechCommand(this);
        }

        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value); }
        }

        public ObservableCollection<ChecklistInstructionViewModel> Instructions { get; private set; }
        public bool IsItemSelected
        {
            get => this.SelectedIndex >= 0;
        }
        public ChecklistInstructionViewModel SelectedInstruction
        {
            get => selectedInstruction;

            set => SetProperty(ref selectedInstruction, value);
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set => SetProperty(ref selectedIndex, value);
        }

        bool ITestTextToSpeech.CanExecute(object parameter)
        {
            return !Title.IsNullOrEmpty();
        }

        void ITestTextToSpeech.TestTTS(object parameter)
        {
            TextToSpeechService.Speak(Title);
        }
    }
}
