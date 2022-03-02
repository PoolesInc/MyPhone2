﻿// OOBEPage

using System;
using System.Collections.Generic;
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

using GoodTimeStudio.MyPhone.Models;


// GoodTimeStudio.MyPhone namespace
namespace GoodTimeStudio.MyPhone
{
    // OOBEPage class
    public sealed partial class OOBEPage : Page
    {
        //
        public OOBEPageViewModel ViewModel;

        //
        public BluetoothDeviceListViewModel DListModel;

        //
        MenuFlyout _ContextMenu;


        #region "constructor" 
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
        }
        #endregion

        #region "oobe"
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DListModel.DeviceScanStart();
        }

        private async void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            // Non-operable at now. TODO
            await ViewModel.Connect();

            _ContextMenu.ShowAt(grid);

        }
        #endregion

    }//class end

}//namespace end
