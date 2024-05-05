using System;
using System.Collections.Generic;

namespace apbdcwiczenia7.Properties.Models
{
    public class WarehouseEndpoints
    {
        private readonly List<Warehouse> warehouseEntries = new List<Warehouse>();

        public string AddWarehouseEntry(Warehouse entry)
        {
            if (entry.IdProduct == 0 || entry.IdWarehouse == 0 || entry.Amount == 0 || entry.CreatedAt == default)
            {
                return "All fields are required.";
            }

            warehouseEntries.Add(entry);
            return "Entry successfully added.";
        }

        public List<Warehouse> GetAllEntries()
        {
            return warehouseEntries;
        }
    }
}