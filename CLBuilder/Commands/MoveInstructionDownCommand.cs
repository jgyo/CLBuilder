using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class MoveInstructionDownCommand : BaseCommand
    {

        private readonly ChecklistEditorViewModel viewModel;

        public MoveInstructionDownCommand(ChecklistEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            if (!viewModel.IsItemSelected)
                return false;

            return viewModel.SelectedIndex < viewModel.Instructions.Count - 1;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            var oldIndex = viewModel.SelectedIndex;

            viewModel.Instructions.Move(oldIndex, oldIndex + 1);
        }
    }
}
