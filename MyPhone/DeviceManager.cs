using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.System;

using GoodTimeStudio.MyPhone.Models;


// GoodTimeStudio.MyPhone namespace
namespace GoodTimeStudio.MyPhone
{
    // DeviceManager class
    public class DeviceManager
    {
        // device info
        public static DeviceInformation DeviceInfo;

        // call device
        public static PhoneLineTransportDevice CallDevice;
        
        // bluetooth device
        public static BluetoothDevice BthDevice;

        // device state
        public static DeviceState State = DeviceState.Disconnected;

        // ConnectTo
        public static async Task<bool> ConnectTo(DeviceInformation deviceInfo)
        {
            CallDevice = null;
            
            try
            {
                CallDevice = PhoneLineTransportDevice.FromId(deviceInfo.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] ConnectTo(Device) Exception: " + ex.Message);
            }            

            if (CallDevice == null)
            {
                return false;
            }

            DeviceAccessStatus status = await CallDevice.RequestAccessAsync();
            if (status != DeviceAccessStatus.Allowed)
            {
                return false;
            }

            BthDevice = await BluetoothDevice.FromIdAsync(deviceInfo.Id);
            DeviceInfo = deviceInfo;

            if (!CallDevice.IsRegistered())
            {
                CallDevice.RegisterApp();
            }
            
            State = DeviceState.Connected;

            return true;

        }//ConnectTo end
    
    }//DeviceManager class end


    // DeviceState
    public enum DeviceState
    {
        Disconnected,
        LostConnection,
        Connected
    }
}//namespace end
