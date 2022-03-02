// BluetoothDeviceListViewModel

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Enumeration;
using Windows.UI.Core;

// GoodTimeStudio.MyPhone.Models
namespace GoodTimeStudio.MyPhone.Models
{
    public class BluetoothDeviceListViewModel : BindableBase
    {
        #region "Variables"
        // device watcher
        private DeviceWatcher _DeviceWatcher;

        // devices
        public ObservableCollection<DeviceInformation> Devices;

        // selected device
        private DeviceInformation _SelectedDevice;
        #endregion

        #region "Properties"
        // get-set selected device's info
        public DeviceInformation SelectedDevice
        {
            get => _SelectedDevice;
            set => SetProperty(ref _SelectedDevice, value);
        }
        #endregion


        // BluetoothDeviceListViewModel
        public BluetoothDeviceListViewModel()
        {
            Devices = new ObservableCollection<DeviceInformation>();
            
            _DeviceWatcher = DeviceInformation.CreateWatcher
             (
                PhoneLineTransportDevice.GetDeviceSelector(PhoneLineTransport.Bluetooth)
             );
            
            _DeviceWatcher.Added += _DeviceWatcher_Added;
            
            _DeviceWatcher.Removed += _DeviceWatcher_Removed;

        }//BluetoothDeviceListViewModel end


        // _DeviceWatcher_Removed
        private void _DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            var de = Devices.Where(d => d.Id == args.Id).FirstOrDefault();
            if (de != null)
            {
                Devices.Remove(de);
            }

        }//_DeviceWatcher_Removed end


        // _DeviceWatcher_Added
        private void _DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            Devices.Add(args);

        }//_DeviceWatcher_Added end


        // DeviceScanStart
        public void DeviceScanStart()
        {
            _DeviceWatcher.Start();

        }//DeviceScanStart end


        // DeviceScanStop
        public void DeviceScanStop()
        {
            _DeviceWatcher.Stop();

        }//DeviceScanStop end

    }//class end

}//namespace end
