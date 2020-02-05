using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

using DalLegacy;

namespace ApplicationCore.Entities
{
    [
        SPNames
        (
            Add="insert into point_of_sale (id, name) values (null, @in_name)",
            Read="select * from point_of_sale where id = @in_id",
            Update="update point_of_sale set name = @in_name where id = @in_id",
            Delete="delete from point_of_sale where id = @in_id",
            DeleteParamName="@in_id"
        )
    ]
    public class PointOfSale: BizObject<PointOfSale>
    {
        int _orderNumber;
        bool _deleted;

        public static DataTable ReadTable(int? in_excludeCurrentId = null, bool in_excludeDeleted = false)
        {
            using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    string where = string.Empty;
                    if (in_excludeCurrentId.HasValue && in_excludeDeleted)
                        where = " where";
                    if (in_excludeCurrentId.HasValue)
                        where += $" \"id\" <> {in_excludeCurrentId.Value}";
                    if (in_excludeDeleted)
                    {
                        if (in_excludeCurrentId.HasValue)
                            where += " and";
                        where += " deleted = 0";
                    }

                    cmd.CommandText = $"select * from point_of_sale{where} order by order_number";
                    cmd.CommandType = CommandType.Text;
                    
                    DataTable table = new DataTable();
                    using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                        a.Fill(table);

                    return table;
                }
            }
        }
        public static IList<PointOfSale> RestoreAll()
        {
            DataTable table = ReadTable();
            List<PointOfSale> result = new List<PointOfSale>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
                result.Add(PointOfSale.Restore((int)(long)row["Id"]));
            return result;
        }

        protected override void FillProps(DataRow in_row)
        {
            base.FillProps(in_row);
            _orderNumber = (int)(long)in_row["order_number"];
            _deleted = (long)in_row["deleted"] == 1;
        }

        public int OrderNumber { get { return _orderNumber; } set { _orderNumber = value; Modify(); } }
        public bool Deleted { get { return _deleted; } set { _deleted = value; Modify(); } }
    };
}
