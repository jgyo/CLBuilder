using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class InsertInstructionCommand : BaseCommand
    {
        private readonly ChecklistEditorViewModel viewModel;

        public InsertInstructionCommand(ChecklistEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return viewModel.IsItemSelected;
        }

        public override void Execute(object parameter)
        {
            viewModel.Instructions.Insert(viewModel.SelectedIndex, new ChecklistInstructionViewModel());
        }
    }
}
