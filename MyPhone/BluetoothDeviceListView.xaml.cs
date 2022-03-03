// BluetoothDeviceListView
// UserControl

using System;
using System.Collections.Generic;
using System.ComponentModel;
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


// GoodTimeStudio.MyPhone namespace

namespace GoodTimeStudio.MyPhone
{
    // BluetoothDeviceListView class 
    public sealed partial class BluetoothDeviceListView : UserControl
    {
        // vm
        public BluetoothDeviceListViewModel ViewModel;

        // BluetoothDeviceListView
        public BluetoothDeviceListView()
        {
            this.InitializeComponent();

            ViewModel = new BluetoothDeviceListViewModel();

        }//

    }//BluetoothDeviceListView end

}//namespace end
