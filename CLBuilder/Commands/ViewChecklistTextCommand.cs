using CLBuilder.model;
using CLBuilder.view;
using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class ViewChecklistTextCommand : BaseCommand
    {
        private readonly ChecklistControlViewModel checklistControlViewModel;

        public ViewChecklistTextCommand(ChecklistControlViewModel viewModel)
        {
            checklistControlViewModel = viewModel;
            checklistControlViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return checklistControlViewModel.IsItemSelected;
        }

        public override void Execute(object parameter)
        {
            ChecklistModel checkListModel;

            var checkListControlModel = checklistControlViewModel.Store();
            checkListModel = checkListControlModel.Checklists[checklistControlViewModel.SelectedIndex];

            var win = new TextView
            {
                ItemName = checkListModel.Name,
                Text = checkListModel.ChecklistText,
                Description = $"The following is the text that would be generated for the checklist named \"{checkListModel.Name}.\"",
                Title = $"Text View of {checkListModel.Name}"
            };
            win.ShowDialog();
        }
    }
}