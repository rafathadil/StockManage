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
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using StockManager.Model;

namespace StockManager.View
{
    /// <summary>
    /// Interaction logic for UcUpdateItem.xaml
    /// </summary>
    public partial class UcUpdateItem : UcControl
    {
        public UcUpdateItem()
        {
            InitializeComponent();

            EnableBack = true;
            EnableNext = false;
            EnableHome = true;
            TitleText = "Add new Item";
        }
        Model.MStockitem MStockitem { get; set; }

        public int LastItemId { get; set; }


        public ObservableCollection<MStockitem> ClStockitem { get; set; }

        public void resetPage()
        {

            // VDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString();

            VPrice.Text = "0";
            VUnit.Text = string.Empty;
            VItemName.Text = string.Empty;
        }


        public bool ValidateItemName(string ItemName)
        {

            if (ClStockitem.Where(i => i.ItemName.ToLower().Equals(ItemName.Trim().ToLower())).Any()) return false;

            return true;

        }
        public Model.MStockitem CreateNewModel()
        {
            MStockitem = new Model.MStockitem();

            this.Dispatcher.Invoke((Action)delegate
            {
                MStockitem.ItemName = VItemName.Text;
                MStockitem.ItemNo = LastItemId++;
                if (!string.IsNullOrEmpty(VPrice.Text))
                    MStockitem.Price = Convert.ToDouble(VPrice.Text);
                else
                    MStockitem.Price = 0;
                if (!string.IsNullOrEmpty(VUnit.Text))
                    MStockitem.Unit = VUnit.Text;
                else
                    MStockitem.Unit = string.Empty;
            });
            return MStockitem;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(VItemName.Text))
            {
                if (ValidateItemName(VItemName.Text))
                    CallCustomEvent(this, e, ((Button)sender).Tag.ToString());
                else
                    MessageBox.Show("item already exists");
            }
            else
            {
                MessageBox.Show("Please Enter Item name");
            }
        }
    }
}
