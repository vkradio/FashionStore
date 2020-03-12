using ApplicationCore.Entities;
using Ardalis.GuardClauses;
using MvvmInfrastructure;

namespace ViewModels
{
    public class StoreButtonViewModel : PropertyChangeNotifier
    {
        #region Private underlying fields
        string storeName;
        int storeId;
        #endregion

        #region Public properties and actions
        public string StoreName
        {
            get => storeName;

            set
            {
                storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        public int StoreId
        {
            get => storeId;

            set
            {
                storeId = value;
                OnPropertyChanged(nameof(StoreId));
            }
        }
        #endregion

        public StoreButtonViewModel(Store store)
        {
            Guard.Against.Null(store, nameof(store));

            StoreName = store.Name;
            StoreId = store.Id;
        }
    }
}
