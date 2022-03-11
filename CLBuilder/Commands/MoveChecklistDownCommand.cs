using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class MoveChecklistDownCommand : BaseCommand
    {

        private readonly ChecklistControlViewModel viewModel;

        public MoveChecklistDownCommand(ChecklistControlViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            if (!viewModel.IsItemSelected)
                return false;

            return viewModel.SelectedIndex < viewModel.Checklists.Count - 1;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            var oldIndex = viewModel.SelectedIndex;

            viewModel.Checklists.Move(oldIndex, oldIndex + 1);
        }
    }
}
