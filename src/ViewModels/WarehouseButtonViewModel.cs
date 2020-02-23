using ApplicationCore.Entities;
using Ardalis.GuardClauses;
using MvvmInfrastructure;

namespace ViewModels
{
    public class WarehouseButtonViewModel : PropertyChangeNotifier
    {
        #region Private underlying fields
        string warehouseName;
        int warehouseId;
        #endregion

        #region Public properties and actions
        public string WarehouseName
        {
            get => warehouseName;

            set
            {
                warehouseName = value;
                OnPropertyChanged(nameof(WarehouseName));
            }
        }

        public int WarehouseId
        {
            get => warehouseId;

            set
            {
                warehouseId = value;
                OnPropertyChanged(nameof(WarehouseId));
            }
        }
        #endregion

        public WarehouseButtonViewModel(Warehouse warehouse)
        {
            Guard.Against.Null(warehouse, nameof(warehouse));

            WarehouseName = warehouse.Name;
            WarehouseId = warehouse.WarehouseId;
        }
    }
}
