﻿#pragma checksum "C:\Users\Pawel Borzym\Desktop\WallET\WallET\WallET\Views\MapPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C28021B27E08E08C524603044EC27A46"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WallET.Views
{
    partial class MapPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.StoreMap = (global::Windows.UI.Xaml.Controls.Maps.MapControl)(target);
                }
                break;
            case 2:
                {
                    this.showStore = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 42 "..\..\..\Views\MapPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.showStore).Tapped += this.showStore_Tapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.MapSrc = (global::Windows.UI.Xaml.Controls.Maps.MapItemsControl)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 22 "..\..\..\Views\MapPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Tapped += this.mapSrcBtn_Tapped;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

