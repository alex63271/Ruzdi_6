using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ruzdi_6.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string Property = null)
        {
            if (Equals(field, value) || value == null)
            {
                return false;
            }
            else
            {
                field = value;
                OnPropertyChanged(Property);
                return true;
            }
        }
    }
}
