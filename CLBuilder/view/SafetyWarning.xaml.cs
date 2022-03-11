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
using System.Windows.Shapes;

namespace CLBuilder.view
{
    /// <summary>
    /// Interaction logic for SafetyWarning.xaml
    /// </summary>
    public partial class SafetyWarning : Window
    {
        public SafetyWarning()
        {
            InitializeComponent();
        }

        public bool CanContinue { get; set; }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CanContinue = false;
            this.Close();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            CanContinue = true;
            this.Close();
        }
    }
}
