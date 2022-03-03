// DebugPageViewModel
// Debug Page ViewModel

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
    // DebugPageViewModel class
    public class DebugPageViewModel : BindableBase
    {
        // Phone Lines
        public ObservableCollection<Guid> PhoneLines = new ObservableCollection<Guid>();

        // PLT Devices
        public ObservableCollection<DeviceInformation> PLTDevices 
            = new ObservableCollection<DeviceInformation>();

        // SelectedDevice
        private DeviceInformation _SeletedDevice;
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

        // RegistrationStatus
        private string _RegistrationStatus;
        public string RegistrationStatus
        {
            get => _RegistrationStatus;
            set => SetProperty(ref _RegistrationStatus, "Registration status: " + value);
        }

        // ConnectionStatus
        private string _ConnectionStatus;
        public string ConnectionStatus
        {
            get => _ConnectionStatus;
            set => SetProperty(ref _ConnectionStatus, "Connection status: " + value);
        }

        // IsWorking
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

        // IsNotWorking
        public bool IsNotWorking
        {
            get => !IsWorking;
        }

    }//class end

}//namespace end
