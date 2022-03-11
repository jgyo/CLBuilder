using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.Commands
{
    public class NewChecklistControlCommand : BaseCommand
    {
        private readonly MainViewModel mainViewModel;

        public NewChecklistControlCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
                if (!mainViewModel.CanContinue())
                {
                    return;
                }

                mainViewModel.FullFilename = string.Empty;
                mainViewModel.ChecklistControlViewModel = new ChecklistControlViewModel();

        }
    }
}
