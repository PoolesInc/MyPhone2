// OOBEPageViewModel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;


// GoodTimeStudio.MyPhone.ViewModels
namespace GoodTimeStudio.MyPhone.ViewModels
{
    // OOBEPageViewModel class
    public class OOBEPageViewModel : BindableBase
    {
        // Device List vm
        public BluetoothDeviceListViewModel DListModel;

        // OOBE Completed Event (future)
        public event EventHandler OOBECompletedEvent;

        // IsWorking
        private bool _IsWorking;
        public bool IsWorking
        {
            get => _IsWorking;
            set 
            {
                SetProperty(ref _IsWorking, value);
                OnPropertyChanged(nameof(IsNotWorking));
            }
        }

        // IsNotWorking
        public bool IsNotWorking
        {
            get => !IsWorking;
        }


        // OOBEPageViewModel
        public OOBEPageViewModel(BluetoothDeviceListViewModel listModel)
        {
            DListModel = listModel;

        }//OOBEPageViewModel end


        // Connect
        public async Task Connect()
        {
            IsWorking = true;
            
            // error status
            bool IsError = false;
            
            // error message
            string ErrorMsg = "";
            
            try
            {
                // Try to connect...
                if (await DeviceManager.ConnectTo(DListModel.SelectedDevice))
                {
                    var settings = ApplicationData.Current.LocalSettings.Values;
                    settings["OOBE"] = false;
                    App.Navigate(typeof(MainPage));
                }
                else
                {
                   
                    IsError = true;  // Status - Fail
                    ErrorMsg = "Fail to connect"; 
                }
            }
            catch (Exception ex)
            {
                IsError = true; // Status - Fail
                ErrorMsg = ex.Message;
            }

            
            if (IsError)
            {
                ContentDialog PopupDialog = new ContentDialog()
                {
                    Title = ErrorMsg,//"Error",
                    Content = "Check connection and try again.",
                    CloseButtonText = "Ok"
                };

                //Notify user fail to connect (Popup dialog mini-window)
                await PopupDialog.ShowAsync();
            }

            IsWorking = false;

        }//Connect end

    }//class end

}//namespace end
