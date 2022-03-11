using CLBuilder.viewModel;
using System.IO;
using System.Linq;

namespace CLBuilder.Commands
{
    public class BuildChecklistScriptsCommand : BaseCommand
    {
        private readonly MainViewModel mainViewModel;

        public BuildChecklistScriptsCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            this.mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;
            if (this.mainViewModel.ChecklistControlViewModel != null)
            {
                this.mainViewModel.ChecklistControlViewModel.PropertyChanged += ChecklistControlViewModel_PropertyChanged;
            }
        }

        public override bool CanExecute(object parameter)
        {
            try
            {
                var root = mainViewModel.ChecklistControlViewModel.ScriptsFolder;
                var dir = mainViewModel.ChecklistControlViewModel.AircraftShortName;
                var invalidPathChars = Path.GetInvalidPathChars();

                if (string.IsNullOrEmpty(root) ||
                    root.Any(x => invalidPathChars.Contains(x)) ||
                    !Directory.Exists(root) ||
                    string.IsNullOrEmpty(dir) ||
                    dir.Any(x => invalidPathChars.Contains(x)))
                {
                    return false;
                }

                foreach(var cl in mainViewModel.ChecklistControlViewModel.Checklists)
                {
                    var name = cl.Name;
                    if (name.Any(m => invalidPathChars.Contains(m)))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void Execute(object parameter)
        {
            if (!mainViewModel.CanContinue())
            {
                return;
            }

            var root = mainViewModel.ChecklistControlViewModel.ScriptsFolder;
            var icao = mainViewModel.ChecklistControlViewModel.AircraftShortName;
            root = Path.Combine(root, icao);

            var controlModel = mainViewModel.ChecklistControlViewModel.Store();

            if (Directory.Exists(root))
            {
                // Delete directory and its contents
                Directory.Delete(root, true);
            }

            // Create a new directory for the files
            Directory.CreateDirectory(root);

            // Create the control filename and save the control text
            var filename = $"{icao}_checklist_control.txt";
            var text = controlModel.ControlText;
            File.WriteAllText(Path.Combine(root, filename), text);

            var i = 1;
            // Write each of the checklist text files in the same way
            foreach (var cl in controlModel.Checklists)
            {
                filename = $"{i++}_{cl.Name}_cl.txt";
                text = cl.ChecklistText;
                File.WriteAllText(Path.Combine(root, filename), text);
            }
        }

        private void ChecklistControlViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ChecklistControlViewModel")
            {
                this.mainViewModel.ChecklistControlViewModel.PropertyChanged += ChecklistControlViewModel_PropertyChanged;
            }

            OnCanExecuteChanged();
        }
    }
}