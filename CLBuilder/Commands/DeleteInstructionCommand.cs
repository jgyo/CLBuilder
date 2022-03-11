using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class DeleteInstructionCommand : BaseCommand
    {
        private readonly ChecklistEditorViewModel viewModel;

        public DeleteInstructionCommand(ChecklistEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
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
            viewModel.Instructions.Remove(viewModel.SelectedInstruction);
        }
    }
}
