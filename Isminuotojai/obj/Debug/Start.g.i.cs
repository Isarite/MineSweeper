#pragma checksum "..\..\Start.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "91A83EDF6F3B564F05A6B0E7AF8CE59BFE1B8C109A459D6247BF96C91D10656D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Isminuotojai;
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


namespace Isminuotojai
{


    /// <summary>
    /// Start
    /// </summary>
    public partial class Start : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 12 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel startScreen;

#line default
#line hidden


#line 13 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_login;

#line default
#line hidden


#line 14 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_register;

#line default
#line hidden


#line 16 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid loginForm;

#line default
#line hidden


#line 36 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_username;

#line default
#line hidden


#line 39 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_password;

#line default
#line hidden


#line 44 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid registerForm;

#line default
#line hidden


#line 64 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_username_reg;

#line default
#line hidden


#line 67 "..\..\Start.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_password_reg;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Isminuotojai;component/start.xaml", System.UriKind.Relative);

#line 1 "..\..\Start.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.startScreen = ((System.Windows.Controls.StackPanel)(target));
                    return;
                case 2:
                    this.btn_login = ((System.Windows.Controls.Button)(target));

#line 13 "..\..\Start.xaml"
                    this.btn_login.Click += new System.Windows.RoutedEventHandler(this.btn_login_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.btn_register = ((System.Windows.Controls.Button)(target));

#line 14 "..\..\Start.xaml"
                    this.btn_register.Click += new System.Windows.RoutedEventHandler(this.btn_register_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.loginForm = ((System.Windows.Controls.Grid)(target));
                    return;
                case 5:
                    this.txt_username = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 6:
                    this.txt_password = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 7:
                    this.registerForm = ((System.Windows.Controls.Grid)(target));
                    return;
                case 8:
                    this.txt_username_reg = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 9:
                    this.txt_password_reg = ((System.Windows.Controls.TextBox)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button btn_play;
    }
}

