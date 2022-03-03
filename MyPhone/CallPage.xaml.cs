// CallPage

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
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
    // CallPage class 
    public sealed partial class CallPage : Page
    {
        // vm
        public CallPageViewModel ViewModel;

        // CallPage
        public CallPage()
        {
            this.InitializeComponent();

            ViewModel = new CallPageViewModel();

        }//CallPage end


        // ButtonBackspace_Click
        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneNumInput.SelectionLength != 0)
            {
                int pos = PhoneNumInput.SelectionStart;
                ViewModel.PhoneNumber = ViewModel.PhoneNumber.Remove(
                    pos, PhoneNumInput.SelectionLength
                    );
                PhoneNumInput.SelectionStart = pos;
            }
            else
            {
                int len = ViewModel.PhoneNumber.Length;
                if (len > 0)
                {
                    ViewModel.PhoneNumber = ViewModel.PhoneNumber.Remove(len - 1);
                    PhoneNumInput.SelectionStart = len - 2;
                }
            }

        }//ButtonBackspace_Click end


        // Input
        private void Input(string digit)
        {
            if (PhoneNumInput.SelectionLength != 0)
            {
                ViewModel.PhoneNumber = ViewModel.PhoneNumber.Remove(
                    PhoneNumInput.SelectionStart, PhoneNumInput.SelectionLength
                    );
                PhoneNumInput.SelectionLength = 0;
                PhoneNumInput.SelectionStart = ViewModel.PhoneNumber.Length - 1;
            }

            ViewModel.PhoneNumber += digit;

        }//Input end

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Input("1");
        }//

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Input("2");
        }//

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Input("3");
        }//

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Input("4");
        }//

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Input("5");
        }//

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            Input("6");
        }//

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            Input("7");
        }//

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            Input("8");
        }//

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            Input("9");
        }//

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            Input("0");
        }//

        private void ButtonStar_Click(object sender, RoutedEventArgs e)
        {
            Input("*");
        }//

        private void ButtonHashtag_Click(object sender, RoutedEventArgs e)
        {
            Input("#");
        }//
    
    }//class end

}//namespace end
