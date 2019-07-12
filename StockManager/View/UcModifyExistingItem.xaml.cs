using StockManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UcModifyExistingItem.xaml
    /// </summary>
    public partial class UcModifyExistingItem : UcControl, INotifyPropertyChanged
    {


        public UcModifyExistingItem()
        {
            InitializeComponent();
            this.DataContext = this;

            EnableNext = false;
            EnableBack = EnableHome = true;
            TitleText = "Modify Existing Item";
        }

        public void SetCombo(ObservableCollection<MStockitem> LsServer)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                CmbItem.ItemsSource = LsServer;
                CmbItem.SelectedIndex = -1;
            });
        }



        private ObservableCollection<MStockitem> _ClStockitem;
        public ObservableCollection<MStockitem> ClStockitem
        {
            get { return _ClStockitem; }
            set
            {
                _ClStockitem = value;
                if (_ClStockitem.Any())
                {
                    SetCombo(_ClStockitem);
                }
                else
                {
                    this.Dispatcher.Invoke((Action)delegate
                    {
                        CmbItem.ItemsSource = null;
                    });
                }

                OnPropertyChangd(nameof(ClStockitem));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChangd(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        MStockitem _CurrentModel { get; set; }

        public MStockitem CurrentModel
        {
            get
            {

                return _CurrentModel;

            }
            set
            {
                _CurrentModel = value;
                OnPropertyChangd(nameof(CurrentModel));
            }
        }

        private void CmbItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbItem.SelectedItem != null)
            {
                CurrentModel = GetModel(((MStockitem)CmbItem.SelectedItem).ItemNo);

            }

        }

        public MStockitem GetModel(int ItemNo)
        {

            if (ClStockitem != null && ClStockitem.Any() && ClStockitem.Where(i => i.ItemNo.Equals(ItemNo)).Any())
            {
                return ClStockitem.Where(i => i.ItemNo.Equals(ItemNo)).FirstOrDefault();
            }

            return null;

        }

        public MStockitem GetModel(string ItemName)
        {

            if (ClStockitem != null && ClStockitem.Any() && ClStockitem.Where(i => i.ItemName.ToLower().Equals(ItemName.ToLower())).Any())
            {
                return ClStockitem.Where(i => i.ItemName.Equals(ItemName.ToLower())).FirstOrDefault();
            }

            return null;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Regex regex = new Regex("[^0-9.]+");
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CmbItem.SelectedIndex != -1 &&  CurrentModel != null)
            {
                CallCustomEvent(this, e, ((Button)sender).Tag.ToString());
            }
            else
            {
                MessageBox.Show("Please make a selection");
            }

        }
    }
}
