﻿#pragma checksum "..\..\Add.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BC58B157AAAE5F9339BAD74DDA12DBB8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Hash;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Hash {
    
    
    /// <summary>
    /// Add
    /// </summary>
    public partial class Add : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderAdd;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Scrolltxb;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPan;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AddMany;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddCount;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox FromBase;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DiamL;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SuperSlider3000;
        
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
            System.Uri resourceLocater = new System.Uri("/Hash;component/add.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Add.xaml"
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
            
            #line 8 "..\..\Add.xaml"
            ((Hash.Add)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Add_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BorderAdd = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.Scrolltxb = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.StackPan = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.AddMany = ((System.Windows.Controls.CheckBox)(target));
            
            #line 30 "..\..\Add.xaml"
            this.AddMany.Click += new System.Windows.RoutedEventHandler(this.AddMany_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddCount = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\Add.xaml"
            this.AddCount.KeyDown += new System.Windows.Input.KeyEventHandler(this.AddCount_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FromBase = ((System.Windows.Controls.CheckBox)(target));
            
            #line 34 "..\..\Add.xaml"
            this.FromBase.Click += new System.Windows.RoutedEventHandler(this.FromBase_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 35 "..\..\Add.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DiamL = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.SuperSlider3000 = ((System.Windows.Controls.Slider)(target));
            
            #line 40 "..\..\Add.xaml"
            this.SuperSlider3000.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SuperSlider3000_ValueChanged);
            
            #line default
            #line hidden
            
            #line 40 "..\..\Add.xaml"
            this.SuperSlider3000.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SuperSlider3000_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

