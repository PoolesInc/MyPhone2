// TestPageViewModel

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Enumeration;


// GoodTimeStudio.MyPhone.Models namespace
namespace GoodTimeStudio.MyPhone.Models
{

    // TestPageViewModel class
    public class TestPageViewModel : BindableBase
    {
        // PhoneLines
        public ObservableCollection<Guid> PhoneLines = new ObservableCollection<Guid>();

        // PLTDevices
        public ObservableCollection<DeviceInformation> PLTDevices 
            = new ObservableCollection<DeviceInformation>();

        // _SeletedDevice
        private DeviceInformation _SeletedDevice;

        // SeletedDevice
        public DeviceInformation SelectedDevice
        {
            get => _SeletedDevice;
            set => SetProperty(ref _SeletedDevice, value);
        }

        // _SelectedPhoneLine
        private Guid _SelectedPhoneLine;

        // SelectedPhoneLine
        public Guid SelectedPhoneLine
        {
            get => _SelectedPhoneLine;
            set => SetProperty(ref _SelectedPhoneLine, value);
        }

        // _RegistrationStatus
        private string _RegistrationStatus;

        //RegistrationStatus
        public string RegistrationStatus
        {
            get => _RegistrationStatus;
            set => SetProperty(ref _RegistrationStatus, "Registration status: " + value);
        }

        // _ConnectionStatus
        private string _ConnectionStatus;

        //ConnectionStatus
        public string ConnectionStatus
        {
            get => _ConnectionStatus;
            set => SetProperty(ref _ConnectionStatus, "Connection status: " + value);
        }

        // _IsWorking 
        private bool _IsWorking;

        // IsWorking
        public bool IsWorking
        {
            get => _IsWorking;
            set 
            { 
                SetProperty(ref _IsWorking, value);
                OnPropertyChanged("IsNotWorking");
            }
        }

        // IsNotWorking
        public bool IsNotWorking
        {
            get => !IsWorking;
        }

    }//TestPageViewModel end

}//GoodTimeStudio.MyPhone.Models namespace end
