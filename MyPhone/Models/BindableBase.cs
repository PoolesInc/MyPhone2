﻿// BindableBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


// GoodTimeStudio.MyPhone.Models end
namespace GoodTimeStudio.MyPhone.Models
{
    public class BindableBase : INotifyPropertyChanged
    {
        // Property Changed event handler
        public event PropertyChangedEventHandler PropertyChanged = delegate { } ;

        // SetProperty
        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(property ,value))
                return false;

            property = value;
        
            OnPropertyChanged(propertyName);

            return true;
        
        }//SetProperty end

        // Notifies listeners that a property value has changed.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }//OnPropertyChanged end
    }
}
