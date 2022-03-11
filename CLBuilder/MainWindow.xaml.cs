using CLBuilder.model;
using CLBuilder.Properties;
using CLBuilder.view;
using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainViewModel;
            mainViewModel.ChecklistControlViewModel = new ChecklistControlViewModel();
            this.Closing += MainWindow_Closing;
            mainViewModel.SaveWarningEnabled = Settings.Default.EnableSafetyWarning;
        }

        /// <summary>
        /// Shows a warning (if enabled) before closing and offers an opportunity to cancel the operation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.
        /// </param>
        /// <seealso cref="CLBuilder.views.SafetyWarning">Safety warning window</seealso>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(DataContext is MainViewModel mainViewModel))
            {
                return;
            }

            if (mainViewModel.SaveWarningEnabled)
            {
                var win = new SafetyWarning();
                win.DataContext = mainViewModel;
                win.ShowDialog();
                if(win.CanContinue == false)
                    e.Cancel = true;
            }

            Settings.Default.EnableSafetyWarning = mainViewModel.SaveWarningEnabled;
            Settings.Default.Save();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
