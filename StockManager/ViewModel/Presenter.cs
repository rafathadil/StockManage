using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.ViewModel
{
    public static class Presenter
    {


        public static  ObservableCollection<Model.MStockitem> GetListOfItems()
        {

            return MainViewModel.CurrentInstance.GetItemList();
        }

       
    }
}
