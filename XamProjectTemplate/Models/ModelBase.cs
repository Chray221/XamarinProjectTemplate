using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamProjectTemplate.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (oldValue == null ? true : !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
