using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// GoodTimeStudio.MyPhone namespace
namespace GoodTimeStudio.MyPhone
{
    // App class
    sealed partial class App : Application
    {
        private ApplicationViewTitleBar _TitleBar;
        private Frame rootFrame;
        
        private static App Instance;

        public static bool Navigate(Type sourcePageType)
        {
            return Instance.rootFrame.Navigate(sourcePageType);
        }

        
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            Instance = this;
        }

       
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            rootFrame = Window.Current.Content as Frame;

           
            if (rootFrame == null)
            {
               
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //
                }

                
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    
                    var settings = ApplicationData.Current.LocalSettings.Values;
                    
                    // oobe flag init
                    bool oobe = true;
                    
                    if (settings.TryGetValue("OOBE", out object obj))
                    {
                        // oobe check
                        if (obj is bool && !(bool)obj)
                        {
                            // no oobe
                            oobe = false;
                        }
                    }
                    
                    if (oobe)
                    {
                        //rootFrame.Navigate(typeof(TestPage), e.Arguments);
                        rootFrame.Navigate(typeof(OOBEPage), e.Arguments);
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    }

                }
                
                Window.Current.Activate();
            }

            //draw into the title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            //remove the solid-colored backgrounds behind the caption controls and system back button
            _TitleBar = ApplicationView.GetForCurrentView().TitleBar;
            _TitleBar.ButtonBackgroundColor = Colors.Transparent;
            _TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            var uiSettings = new UISettings();
            uiSettings.ColorValuesChanged += UiSettings_ColorValuesChanged;
            setupSystemCaptionColor(uiSettings);
        }

        private void setupSystemCaptionColor(UISettings settings)
        {
            var color = settings.GetColorValue(UIColorType.Background);
            if (color == Colors.White)
            {
                _TitleBar.ButtonForegroundColor = Colors.Black;
            }
            else
            {
                _TitleBar.ButtonForegroundColor = Colors.White;
            }
        }

        private void UiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            setupSystemCaptionColor(sender);
        }

       
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

       
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            
            //TODO: suspending...
            deferral.Complete();
        }
    }
}
