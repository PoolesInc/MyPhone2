// SetPageViewModel
// Not ready yet. TODO!

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Calls;
using Windows.Devices.Enumeration;


// GoodTimeStudio.MyPhone.ViewModels namespace
namespace GoodTimeStudio.MyPhone.ViewModels
{

    // SetPageViewModel class
    public class SetPageViewModel : BindableBase
    {
        /*
         
        // this is my "code snippet" for fast new feature dev =)
         
        public ObservableCollection<Guid> PhoneLines = new ObservableCollection<Guid>();
        
        public ObservableCollection<DeviceInformation> PLTDevices 
            = new ObservableCollection<DeviceInformation>();

        private DeviceInformation _SeletedDevice;

        // SelectedDevice
        public DeviceInformation SelectedDevice
        {
            get => _SeletedDevice;
            set => SetProperty(ref _SeletedDevice, value);
        }

        // SelectedPhoneLine
        private Guid _SelectedPhoneLine;
        public Guid SelectedPhoneLine
        {
            get => _SelectedPhoneLine;
            set => SetProperty(ref _SelectedPhoneLine, value);
        }

        private string _RegistrationStatus;
        public string RegistrationStatus
        {
            get => _RegistrationStatus;
            set => SetProperty(ref _RegistrationStatus, "Registration status: " + value);
        }

        private string _ConnectionStatus;
        public string ConnectionStatus
        {
            get => _ConnectionStatus;
            set => SetProperty(ref _ConnectionStatus, "Connection status: " + value);
        }

        private bool _IsWorking;
        public bool IsWorking
        {
            get => _IsWorking;
            set 
            { 
                SetProperty(ref _IsWorking, value);
                OnPropertyChanged("IsNotWorking");
            }
        }

        public bool IsNotWorking
        {
            get => !IsWorking;
        }
        */
    }
}
