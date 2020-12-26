using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class SizeChart
    {
        public int Id { get; set; }

        public string CellsX { get; set; } = string.Empty;

        public string CellsY { get; set; } = string.Empty;
    }
}
