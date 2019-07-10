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

namespace StockManager.View
{
    /// <summary>
    /// Interaction logic for UcProcessingScreen.xaml
    /// </summary>
    public partial class UcProcessingScreen : UcControl
    {
        public UcProcessingScreen()
        {
            InitializeComponent();
            base.EnableBack = base.EnableHome = base.EnableNext = false;
            TitleText = "please wait";
            StartProgressAnimation();
        }
        #region Properties
        public void StartProgressAnimation()
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\Images\img_Processing.gif";
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        img_Processing.GIFSource = filePath;
                        img_Processing.PlayAnimation = true;
                    }
                }
            }, null);
        }
        public void StopProgressAnimation()
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                img_Processing.PlayAnimation = false;
            }, null);
        }
        public string ProcessingMessage
        {
            get
            {
                string response = string.Empty;
                this.Dispatcher.Invoke((Action)delegate
                {
                    response = lbl_ProcessingMessage.Text;
                });
                return response;
            }
            set
            {
                this.Dispatcher.Invoke((Action)delegate
                {
                    lbl_ProcessingMessage.Text = value;
                });
            }
        }
        public void SetProgressBarVisibility(bool visible)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                img_Processing.Visibility = (visible) ? Visibility.Visible : Visibility.Collapsed;
            }, null);
        }
        #endregion
    }
}
