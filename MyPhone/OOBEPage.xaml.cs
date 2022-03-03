// OOBEPage
// OOBE Page wizart (hangling some work at "first start")

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using GoodTimeStudio.MyPhone.ViewModels;


// namespace
namespace GoodTimeStudio.MyPhone
{
    // OOBEPage 
    public sealed partial class OOBEPage : Page
    {
        // OOBE Page vm
        public OOBEPageViewModel ViewModel;

        // Bluetooth DeviceList vm
        public BluetoothDeviceListViewModel DListModel;

        MenuFlyout _ContextMenu;

        // OOBEPage
        public OOBEPage()
        {
            this.InitializeComponent();

            DListModel = _List.ViewModel;
            
            ViewModel = new OOBEPageViewModel(DListModel);

            _ContextMenu = new MenuFlyout();
            _ContextMenu.Items.Add(new MenuFlyoutItem { Text = "Exit", Icon = new SymbolIcon(Symbol.Clear) });

            Style style = new Style(typeof(MenuFlyoutPresenter));
            style.Setters.Add(new Setter(Windows.UI.Xaml.FrameworkElement.MinWidthProperty, 150));
            _ContextMenu.MenuFlyoutPresenterStyle = style;

        }//OOBEPage end 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DListModel.DeviceScanStart();
        }

        private async void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.Connect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] OOBEPage ViewModel.Connect Exception: " +
                    ex.Message);
            }
        }

        private async void ButtonDeviceScanStart_Click(object sender, RoutedEventArgs e)
        {
            //
            try
            {
                //await 
                DListModel.DeviceScanStart();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] OOBEPage ViewModel.Connect Exception: " +
                    ex.Message);
            }
        }

        private async void ButtonDeviceScanStop_Click(object sender, RoutedEventArgs e)
        {
            //
            try
            {
                //await 
                DListModel.DeviceScanStop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] OOBEPage ViewModel.Connect Exception: " +
                    ex.Message);
            }
        }
    }
}
