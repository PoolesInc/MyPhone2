// BindableBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


// GoodTimeStudio.MyPhone.ViewModels
namespace GoodTimeStudio.MyPhone.ViewModels
{

    // BindableBase class
    public class BindableBase : INotifyPropertyChanged
    {

        // PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged = delegate { } ;

        // SetProperty
        protected bool SetProperty<T>(
            ref T property, T value, 
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(property ,value))
                return false;

            property = value;

            OnPropertyChanged(propertyName);
            
            return true;

        }//SetProperty end


        // OnPropertyChanged
        // Notifies listeners that a property value has changed.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }//OnPropertyChanged end

    }//class end

}//namespace end

