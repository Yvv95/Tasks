﻿#pragma checksum "..\..\DynamicForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4B6A7005F8343B301B00DBAC0B146741"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExchangeRates;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chart;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace ExchangeRates {
    
    
    /// <summary>
    /// DynamicForm
    /// </summary>
    public partial class DynamicForm : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox selectBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox periodBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.Chart.Chart graphicChart;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.Chart.Legend legenda;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.Chart.Area _area1;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\DynamicForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.Chart.Series _series1;
        
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
            System.Uri resourceLocater = new System.Uri("/ExchangeRates;component/dynamicform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DynamicForm.xaml"
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
            this.selectBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 38 "..\..\DynamicForm.xaml"
            this.selectBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.selectBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.periodBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\DynamicForm.xaml"
            this.periodBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.periodBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.graphicChart = ((Xceed.Wpf.Toolkit.Chart.Chart)(target));
            return;
            case 4:
            this.legenda = ((Xceed.Wpf.Toolkit.Chart.Legend)(target));
            return;
            case 5:
            this._area1 = ((Xceed.Wpf.Toolkit.Chart.Area)(target));
            return;
            case 6:
            this._series1 = ((Xceed.Wpf.Toolkit.Chart.Series)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

