﻿#pragma checksum "..\..\..\Tabs\SavingForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FA4BAD88642CFE30161450E83BADAFFE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExchangeRates.Controls;
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


namespace ExchangeRates.Controls {
    
    
    /// <summary>
    /// SavingForm
    /// </summary>
    public partial class SavingForm : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 11 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid tmpGrid;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid headerGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lineControl;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox newValuteBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Itogo;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label textLabel;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox countedBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Tabs\SavingForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox outValute;
        
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
            System.Uri resourceLocater = new System.Uri("/ExchangeRates;component/tabs/savingform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Tabs\SavingForm.xaml"
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
            this.tmpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.headerGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lineControl = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.newValuteBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 45 "..\..\..\Tabs\SavingForm.xaml"
            this.newValuteBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.newValuteBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Itogo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.textLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.countedBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.outValute = ((System.Windows.Controls.ComboBox)(target));
            
            #line 49 "..\..\..\Tabs\SavingForm.xaml"
            this.outValute.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.outValute_SelectionChanged);
            
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
            case 5:
            
            #line 39 "..\..\..\Tabs\SavingForm.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.convertBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\Tabs\SavingForm.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.convertBox_TextChanged);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 40 "..\..\..\Tabs\SavingForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
