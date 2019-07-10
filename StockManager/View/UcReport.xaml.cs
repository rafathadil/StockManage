using CrystalDecisions.CrystalReports.Engine;
using StockManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for UcReport.xaml
    /// </summary>
    public partial class UcReport : UcControl
    {
        string ReportPath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\reports\ItemReport.rpt";
        public UcReport()
        {
            InitializeComponent();
            EnableNext = false;
            TitleText = "View Report";
        }


        public void LoadReport(DataTable Dt,MreportModel  ReportModel)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                try
                {
                    ReportDocument report = new ReportDocument();

                    if (File.Exists(ReportPath))
                    {
                        report.Load(ReportPath);

                        report.SetDataSource(Dt);
                        if (ReportModel.IsUseDateFilter)
                        {
                            report.SetParameterValue("Header", "Report for the item ' "+ ReportModel.ItemName+" '"+"Dated from :" + ReportModel.DateFrom +" to " + ReportModel.DateTO);
                        }
                        else
                        {
                            report.SetParameterValue("Header", "Report for the item ' " + ReportModel.ItemName + " '");
                        }
                        crystalReportsViewer1.ViewerCore.ReportSource = report;
                    }
                    else
                    {
                        MessageBox.Show("Report File Not found on path :" + ReportPath);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);

                }
            });
        }
    }
}
