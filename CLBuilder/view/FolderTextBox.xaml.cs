using Ookii.Dialogs.Wpf;
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
using CLBuilder.Extentions;

namespace CLBuilder.view
{
    /// <summary>
    /// Interaction logic for FolderTextBox.xaml
    /// </summary>
    public partial class FolderTextBox : UserControl
    {
        public FolderTextBox()
        {
            this.Text = "FolderTextBox";
            // InitializeComponent();
            this.LoadViewFromUri("/CLBuilder;component/view/FolderTextBox.xaml");
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FolderTextBox), new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var browser = new VistaFolderBrowserDialog()
            {
                Description="Browse to a folder to save your scripts.",
                Multiselect=false,
                SelectedPath=Text,
                ShowNewFolderButton=true,
                UseDescriptionForTitle=true
            };
            browser.ShowDialog();
            Text = browser.SelectedPath;
        }
    }
}
