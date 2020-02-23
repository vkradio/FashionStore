using System.ComponentModel;

namespace MvvmInfrastructure
{
    public class PropertyChangeNotifier : INotifyPropertyChanged
    {
        bool busy;

        protected PropertyChangeNotifier parent;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
