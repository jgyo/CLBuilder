using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class DeleteChecklistCommand : BaseCommand
    {
        private readonly ChecklistControlViewModel viewModel;

        public DeleteChecklistCommand(ChecklistControlViewModel viewModel)
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
            viewModel.Checklists.Remove(viewModel.SelectedChecklist);
        }
    }
}
