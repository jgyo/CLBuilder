using CLBuilder.Commands;
using CLBuilder.view;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CLBuilder.viewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ChecklistControlViewModel checkListControlViewModel;
        private string filename;
        private string fullFilename;
        private bool saveWarningEnabled = true;

        public MainViewModel()
        {
            NewChecklistCommand = new NewChecklistControlCommand(this);
            OpenChecklistControlCommand = new OpenChecklistControlCommand(this);
            SaveChecklistCommand = new SaveChecklistControlCommand(this);
            SaveChecklistAsCommand = new SaveChecklistControlAsCommand(this);
            BuildChecklistScriptsCommand = new BuildChecklistScriptsCommand(this);
        }

        public ChecklistControlViewModel ChecklistControlViewModel
        {
            get => checkListControlViewModel;
            set => SetProperty(ref checkListControlViewModel, value);
        }

        public string Filename
        {
            get => filename;
            private set => SetProperty(ref filename, value);
        }

        public string FullFilename
        {
            get => fullFilename;
            set
            {
                SetProperty(ref fullFilename, value);
                Filename = Path.GetFileName(FullFilename);
            }
        }

        public ICommand BuildChecklistScriptsCommand { get; }
        public ICommand NewChecklistCommand { get; }
        public ICommand OpenChecklistControlCommand { get; }
        public ICommand SaveChecklistAsCommand { get; }
        public ICommand SaveChecklistCommand { get; }

        public bool SaveWarningEnabled
        {
            get => saveWarningEnabled;
            set => SetProperty(ref saveWarningEnabled, value);
        }

        public bool CanContinue()
        {
            if (!SaveWarningEnabled)
            {
                return true;
            }

            var win = new SafetyWarning
            {
                DataContext = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            _ = win.ShowDialog();

            return win.CanContinue;
        }
    }
}