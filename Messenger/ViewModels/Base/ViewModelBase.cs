using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Messenger.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T property, T value, [CallerMemberName] string name = "")
        {
            if (property == null || !property.Equals(value))
            {
                property = value;
                NotifyPropertyChanged(name);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}