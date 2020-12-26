using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    ///  Stock keeping unit
    /// </summary>
    /// <remarks>
    ///  Old Name: Article
    /// </remarks>
    public class Sku
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// SKU name
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Size chart
        /// </summary>
        public SizeChart SizeChart { get; set; } = default!;

        /// <summary>
        /// Purchasing cost
        /// </summary>
        public decimal PurchasingCost { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Time of the last change
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
