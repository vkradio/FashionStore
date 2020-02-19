using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class WarehouseSelectorButtonViewModel
    {
        readonly WarehouseSelectorViewModel warehouseSelector;

        public WarehouseSelectorButtonViewModel(WarehouseSelectorViewModel warehouseSelector) => this.warehouseSelector = warehouseSelector;

        public string WarehouseName { get; }

        public int OrdinalNumber { get; }

        public bool IsSelected { get; set; }

        public void Click() => warehouseSelector.SelectButton(this);
    }
}
