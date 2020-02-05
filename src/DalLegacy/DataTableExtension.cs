using System.Collections.Generic;
using System.Data;

namespace DalLegacy
{
    /// <summary>
    /// Imitate AsEnumerable on Rows, because it is not supported in .NET Standard 2.0
    /// </summary>
    public static class DataTableExtension
    {
        public static IEnumerable<DataRow> AsEnumerable(this DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
                yield return row;
        }
    }
}
