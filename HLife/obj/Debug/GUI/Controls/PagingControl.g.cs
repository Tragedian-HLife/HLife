﻿#pragma checksum "..\..\..\..\GUI\Controls\PagingControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CC88D5BE85D22F85407F9170DE09D398"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HLife;
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


namespace HLife {
    
    
    /// <summary>
    /// PagingControl
    /// </summary>
    public partial class PagingControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFirstPage;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPageIndex;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNumberOfPages;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextPage;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\GUI\Controls\PagingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLastPage;
        
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
            System.Uri resourceLocater = new System.Uri("/HLife;component/gui/controls/pagingcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\GUI\Controls\PagingControl.xaml"
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
            this.btnFirstPage = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\GUI\Controls\PagingControl.xaml"
            this.btnFirstPage.Click += new System.Windows.RoutedEventHandler(this.btnFirstPage_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnPreviousPage = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\GUI\Controls\PagingControl.xaml"
            this.btnPreviousPage.Click += new System.Windows.RoutedEventHandler(this.btnPreviousPage_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lblPageIndex = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblNumberOfPages = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnNextPage = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\GUI\Controls\PagingControl.xaml"
            this.btnNextPage.Click += new System.Windows.RoutedEventHandler(this.btnNextPage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnLastPage = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\GUI\Controls\PagingControl.xaml"
            this.btnLastPage.Click += new System.Windows.RoutedEventHandler(this.btnLastPage_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

