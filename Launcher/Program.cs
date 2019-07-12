using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using StockManager.ViewModel;

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
            #region Run application WOR
             // MainViewModel ViewModel = new MainViewModel(connection);
             //  ViewModel.StartApplication();
            #endregion
           
            #region Run application WR

            try
            {
                string[] Arg = new string[] { connection};
                Assembly a = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "StockManager.dll");
                Type t = a.GetType("StockManager.ViewModel.MainViewModel");
                object obj = Activator.CreateInstance(t,connection);
                MethodInfo m = t.GetMethod("StartApplication");//, BindingFlags.Public | BindingFlags.Instance, null, new Object[] { typeof(string) });
                m.Invoke(obj,null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error running Kiosk Application : ");
                Console.WriteLine(ex.Message);
                Console.WriteLine("press any key to continue ...");
                Console.Read();

               
            }
        
          
            #endregion
        }
    }
}
