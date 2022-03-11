using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.Commands
{
    public class AddChecklistCommand : BaseCommand
    {
        private readonly ChecklistControlViewModel viewModel;

        public AddChecklistCommand(ChecklistControlViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            viewModel.Checklists.Add(new ChecklistEditorViewModel());
        }
    }
}
