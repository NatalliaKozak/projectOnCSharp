﻿#pragma checksum "..\..\..\Views\addInfoSub.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "07D582773F091FD9D609586B46C2A1A68B16D2C64BF50466A632FD29268806AA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using kurs.Views;


namespace kurs.Views {
    
    
    /// <summary>
    /// addInfoSub
    /// </summary>
    public partial class addInfoSub : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 99 "..\..\..\Views\addInfoSub.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid info;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Views\addInfoSub.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button del;
        
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
            System.Uri resourceLocater = new System.Uri("/kurs;component/views/addinfosub.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\addInfoSub.xaml"
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
            
            #line 8 "..\..\..\Views\addInfoSub.xaml"
            ((kurs.Views.addInfoSub)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.info = ((System.Windows.Controls.DataGrid)(target));
            
            #line 106 "..\..\..\Views\addInfoSub.xaml"
            this.info.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.info_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 107 "..\..\..\Views\addInfoSub.xaml"
            this.info.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 119 "..\..\..\Views\addInfoSub.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.openWindowForAddNewSubb);
            
            #line default
            #line hidden
            return;
            case 4:
            this.del = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\Views\addInfoSub.xaml"
            this.del.Click += new System.Windows.RoutedEventHandler(this.delSub);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 139 "..\..\..\Views\addInfoSub.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.refreshList);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

