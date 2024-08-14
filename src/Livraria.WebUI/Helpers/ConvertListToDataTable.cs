using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Livraria.WebUI.Helpers
{
    public static class DataTableExtensions
    {
        public static DataTable ConvertListToDataTable<T>(IEnumerable<T> list)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all the properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Define columns
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name);
            }

            // Fill rows
            foreach (var item in list)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}


