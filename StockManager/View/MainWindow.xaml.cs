using StockManager.View;
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

namespace StockManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUcControl
    {
        
        public MainWindow()
        {
            InitializeComponent();
            CurrentPage = this;
        }

        private void Btn_Navigation_Click(object sender, RoutedEventArgs e)
        {
            CallCustomEvent(this.CurrentPage, e, ((Button)sender).Tag.ToString(),"");
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Image_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        public void UpdateTitile(string Titletext)
        {
            this.Dispatcher.Invoke((Action)delegate
                     {
                         lbl_Title.Text = Titletext;
                     });
        }
        public event BtnClick BtnClickEvent;

        public void CallCustomEvent(object sender, RoutedEventArgs e, string commandCode, object dataModel)
        {
            if (BtnClickEvent != null)
            {
                BtnClickEvent(sender, e, sender.GetType().Name, commandCode, dataModel);
            }
        }

        public bool EnableBack
        {
            get;
            set;
        }

        public bool EnableHome
        {
            get;
            set;
        }

        public bool EnableNext
        {
            get;
            set;
        }
        public string TitleText
        {
            get;
            set;
        }

        public Control CurrentPage { get; set; }

        internal void ShowPage(Control Control, bool IsProcessing = false)
        {
            this.Dispatcher.Invoke((Action)delegate
           {
               panel_pages.Children.Clear();
               if (!IsProcessing)
                   CurrentPage = Control;

               if(CurrentPage is UcReport)
               {
                   this.WindowState = WindowState.Maximized;
               }
               else
               {
                   this.WindowState = WindowState.Normal;
               }
               lbl_Title.Text = ((IUcControl)Control).TitleText.ToUpper();
               btnBack.Visibility = ((IUcControl)Control).EnableBack == true ? Visibility.Visible : Visibility.Collapsed;
               btnHome.Visibility = ((IUcControl)Control).EnableHome == true ? Visibility.Visible : Visibility.Collapsed;
               btnNext.Visibility = ((IUcControl)Control).EnableNext == true ? Visibility.Visible : Visibility.Collapsed;
               panel_pages.Children.Add(Control);
           });

        }

    }
}
