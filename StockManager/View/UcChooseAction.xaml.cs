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

namespace StockManager.View
{
    /// <summary>
    /// Interaction logic for UcChooseAction.xaml
    /// </summary>
    public partial class UcChooseAction : UcControl
    {
        public UcChooseAction()
        {
            InitializeComponent();
            EnableHome = EnableBack = EnableNext = false;
            TitleText = "Manage Stock";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CallCustomEvent(this, e, ((Button)sender).Tag.ToString());
        }
    }
}
