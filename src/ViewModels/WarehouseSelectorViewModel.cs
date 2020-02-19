using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class WarehouseSelectorViewModel
    {
        public WarehouseSelectorViewModel(IEnumerable<Warehouse> orderedWarehouses)
        {
            SelectorButtons = orderedWarehouses
                .Select(w => new WarehouseSelectorButtonViewModel(this))
                .ToList();
        }

        public IEnumerable<WarehouseSelectorButtonViewModel> SelectorButtons { get; }

        public void SelectButton(WarehouseSelectorButtonViewModel button)
        {
            // Defensive: Check out if it exists in the collection
            var existingButton = SelectorButtons
                .Where(b => b == button)
                .SingleOrDefault();
            if (existingButton == null)
                throw new InvalidOperationException($"Argument {nameof(button)} contains button which do not exists in the collection.");

            if (!existingButton.IsSelected)
            {
                // Switch off the old selected button
                SelectorButtons
                    .Where(b => b.IsSelected)
                    .ToList()
                    .ForEach(b => b.IsSelected = false);

                // Switch on the new one
                existingButton.IsSelected = true;
            }
        }
    }
}
