using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class WarehouseSelectorViewModel : PropertyChangeNotifier
    {
        bool ignoreChecksForUnsavedWorkspaceEntry;

        readonly WorkspaceViewModel workspaceViewModel;
        readonly IDialogService dialogService;
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
                if (ignoreChecksForUnsavedWorkspaceEntry)
                {
                    selectedWarehouse = value;
                    OnPropertyChanged(nameof(SelectedWarehouse));
                }
                else
                {
                    if (selectedWarehouse != value)
                    {
                        if (!workspaceViewModel.IsThereUnsavedEntry() ||
                            dialogService.PresentDialog("Не сохранять?", DialogOptionsEnum.YesNoCancel) == DialogResultEnum.Yes)
                        {
                            selectedWarehouse = value;
                            OnPropertyChanged(nameof(SelectedWarehouse));
                        }
                        else
                        {
                            ignoreChecksForUnsavedWorkspaceEntry = true;
                            SelectedWarehouse = selectedWarehouse;
                            ignoreChecksForUnsavedWorkspaceEntry = false;
                        }
                    }
                }
            }
        }
        #endregion

        public WarehouseSelectorViewModel(WorkspaceViewModel workspaceViewModel, IDialogService dialogService, IWarehouseManagementService warehouseManagementService)
        {
            Guard.Against.Null(workspaceViewModel, nameof(workspaceViewModel));
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(warehouseManagementService, nameof(warehouseManagementService));

            this.workspaceViewModel = workspaceViewModel;
            this.dialogService = dialogService;
            this.warehouseManagementService = warehouseManagementService;
        }

        public async Task Initialize()
        {
            ignoreChecksForUnsavedWorkspaceEntry = true;

            var warehouses = await warehouseManagementService.GetFunctioningOrderedWarehousesAsync().ConfigureAwait(false);
            var warehouseButtons = warehouses
                .Select(wh => new WarehouseButtonViewModel(wh));

            SelectorButtons = new ObservableCollection<WarehouseButtonViewModel>(warehouseButtons);
            SelectedWarehouse = SelectorButtons.FirstOrDefault();

            ignoreChecksForUnsavedWorkspaceEntry = false;
        }
    }
}
