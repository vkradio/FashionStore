using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class WarehouseSelectorViewModel : PropertyChangeNotifier
    {
        readonly WorkspaceViewModel workspaceViewModel;
        readonly IWarehouseManagementService warehouseManagementService;

        #region Private underlying fields
        ObservableCollection<WarehouseButtonViewModel> selectorButtons;
        WarehouseButtonViewModel selectedWarehouse;
        #endregion

        #region Public properties and actions
        public ObservableCollection<WarehouseButtonViewModel> SelectorButtons
        {
            get => selectorButtons;

            set
            {
                selectorButtons = value;
                OnPropertyChanged(nameof(SelectorButtons));
            }
        }

        public WarehouseButtonViewModel SelectedWarehouse
        {
            get => selectedWarehouse;

            set
            {
                if (workspaceViewModel.IsItAllowedToChangeCurrentWarehouse())
                {
                    selectedWarehouse = value;
                    OnPropertyChanged(nameof(SelectedWarehouse));
                }
            }
        }
        #endregion

        public WarehouseSelectorViewModel(WorkspaceViewModel workspaceViewModel, IWarehouseManagementService warehouseManagementService)
        {
            Guard.Against.Null(workspaceViewModel, nameof(workspaceViewModel));
            Guard.Against.Null(warehouseManagementService, nameof(warehouseManagementService));

            this.workspaceViewModel = workspaceViewModel;
            this.warehouseManagementService = warehouseManagementService;
        }

        public async Task Initialize()
        {
            var warehouses = await warehouseManagementService.GetFunctioningOrderedWarehousesAsync().ConfigureAwait(false);
            var warehouseButtons = warehouses
                .Select(wh => new WarehouseButtonViewModel(wh));

            SelectorButtons = new ObservableCollection<WarehouseButtonViewModel>(warehouseButtons);
            SelectedWarehouse = SelectorButtons.FirstOrDefault();
        }
    }
}
