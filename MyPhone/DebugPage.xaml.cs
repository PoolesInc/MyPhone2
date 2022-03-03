// DebugPage
// Not ready yet. TODO!

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
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
    // DebugPage class
    public sealed partial class DebugPage : Page
    {
        // vm
        public DebugPageViewModel ViewModel;

        // line watcher
        PhoneLineWatcher _LineWatcher;
        
        // device watcher
        DeviceWatcher _DeviceWatcher;

        // locker
        private object locker = new object();

        // selected device
        PhoneLineTransportDevice SelectedDevice;

        // DebugPage
        public DebugPage()
        {
            this.InitializeComponent();

            ViewModel = new DebugPageViewModel();

        }//DebugPage end


        // OnNavigatedTo
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_LineWatcher == null)
            {
                PhoneCallStore store = await PhoneCallManager.RequestStoreAsync();
                _LineWatcher = store.RequestLineWatcher();
                _LineWatcher.LineAdded += _watcher_LineAdded;
                _LineWatcher.LineRemoved += _watcher_LineRemoved;

                _LineWatcher.Start();
            }
            string str = PhoneLineTransportDevice.GetDeviceSelector(PhoneLineTransport.Bluetooth);
            if (_DeviceWatcher == null)
            {
                _DeviceWatcher = DeviceInformation.CreateWatcher(str);
                _DeviceWatcher.Added += _DeviceWatcher_Added;
                _DeviceWatcher.Removed += _DeviceWatcher_Removed;

                _DeviceWatcher.Start();
            }

            string str1 = BluetoothDevice.GetDeviceSelector();
            var watch = DeviceInformation.CreateWatcher(str1);

            watch.Added += _DeviceWatcher_Added;
            watch.Removed += _DeviceWatcher_Removed;

            watch.Start();
        
        }//OnNavigatedTo end


        // _DeviceWatcher_Removed
        private async void _DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var de = ViewModel.PLTDevices.Where(d => d.Id == args.Id).FirstOrDefault();
                
                if (de != null)
                {
                    ViewModel.PLTDevices.Remove(de);
                }
            });

        }//_DeviceWatcher_Removed end


        // _DeviceWatcher_Added
        private async void _DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ViewModel.PLTDevices.Add(args);
            });

        }//_DeviceWatcher_Added end


        // _watcher_LineAdded
        private async void _watcher_LineAdded(PhoneLineWatcher sender, PhoneLineWatcherEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ViewModel.PhoneLines.Add(args.LineId);
            });

        }//_watcher_LineAdded end


        // _watcher_LineRemoved
        private async void _watcher_LineRemoved
        (
            PhoneLineWatcher sender, 
            PhoneLineWatcherEventArgs args
        )
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Line removing: {0}", args.LineId);
#endif
                Guid id = args.LineId;

                ViewModel.PhoneLines.Remove(id);
            });

        }//_watcher_LineRemoved end


        // Button_Click_Call
        private async void Button_Click_Call(object sender, RoutedEventArgs e)
        {
            if (_PhoneLineList.SelectedItem == null)
            {
                return;
            }

            PhoneLine line = await PhoneLine.FromIdAsync((Guid)_PhoneLineList.SelectedItem);
            
            if (line == null)
            {
                return;
            }

            if (line.CanDial)
            {
                line.Dial(PhoneNumberBox.Text, PhoneNumberBox.Text);
            }

        }//Button_Click_Call end


        // Button_Click_ConnectDevice
        private async void Button_Click_ConnectDevice(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedDevice == null)
            {
                return;
            }

            ViewModel.IsWorking = true;
            
            if (await SelectedDevice.ConnectAsync())
            {
                ViewModel.ConnectionStatus = "Success";
            }
            else
            {
                ViewModel.ConnectionStatus = "Failed";
            }

            ViewModel.IsWorking = false;

        }//Button_Click_ConnectDevice end


        // _PLTDList_SelectionChanged
        private async void _PLTDList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.RegistrationStatus = string.Empty;

            PhoneLineTransportDevice device 
                = PhoneLineTransportDevice.FromId(ViewModel.SelectedDevice.Id);
            
            if (device == null)
            {
                return;
            }

            SelectedDevice = device;
            var result = await device.RequestAccessAsync();
            if (result != DeviceAccessStatus.Allowed)
            {
                ViewModel.RegistrationStatus = result.ToString();
            }

            RefreashRegStatus();

        }//_PLTDList_SelectionChanged end


        // Button_Click_RegisterApp
        private void Button_Click_RegisterApp(object sender, RoutedEventArgs e)
        {
            if (SelectedDevice != null)
            {
                SelectedDevice.RegisterApp();
                
                RefreashRegStatus();
            }
        }


        // Button_Click_UnregisterApp
        private void Button_Click_UnregisterApp(object sender, RoutedEventArgs e)
        {

            if (SelectedDevice != null)
            {
                SelectedDevice.UnregisterApp();

                RefreashRegStatus();
            }

        }//Button_Click_UnregisterApp end


        // RefreashRegStatus
        private void RefreashRegStatus()
        {
            ViewModel.RegistrationStatus = SelectedDevice.IsRegistered().ToString();

        }//RefreashRegStatus end


        // Button_Click_GetRfcommService
        private async void Button_Click_GetRfcommServices(object sender, RoutedEventArgs e)
        {
            // get bt device
            BluetoothDevice bt = 
                await BluetoothDevice.FromIdAsync(ViewModel.SelectedDevice.Id);
            
            // get rf comm services
            RfcommDeviceServicesResult result = await bt.GetRfcommServicesAsync();
            
            // cycle sdp records
            foreach (var b in bt.SdpRecords)
            {
                // TODO: refresh output

                Debug.WriteLine(Encoding.UTF8.GetString(b.ToArray()));
            }
            
            // cycle services
            foreach (var service in result.Services)
            {

                // TODO: refresh output
                
                Debug.WriteLine(service.ServiceId.AsString() + "    " 
                    + service.ConnectionHostName + "    " 
                    + service.ConnectionServiceName);
            }
        }//Button_Click_GetRfcommService end

    }//class end

}//namespace end
