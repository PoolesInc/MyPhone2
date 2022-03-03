// AboutPage
// About ( Thanks, etc.)

using System;
using System.Collections.Generic;
using System.Diagnostics;
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


// namespace
namespace GoodTimeStudio.MyPhone
{
    // OOBEPage 
    public sealed partial class AboutPage : Page
    {
        // OOBE Page vm
        public AboutPageViewModel ViewModel;

        // OOBEPage
        public AboutPage()
        {
            this.InitializeComponent();

            ViewModel = new AboutPageViewModel();            

        }//AboutPage end 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //DListModel.DeviceScanStart();
        }

      

    }//AboutPage class end

}//namespace end 
