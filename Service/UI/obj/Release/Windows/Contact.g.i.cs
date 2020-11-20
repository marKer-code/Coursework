﻿#pragma checksum "..\..\..\Windows\Contact.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "373A1E06FCE53135F855940A3C9529768F8A2BF62DFFD4E4587F685ED9782C61"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using UI;


namespace UI {
    
    
    /// <summary>
    /// Contact
    /// </summary>
    public partial class Contact : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label B_Close;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_menu;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image i_profile;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Badged chat_badged;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Badged allContacts_badget;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Badged addFriend_badget;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_contacts;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gr_contactInfo;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image avatar_fr;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label l_login_r;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock login;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label l_nickname_r;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock nickname;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label l_status_r;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock status;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Windows\Contact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_remove_fr;
        
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
            System.Uri resourceLocater = new System.Uri("/UI;component/windows/contact.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Contact.xaml"
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
            
            #line 23 "..\..\..\Windows\Contact.xaml"
            this.B_Close.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Close_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lb_menu = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.i_profile = ((System.Windows.Controls.Image)(target));
            
            #line 35 "..\..\..\Windows\Contact.xaml"
            this.i_profile.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.I_Profile_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.chat_badged = ((MaterialDesignThemes.Wpf.Badged)(target));
            
            #line 41 "..\..\..\Windows\Contact.xaml"
            this.chat_badged.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Chat_badged_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.allContacts_badget = ((MaterialDesignThemes.Wpf.Badged)(target));
            
            #line 49 "..\..\..\Windows\Contact.xaml"
            this.allContacts_badget.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.AllContacts_badget_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addFriend_badget = ((MaterialDesignThemes.Wpf.Badged)(target));
            
            #line 57 "..\..\..\Windows\Contact.xaml"
            this.addFriend_badget.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.AddFriend_badget_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lb_contacts = ((System.Windows.Controls.ListBox)(target));
            
            #line 66 "..\..\..\Windows\Contact.xaml"
            this.lb_contacts.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Lb_contacts_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.gr_contactInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.avatar_fr = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.l_login_r = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.login = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.l_nickname_r = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.nickname = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.l_status_r = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.status = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.bt_remove_fr = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\Windows\Contact.xaml"
            this.bt_remove_fr.Click += new System.Windows.RoutedEventHandler(this.Bt_remove_fr_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

