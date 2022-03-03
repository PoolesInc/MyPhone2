// DeviceManager

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Enumeration;
using Windows.Storage;

using GoodTimeStudio.MyPhone.ViewModels;



// GoodTimeStudio.MyPhone namespace
namespace GoodTimeStudio.MyPhone
{
    // DeviceManager class
    public class DeviceManager
    {
        // device info
        public static DeviceInformation DeviceInfo;

        // line
        public static PhoneLine Line;

        // line id
        public static Guid LineId;

        // state
        public static DeviceState State = DeviceState.Disconnected;

        // line watcher
        private static PhoneLineWatcher LineWatcher;


        // Init
        public static async Task Init()
        {

            // RnD

            Windows.Foundation.Collections.IPropertySet settings = 
                ApplicationData.Current.LocalSettings.Values;
            
            //TEMP "device id" disabled
            if (settings.TryGetValue("deviceId", out object obj))
            {
                if (obj is string)
                {
                    string id = obj as string;
                    if (!string.IsNullOrEmpty(id))
                    {
                        DeviceInfo = await DeviceInformation.CreateFromIdAsync(id);
                        await EnsureInitPhoneLineWatcher();
                        LineWatcher.Start();
                    }
                }
                else
                {
                    // Remove (device)
                    settings.Remove("deviceId");
                }
            }
        }//

        // ConnectTo
        public static async Task<bool> ConnectTo(DeviceInformation deviceInfo)
        {
            if (!await EnsureInitPhoneLineWatcher())
            {
                return false;
            }

            string result = await App.SendRequest("goodtimestudio.myphone.trayapp://connect/" + deviceInfo.Id);
            if (result.StartsWith("goodtimestudio.myphone://connect/"))
            {
                if (result.Substring(result.LastIndexOf('/') + 1) == "true")
                {
                    DeviceInfo = deviceInfo;

                    var settings = ApplicationData.Current.LocalSettings.Values;
                    
                    settings["deviceId"] = deviceInfo.Id;
                    
                    LineWatcher.Start();
                    
                    return true;
                }
            }
            return false;
        }

        public static void Call(string number)
        {
            if (Line !=null && Line.CanDial)
            {
                Line.Dial(number, number);
            }
        }

        public static async Task<bool> EnsureInitPhoneLineWatcher()
        {
            if (LineWatcher == null)
            {
                PhoneCallStore store = await PhoneCallManager.RequestStoreAsync();
                if (store == null)
                {
                    return false;
                }
                LineWatcher = store.RequestLineWatcher();
                LineWatcher.LineAdded += LineWatcher_LineAdded;
                LineWatcher.LineRemoved += LineWatcher_LineRemoved;
                LineWatcher.LineUpdated += LineWatcher_LineUpdated;
            }

            return true;
        }

        private static void LineWatcher_LineUpdated(PhoneLineWatcher sender, PhoneLineWatcherEventArgs args)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("LineUpdated: " + args.LineId);
#endif
        }

        private static void LineWatcher_LineRemoved(PhoneLineWatcher sender, PhoneLineWatcherEventArgs args)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("LineRemoved: " + args.LineId);
#endif
            if (Guid.Equals(args.LineId, LineId))
            {
                Line = null;
                LineId = Guid.Empty;
            }
        }

        private static async void LineWatcher_LineAdded(PhoneLineWatcher sender, PhoneLineWatcherEventArgs args)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("LineAdded: " + args.LineId);
#endif
            PhoneLine line = await PhoneLine.FromIdAsync(args.LineId);
            if (line.TransportDeviceId == DeviceInfo.Id)
            {
                Line = line;
                LineId = args.LineId;
            }
        }
    }//

    public enum DeviceState
    {
        Disconnected,
        LostConnection,
        Connected
    }
}
