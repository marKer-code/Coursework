﻿#pragma checksum "..\..\..\Windows\Initial.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C9B72B8D1DD741E76C139F0D7D6EB475A38CF042ACAA96A6B633EC62F73AEF60"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using UI.Windows;


namespace UI.Windows {
    
    
    /// <summary>
    /// Initial
    /// </summary>
    public partial class Initial : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label B_Close;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tb_Login;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image B_Ruby;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Pb_Password;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tb_Password;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button B_SignIn;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Windows\Initial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button B_SignUp;
        
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
            System.Uri resourceLocater = new System.Uri("/UI;component/windows/initial.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Initial.xaml"
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
            this.B_Close = ((System.Windows.Controls.Label)(target));
            
            #line 16 "..\..\..\Windows\Initial.xaml"
            this.B_Close.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Close_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Tb_Login = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.B_Ruby = ((System.Windows.Controls.Image)(target));
            
            #line 43 "..\..\..\Windows\Initial.xaml"
            this.B_Ruby.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Ruby_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Pb_Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.Tb_Password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.B_SignIn = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Windows\Initial.xaml"
            this.B_SignIn.Click += new System.Windows.RoutedEventHandler(this.B_SignIn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.B_SignUp = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\Windows\Initial.xaml"
            this.B_SignUp.Click += new System.Windows.RoutedEventHandler(this.B_SignUp_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
