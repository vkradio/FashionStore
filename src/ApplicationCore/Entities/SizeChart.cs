namespace ApplicationCore.Entities
{
    /// <summary>
    ///  Two-dimensional size chart
    /// </summary>
    /// <remarks>
    ///  The dress SKU can use either both dimensions (ex.: Size + Height of suit) or only
    ///  one of them (ex.: S, M, L, XL). In the latter case, the first size axis (X) will
    ///  be ignored. If SKU has no size (ex.: tie), it will use the one-cell fictitious size
    ///  chart.
    /// </remarks>
    public class SizeChart
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comma-separated names of sizes on the X (first) axis
        /// </summary>
        public string CellsX { get; set; } = string.Empty;

        /// <summary>
        /// Comma-separated names of sizes on the Y (second) axis
        /// </summary>
        public string CellsY { get; set; } = string.Empty;
    }
}
