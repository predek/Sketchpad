﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "95A885FAEDF8F4B305910F2534637383"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Sketchpad {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas rectangle1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas labelsRectangle1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PencilButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EraserButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas rectangle2;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label valueLabel;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox expressionTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Test;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 4 "..\..\MainWindow.xaml"
            ((Sketchpad.MainWindow)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 9 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.New_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 10 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Open_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 11 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rectangle1 = ((System.Windows.Controls.Canvas)(target));
            
            #line 16 "..\..\MainWindow.xaml"
            this.rectangle1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseDown);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MainWindow.xaml"
            this.rectangle1.MouseLeave += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseLeave);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MainWindow.xaml"
            this.rectangle1.MouseMove += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseMove);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MainWindow.xaml"
            this.rectangle1.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.labelsRectangle1 = ((System.Windows.Controls.Canvas)(target));
            
            #line 17 "..\..\MainWindow.xaml"
            this.labelsRectangle1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.labelsRectangle1.MouseLeave += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseLeave);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.labelsRectangle1.MouseMove += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseMove);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.labelsRectangle1.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.SelectButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.SelectButton.Click += new System.Windows.RoutedEventHandler(this.SelectButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PencilButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\MainWindow.xaml"
            this.PencilButton.Click += new System.Windows.RoutedEventHandler(this.PencilButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.EraserButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\MainWindow.xaml"
            this.EraserButton.Click += new System.Windows.RoutedEventHandler(this.EraserButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.rectangle2 = ((System.Windows.Controls.Canvas)(target));
            
            #line 28 "..\..\MainWindow.xaml"
            this.rectangle2.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\MainWindow.xaml"
            this.rectangle2.MouseLeave += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseLeave);
            
            #line default
            #line hidden
            
            #line 28 "..\..\MainWindow.xaml"
            this.rectangle2.MouseMove += new System.Windows.Input.MouseEventHandler(this.rectangle1_MouseMove);
            
            #line default
            #line hidden
            
            #line 28 "..\..\MainWindow.xaml"
            this.rectangle2.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.rectangle1_MouseUp);
            
            #line default
            #line hidden
            return;
            case 12:
            this.valueLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.expressionTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\MainWindow.xaml"
            this.expressionTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.expressionTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

