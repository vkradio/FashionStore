using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmInfrastructure
{
    public class PropertyChangeNotifier : INotifyPropertyChanged
    {
        readonly PropertyChangeNotifier? parent;

        bool busy;

        public PropertyChangeNotifier() { }

        public PropertyChangeNotifier(PropertyChangeNotifier parent) => this.parent = parent;

        public virtual bool Busy
        {
            get => busy;

            set
            {
                busy = value;
                OnPropertyChanged(nameof(Busy));
                if (parent != null)
                    parent.Busy = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
