using StockManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for UcItemMaster.xaml
    /// </summary>
    public partial class UcItemMaster : UcControl, INotifyPropertyChanged
    {
        public UcItemMaster()
        {
            InitializeComponent();
            EnableBack = EnableNext = false;
            EnableHome = true;
            TitleText = "ITEM MASTER";
            this.DataContext = this;

        }


        

        private ObservableCollection<MStockitem> _ClStockitem;
        public ObservableCollection<MStockitem> ClStockitem
        {
            get { return _ClStockitem; }
            set
            {
                _ClStockitem = value;
                _ClStockitem = new ObservableCollection<MStockitem>(_ClStockitem.OrderBy(i => i.ItemNo));
                OnPropertyChangd(nameof(ClStockitem));
            }
        }







        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChangd(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum EAction
        {
            AddNewItem, UpdateExistingitem, DeleteExstingItems
        }

        public EAction ElastAction { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

            CallCustomEvent(sender, e, "ActionFromItemMaster", ((Button)sender).Tag.ToString());
        }
    }
}
