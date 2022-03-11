using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.viewModel
{
    class ChecklistControlVM : ChecklistControlViewModel
    {
        public ChecklistControlVM()
        {
            this.AircraftShortName = "K100";

            this.Checklists.Add(new ChecklistEditorViewModel());
            this.Checklists.Add(new ChecklistEditorViewModel());
            this.Checklists.Add(new ChecklistEditorViewModel());
        }
    }
}
