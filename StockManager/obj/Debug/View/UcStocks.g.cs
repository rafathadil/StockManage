﻿#pragma checksum "..\..\..\View\UcStocks.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0AD70EB67C0C2E87B642BB9A0504653166C5CD0EDA3892056918FFE27636FEFA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using RootLibrary.WPF.Localization;
using StockManager;
using StockManager.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace StockManager.View {
    
    
    /// <summary>
    /// UcStocks
    /// </summary>
    public partial class UcStocks : StockManager.UcControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 33 "..\..\..\View\UcStocks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Confrim;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\View\UcStocks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.DataGrid McDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StockManager;component/view/ucstocks.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\UcStocks.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_Confrim = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\View\UcStocks.xaml"
            this.btn_Confrim.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.McDataGrid = ((Microsoft.Windows.Controls.DataGrid)(target));
            
            #line 41 "..\..\..\View\UcStocks.xaml"
            this.McDataGrid.CellEditEnding += new System.EventHandler<Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs>(this.McDataGrid_CellEditEnding_1);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\View\UcStocks.xaml"
            this.McDataGrid.BeginningEdit += new System.EventHandler<Microsoft.Windows.Controls.DataGridBeginningEditEventArgs>(this.McDataGrid_BeginningEdit);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 3:
            
            #line 87 "..\..\..\View\UcStocks.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItemButtonClick);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 102 "..\..\..\View\UcStocks.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.CmbBox_Loaded);
            
            #line default
            #line hidden
            
            #line 103 "..\..\..\View\UcStocks.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbBox_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 122 "..\..\..\View\UcStocks.xaml"
            ((System.Windows.Controls.DatePicker)(target)).Loaded += new System.Windows.RoutedEventHandler(this.DatePicker_Loaded);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

