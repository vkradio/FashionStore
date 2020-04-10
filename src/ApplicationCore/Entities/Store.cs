using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OrdinalNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
