using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IWarehouseManagementService
    {
        Task<IEnumerable<Warehouse>> GetFunctioningOrderedWarehousesAsync();

        Task<Warehouse> GetWarehouse(int warehouseId);
    }
}
