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
    /// Interaction logic for UcCreateReport.xaml
    /// </summary>
    public partial class UcCreateReport : UcControl, INotifyPropertyChanged
    {
        public UcCreateReport()
        {
            InitializeComponent();

            EnableBack = false;
            TitleText = "Reports";
            this.DataContext = this;

        }

        private MreportModel _MreportModel;
        public MreportModel MreportModel
        {
            get { return _MreportModel; }
            set
            {
                _MreportModel = value;
                OnPropertyChangd(nameof(MreportModel));
                this.Dispatcher.Invoke((Action)delegate
                {
                    CmbBox.ItemsSource = new StatusList();
                });
            }
        }

        public bool ValidateModel()
        {
            if (MreportModel.ItemNo == null)
            {
                return false;
            }
            else if (MreportModel.IsUseDateFilter)
            {
                if (MreportModel.DateFrom.Equals(DateTime.MinValue) || MreportModel.DateFrom.Equals(DateTime.MaxValue) ||
                     MreportModel.DateTO.Equals(DateTime.MinValue) || MreportModel.DateTO.Equals(DateTime.MaxValue))
                {
                    return false;
                }
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChangd(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void CmbBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
