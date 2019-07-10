using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  StockManager.Model
{
    public class MreportModel
    {
        public MreportModel()
        {
            DateFrom = DateTO = DateTime.Now;
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTO { get; set; }

        public object ItemNo { get; set; }

        public string ItemName { get; set; }

        public bool IsUseDateFilter { get; set; }
    }
}
