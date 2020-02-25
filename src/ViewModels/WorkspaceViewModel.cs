using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class WorkspaceViewModel : PropertyChangeNotifier
    {
        readonly IDialogService dialogService;
        readonly IWarehouseManagementService warehouseManagementService;
        readonly ILegacyWorkspaceContext legacyWorkspaceContext;

        #region Private underlying fields
        WarehouseSelectorViewModel warehouseSelector;
        Warehouse currentWarehouse;
        #endregion

        #region Public properties and actions
        public WarehouseSelectorViewModel WarehouseSelector
        {
            get => warehouseSelector;

            set
            {
                warehouseSelector = value;
                OnPropertyChanged(nameof(WarehouseSelector));
            }
        }

        public Warehouse CurrentWarehouse
        {
            get => currentWarehouse;

            set
            {
                currentWarehouse = value;
                OnPropertyChanged(nameof(CurrentWarehouse));
            }
        }

        public bool IsItAllowedToChangeCurrentWarehouse()
        {
            return false;
        }
        #endregion

        public WorkspaceViewModel(IDialogService dialogService, IWarehouseManagementService warehouseManagementService, ILegacyWorkspaceContext legacyWorkspaceContext)
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(warehouseManagementService, nameof(warehouseManagementService));
            Guard.Against.Null(legacyWorkspaceContext, nameof(legacyWorkspaceContext));

            this.dialogService = dialogService;
            this.warehouseManagementService = warehouseManagementService;
            this.legacyWorkspaceContext = legacyWorkspaceContext;

            WarehouseSelector = new WarehouseSelectorViewModel(this, this.warehouseManagementService);
            WarehouseSelector.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(WarehouseSelectorViewModel.SelectedWarehouse))
                    CurrentWarehouse = await warehouseManagementService.GetWarehouse(WarehouseSelector.SelectedWarehouse?.WarehouseId ?? -1).ConfigureAwait(false);
            };
        }

        public async Task Initialize() => await WarehouseSelector.Initialize().ConfigureAwait(false);
    }
}
