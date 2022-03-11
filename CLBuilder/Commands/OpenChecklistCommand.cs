using CLBuilder.view;
using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.Commands
{
    public class OpenChecklistCommand : BaseCommand
    {
        private readonly ChecklistEditorViewModel viewModel;

        public OpenChecklistCommand(ChecklistEditorViewModel mainViewModel)
        {
            this.viewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            var win = new ChecklistEditorView(viewModel);
            win.Show();
        }
    }
}
