// Main Page

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

using GoodTimeStudio.MyPhone.ViewModels;

using MUXC = Microsoft.UI.Xaml.Controls;


// GoodTimeStudio.MyPhone class 
namespace GoodTimeStudio.MyPhone
{
    // MainPage class
    public sealed partial class MainPage : Page
    {
        // vm
        public MainPageViewModel ViewModel { get; set; }

        // App Title Display name
        public string AppTitleDisplayName { get => Windows.ApplicationModel.Package.Current.DisplayName; }


        // MainPage
        public MainPage()
        {
            this.InitializeComponent();
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Hide default title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);

            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);

            // Register a handler for when the size of the overlaid caption control changes.
            // For example, when the app moves to a screen with a different DPI.
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            //Register a handler for when the window changes focus
            Window.Current.Activated += Current_Activated;

            ViewModel = new MainPageViewModel();

        }//MainPage end


        // CoreTitleBar_LayoutMetricsChanged
        private void CoreTitleBar_LayoutMetricsChanged
        (
            CoreApplicationViewTitleBar sender, 
            object args
        )
        {
            UpdateTitleBarLayout(sender);

        }//CoreTitleBar_LayoutMetricsChanged end


        // UpdateTitleBarLayout
        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Update title bar control size as needed to account for system size changes.
            AppTitleBar.Height = coreTitleBar.Height;

            // Ensure the custom title bar does not overlap window caption controls
            Thickness currMargin = AppTitleBar.Margin;
            AppTitleBar.Margin = new Thickness(currMargin.Left, currMargin.Top, coreTitleBar.SystemOverlayRightInset, currMargin.Bottom);
        
        }//UpdateTitleBarLayout end


        // CoreTitleBar_IsVisibleChanged
        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }

        }//CoreTitleBar_IsVisibleChanged end

            
        // Update the TitleBar based on the inactive/active state of the app
        // Current_Activated
        private void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            SolidColorBrush defaultForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            SolidColorBrush inactiveForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorDisabledBrush"];

            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                AppTitle.Foreground = inactiveForegroundBrush;
            }
            else
            {
                AppTitle.Foreground = defaultForegroundBrush;
            }
        }//Current_Activated end


        // OnNavigatedTo
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            //RnD
            //NavigationViewControl.SelectedItem = TabCall;

        }//OnNavigatedTo

        // NavigationViewControl_SelectionChanged
        private void NavigationViewControl_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                // go to Settings page
                contentFrame.Navigate(typeof(SetPage));
              
            }
            else
            {
                NavigationMenu item = (NavigationMenu)args.SelectedItem; //TODO: better MVVM
                switch (item.Name)
                {
                    case "Call":

                        // go to Call page
                        contentFrame.Navigate(typeof(CallPage));
                        break;
                    case "Message":

                        // go to Message page
                        contentFrame.Navigate(typeof(MessagePage));
                        break;
                    case "Debug":

                        // go to Debug page 
                        contentFrame.Navigate(typeof(DebugPage));
                        break;

                    case "About":
                        contentFrame.Navigate(typeof(AboutPage));
                        break;

                }//switch

            }//else

        }//NavigationViewControl_SelectionChanged end

    }//class end

}//namespace end

