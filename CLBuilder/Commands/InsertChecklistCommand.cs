using CLBuilder.viewModel;
using System;
using System.ComponentModel;

namespace CLBuilder.Commands
{
    public class InsertChecklistCommand : BaseCommand
    {
        private readonly ChecklistControlViewModel viewModel;

        public InsertChecklistCommand(ChecklistControlViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return viewModel.IsItemSelected;
        }

        public override void Execute(object parameter)
        {
            viewModel.Checklists.Insert(viewModel.SelectedIndex, new ChecklistEditorViewModel());
        }
    }
}
