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

        /// <summary>Gets or sets a value indicating whether the current operation can continue.</summary>
        /// <value>
        ///   <c>true</c> if the current operation can continue; otherwise, <c>false</c>.</value>
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
