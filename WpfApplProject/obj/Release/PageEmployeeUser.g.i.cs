﻿#pragma checksum "..\..\PageEmployeeUser.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F89E6343CC83A348C12ACC45ACAA643F1CD3FC22631B09A2E97721B1F1D24312"
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
using WpfApplProject;


namespace WpfApplProject {
    
    
    /// <summary>
    /// PageEmployeeUser
    /// </summary>
    public partial class PageEmployeeUser : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridEmployee;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockAuthor;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxNazvanie;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Find_Button;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Combo;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\PageEmployeeUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockAuthor_Копировать;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplProject;component/pageemployeeuser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PageEmployeeUser.xaml"
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
            this.DataGridEmployee = ((System.Windows.Controls.DataGrid)(target));
            
            #line 25 "..\..\PageEmployeeUser.xaml"
            this.DataGridEmployee.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGridEmployee_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBlockAuthor = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.TextBoxNazvanie = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Find_Button = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\PageEmployeeUser.xaml"
            this.Find_Button.Click += new System.Windows.RoutedEventHandler(this.Find_Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Combo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\PageEmployeeUser.xaml"
            this.Combo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Combo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TextBlockAuthor_Копировать = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

