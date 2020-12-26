using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    /// <summary>
    ///  Inventory stock of the SKU, per size
    /// </summary>
    /// <remarks>
    ///  Old name: CellInStock
    /// </remarks>
    public class SkuSizeInStock
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public Sku Sku { get; set; } = default!;

        /// <summary>
        /// Store
        /// </summary>
        public Store Store { get; set; } = default!;

        /// <summary>
        /// X coordinate of the Size Chart
        /// </summary>
        public string X { get; set; } = default!;

        /// <summary>
        /// Y coordinate of the Size Chart
        /// </summary>
        public string Y { get; set; } = default!;

        //public int 
    }
}
