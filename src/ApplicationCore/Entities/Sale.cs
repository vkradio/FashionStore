using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Canceled { get; set; }

        public int SkuId { get; set; }

        public string SkuName { get; set; } = default!;

        public decimal PurchasingCost { get; set; }

        public decimal Price { get; set; }

        public int SoldFromStoreId { get; set; }

        public string SoldFromStoreName { get; set; } = default!;

        public decimal UnitPrice { get; set; }

        public int Units { get; set; }

        public string CellX { get; set; } = default!;

        public string CellY { get; set; } = default!;

        public decimal PayedByCard { get; set; }
    }
}
