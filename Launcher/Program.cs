using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using StockManager.ViewModel;

namespace Launcher
{
    class Program
    {        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            #region Run application
            MainViewModel ViewModel = new MainViewModel(connection);
            ViewModel.StartApplication();
            #endregion
        }
    }
}
