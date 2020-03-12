using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IStoreManagementService
    {
        Task<IEnumerable<Store>> GetFunctioningOrderedStoresAsync();

        Task<Store> GetStore(int storeId);
    }
}
