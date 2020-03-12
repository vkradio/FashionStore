using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IStorePersistentStorage
    {
        IEnumerable<Store> GetOrderedListOfFunctioningStores();
    }
}
