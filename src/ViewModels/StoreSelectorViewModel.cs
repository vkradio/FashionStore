using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class StoreSelectorViewModel : PropertyChangeNotifier
    {
        readonly WorkspaceViewModel workspaceViewModel;
        readonly IStoreManagementService storeManagementService;

        #region Private underlying fields
        ObservableCollection<StoreButtonViewModel> selectorButtons;
        StoreButtonViewModel selectedStore;
        #endregion

        #region Public properties and actions
        public ObservableCollection<StoreButtonViewModel> SelectorButtons
        {
            get => selectorButtons;

            set
            {
                selectorButtons = value;
                OnPropertyChanged(nameof(SelectorButtons));
            }
        }

        public StoreButtonViewModel SelectedStore
        {
            get => selectedStore;

            set
            {
                if (workspaceViewModel.IsItAllowedToChangeCurrentStore())
                {
                    selectedStore = value;
                    OnPropertyChanged(nameof(SelectedStore));
                }
            }
        }
        #endregion

        public StoreSelectorViewModel(WorkspaceViewModel workspaceViewModel, IStoreManagementService storeManagementService)
        {
            Guard.Against.Null(workspaceViewModel, nameof(workspaceViewModel));
            Guard.Against.Null(storeManagementService, nameof(storeManagementService));

            this.workspaceViewModel = workspaceViewModel;
            this.storeManagementService = storeManagementService;
        }

        public async Task Initialize()
        {
            var stores = await storeManagementService.GetFunctioningOrderedStoresAsync().ConfigureAwait(false);
            var storeButtons = stores
                .Select(store => new StoreButtonViewModel(store));

            SelectorButtons = new ObservableCollection<StoreButtonViewModel>(storeButtons);
            var tmp = SelectorButtons.FirstOrDefault();
            SelectedStore = tmp;
        }
    }
}
