// MainPage

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;

using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using MUXC = Microsoft.UI.Xaml.Controls;

// GoodTimeStudio.MyPhone namespace
namespace GoodTimeStudio.MyPhone
{
    // MainPage class
    public sealed partial class MainPage : Page
    {
        #region "Constructor"

        // MainPage
        public MainPage()
        {
            this.InitializeComponent();
            
            Window.Current.SetTitleBar(AppTitleBar);
        
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, e) => UpdateAppTitle(s);
        
        }//MainPage end
        #endregion


        // GetAppTitleFromSystem
        public string GetAppTitleFromSystem()
        {
            return Windows.ApplicationModel.Package.Current.DisplayName;

        }//GetAppTitleFromSystem end


        // UpdateAppTitle
        void UpdateAppTitle(CoreApplicationViewTitleBar coreTitleBar)
        {
            //ensure the custom title bar does not overlap window caption controls
            
            Thickness currMargin = AppTitleBar.Margin;
            
            AppTitleBar.Margin = new Thickness(currMargin.Left, currMargin.Top, 
                coreTitleBar.SystemOverlayRightInset, currMargin.Bottom);

        }//UpdateAppTitle end


        // NavigationViewControl_ItemInvoked
        private void NavigationViewControl_ItemInvoked(MUXC.NavigationView sender, 
            MUXC.NavigationViewItemInvokedEventArgs args)
        {
            if(args.InvokedItem == _Tab_Call)
            {
                //
            }
            else if (args.InvokedItem == _Tab_Message)
            {
                //
            }
            else if (args.IsSettingsInvoked)
            {
                //
            }
        }//NavigationViewControl_ItemInvoked end

    }//class end

}//namespace end
