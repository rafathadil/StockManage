using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StockManager
{
    public delegate void BtnClick(object sender, RoutedEventArgs e, string parentKey, string commandCode, object dataModel = null);

    public interface IUcControl
    {
        event BtnClick BtnClickEvent;
        void CallCustomEvent(object sender, RoutedEventArgs e, string commandCode, object dataModel = null);
        bool EnableBack { get; set; }
        bool EnableHome { get; set; }
        bool EnableNext { get; set; }
        string TitleText { get; set; }

    }
    public abstract class UcControl : UserControl, IUcControl
    {
        #region Custom Event Handling
        public event BtnClick BtnClickEvent;
        public void CallCustomEvent(object sender, RoutedEventArgs e, string commandCode, object dataModel = null)
        {
            if (BtnClickEvent != null)
                BtnClickEvent(sender, e, this.GetType().Name, commandCode,  dataModel);
        }
        #endregion

        #region Properties
        private bool _enableBack = true;
        public bool EnableBack { get { return _enableBack; } set { _enableBack = value; } }
        private bool _enableHome = true;
        public bool EnableHome { get { return _enableHome; } set { _enableHome = value; } }
        private bool _enableNext = true;
        public bool EnableNext { get { return _enableNext; } set { _enableNext = value; } }

        private string _titleText = string.Empty;
        public string TitleText { get { return _titleText; } set { _titleText = value; } }
        #endregion
    }
}
