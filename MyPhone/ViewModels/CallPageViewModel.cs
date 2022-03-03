// CallPageViewModel
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Calls;
using Windows.UI.Xaml;


// GoodTimeStudio.MyPhone.ViewModels namespace
namespace GoodTimeStudio.MyPhone.ViewModels
{
    // CallPageViewModel class 
    public class CallPageViewModel : BindableBase
    {

        // PhoneNumber
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => SetProperty(ref _PhoneNumber, value);
        }

        // InputIndex
        private int _InputIndex;
        public int InputIndex
        {
            get => _InputIndex;
            set => SetProperty(ref _InputIndex, value);
        }

        // SelectionLength
        private int _SelectionLength;
        public int SelectionLength
        {
            get => _SelectionLength;
            set => SetProperty(ref _SelectionLength, value);
        }

        //ButtonCall_Click
        public void ButtonCall_Click(object sender, RoutedEventArgs e)
        {
            DeviceManager.Call(PhoneNumber);

        }//ButtonCall_Click end

    }//CallPageViewModel class end

}//namespace end

