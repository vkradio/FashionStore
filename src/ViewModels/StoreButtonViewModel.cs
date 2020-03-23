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
                if (value != storeName)
                {
                    storeName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StoreId
        {
            get => storeId;

            set
            {
                if (value != storeId)
                {
                    storeId = value;
                    OnPropertyChanged();
                }
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
