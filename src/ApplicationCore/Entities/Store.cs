namespace ApplicationCore.Entities
{
    /// <summary>
    ///  Fashion store
    /// </summary>
    /// <remarks>
    ///  This could be a dress or boot shop, warehouse, or something, which
    ///  can store and/or sell goods.
    /// </remarks>
    public class Store
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Ordinal number in lists, reports, etc.
        /// </summary>
        public int OrdinalNumber { get; set; }

        /// <summary>
        /// Is the store functioning
        /// </summary>
        public bool IsActive { get; set; }
    }
}
