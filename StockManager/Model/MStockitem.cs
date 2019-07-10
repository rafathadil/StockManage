using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Model
{
    public class MStockitem
    {
        public MStockitem()
        {
            Date = DateTime.Now;
        }
        public int ItemNo { get; set; }

        public object ItemNoForStock { get; set; }

        public int ItemDetailIDForStock { get; set; }
        public string ItemName { get; set; }

        public double Price { get; set; }

        public string Unit { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }

        public bool IsChanged { get; set; }

       // public Dictionary<string,string> LsMStockitem { get; set; }

    }
}
