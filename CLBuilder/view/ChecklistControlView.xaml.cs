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
    /// Interaction logic for ChecklistControlView.xaml
    /// </summary>
    public partial class ChecklistControlView : UserControl
    {
        public ChecklistControlView()
        {
            //InitializeComponent();

            this.LoadViewFromUri("ACDB;component/view/checklistcontrolview.xaml");
        }
    }
}
