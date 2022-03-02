// OOBEPageViewModel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

// GoodTimeStudio.MyPhone.Models
namespace GoodTimeStudio.MyPhone.Models
{
    // OOBEPageViewModel class
    public class OOBEPageViewModel : BindableBase
    {
        // DListModel
        public BluetoothDeviceListViewModel DListModel;

        // OOBECompletedEvent
        public event EventHandler OOBECompletedEvent;


        // OOBEPageViewModel
        public OOBEPageViewModel(BluetoothDeviceListViewModel listModel)
        {
            DListModel = listModel;

        }//OOBEPageViewModel end


        // Connect
        public async Task Connect()
        {
            if (await DeviceManager.ConnectTo(DListModel.SelectedDevice))
            {
                var settings = ApplicationData.Current.LocalSettings.Values;

                settings["OOBE"] = false;
                
                App.Navigate(typeof(MainPage));
            }
            else
            {
                //TODO: Notify user fail to connect
            }

        }//Connect end

    }//class end

}//namespace end
