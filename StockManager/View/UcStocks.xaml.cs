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
using StockManager.ViewModel;
using Microsoft.Windows.Controls.Primitives;

namespace StockManager.View
{
    /// <summary>
    /// Interaction logic for UcStocks.xaml
    /// </summary>
    public partial class UcStocks : UcControl, INotifyPropertyChanged
    {
        public UcStocks()
        {
            InitializeComponent();

            EnableBack = EnableNext = false;
            EnableHome = true;
            TitleText = "Manage stock";

            this.DataContext = this;

        }

        private ObservableCollection<MStockitem> _ClStockitem;
        public ObservableCollection<MStockitem> ClStockitem
        {
            get { return _ClStockitem; }
            set
            {
                //this.Dispatcher.Invoke((Action)delegate
                //{
                    
               // });
                _ClStockitem = value;
                _ClStockitem = new ObservableCollection<MStockitem>(_ClStockitem.OrderBy(i => i.ItemNo));

                ClItemList = new ObservableCollection<string>(_ClStockitem.OrderBy(i => i.ItemNo).Select(i => i.ItemName));
                OnPropertyChangd(nameof(ClStockitem));
                //StatusList sd = new StatusList();
                //OnPropertyChangd(nameof(StatusList));

                //Keyboard.ClearFocus();

            }
        }

        private ObservableCollection<string> _ClItemList;
        public ObservableCollection<string> ClItemList
        {
            get { return _ClItemList; }
            set
            {
                _ClItemList = value;
                OnPropertyChangd(nameof(ClItemList));
                StatusList sd = new StatusList();
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
            Keyboard.ClearFocus();
            if (ClStockitem != null && ClStockitem.Where(i => i.IsChanged).Any())
            {
                if (ClStockitem.Where(i => i.IsChanged && string.IsNullOrEmpty(i.ItemName)).Any())
                {
                    MessageBox.Show("Item name shouldn't be empty for the changed rows ");
                    return;
                }
                CallCustomEvent(sender, e, "UpdateStock", ClStockitem);
            }
            else
            {
                MessageBox.Show("Nothing to update");
            }

        }


        private void McDataGrid_CellEditEnding(object sender, SelectionChangedEventArgs e)
        {
            if (McDataGrid.SelectedItem != null)
            {
                ((MStockitem)McDataGrid.SelectedItem).IsChanged = true;
                OnPropertyChangd(nameof(ClStockitem));
            }
        }



        private void McDataGrid_CellEditEnding_1(object sender, Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            if (McDataGrid.SelectedItem != null)
            {
                ((MStockitem)McDataGrid.SelectedItem).IsChanged = true;
                OnPropertyChangd(nameof(ClStockitem));


            }
        }

        private void AddItemButtonClick(object sender, RoutedEventArgs e)
        {
            //var CurrentRowCount = McDataGrid.Items.Count;

            if (ClStockitem != null && ClStockitem.Any() && ClStockitem.Where(i => string.IsNullOrEmpty(i.ItemName)).Any())
            {
                MessageBox.Show("Please insert valid data in the last column then procced to add new ones");
                return;
            }
            else if (ClStockitem != null && ClStockitem.Count == 0)
            {
                MessageBox.Show("Please insert valid data in first columns then procced to add new ones");
                return;
            }
            else
            {
                Keyboard.ClearFocus();
            }
            //if(CurrentRowCount.Equals(McDataGrid.Items.Count))
            //{
            //    MessageBox.Show("Please insert valid data in the last column then procced to add new ones");
            //    return;
            //}
        }


        private void Datagrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
          

        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
       where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }




        private void McDataGrid_BeginningEdit(object sender, Microsoft.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            
        }

        private void CmbBox_Selected(object sender, RoutedEventArgs e)
        {

            if (McDataGrid.SelectedItem != null)
            {
                ((MStockitem)McDataGrid.SelectedItem).IsChanged = true;
                OnPropertyChangd(nameof(ClStockitem));
            }

        }

        private void CmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (McDataGrid.SelectedItem != null)
            {
                ((MStockitem)McDataGrid.SelectedItem).IsChanged = true;
                OnPropertyChangd(nameof(ClStockitem));


            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            ((DatePicker)sender).SelectedDate = DateTime.Now;
        }

        private void CmbBox_Loaded(object sender, RoutedEventArgs e)
        {
            StatusList SL = new StatusList();
            ((ComboBox)sender).ItemsSource = SL;
            if (((ComboBox)sender).ItemsSource != null)
            {
                ((ComboBox)sender).SelectedIndex = 0;

            }
        }
    }



    public class StatusList : Dictionary<int, string> , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChangd(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public StatusList()
        {
            var ClItem = Presenter.GetListOfItems();

            if (ClItem.Any())
            {
                foreach (var item in ClItem)
                {
                    this.Add(item.ItemNo, item.ItemName);
                }
            }

            OnPropertyChangd(nameof(StatusList));

        }
    }
}
