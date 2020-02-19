using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        public string Name { get; set; }

        public int OrdinalNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
