using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

using DalLegacy;

namespace ApplicationCore.Entities
{
    [
        SPNames
        (
            Add="insert into dress_matrix (id, name, cells_x, cells_y) values (null, @in_name, @in_cellsX, @in_cellsY)",
            Read="select * from dress_matrix where id = @in_id",
            Update="update dress_matrix set name = @in_name, cells_x = @in_cellsX, cells_y = @in_cellsY where id = @in_id",
            Delete="delete from dress_matrix where id = @in_id",
            DeleteParamName="@in_id"
        )
    ]
    public class DressMatrix: BizObject<DressMatrix>
    {
        readonly char[] c_splitterAsCharArr = new char[]{','};
        const string c_splitter = ",";

        string[] _cellsX;
        string[] _cellsY;

        protected override void FillProps(System.Data.DataRow in_row)
        {
            base.FillProps(in_row);
            CellsXAsString = (string)in_row["cells_x"];
            CellsYAsString = (string)in_row["cells_y"];
        }
        protected override void FillUpdateParams(CustomSqlCommand in_sp)
        {
            base.FillUpdateParams(in_sp);
            in_sp.AddParameter("@in_cellsX", CellsXAsString);
            in_sp.AddParameter("@in_cellsY", CellsYAsString);
        }

        public static DataTable ReadAll()
        {
            using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from dress_matrix order by id";
                    cmd.CommandType = CommandType.Text;
                    
                    DataTable table = new DataTable();
                    using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                        a.Fill(table);

                    return table;
                }
            }
        }
        public static List<DressMatrix> RestoreAll()
        {
            List<DressMatrix> result = new List<DressMatrix>();
            foreach (DataRow row in ReadAll().Rows)
                result.Add(DressMatrix.Restore((int)(long)row["Id"]));
            return result;
        }

        public IList<string> CellsX { get { return _cellsX; } }
        public IList<string> CellsY { get { return _cellsY; } }
        public string CellsXAsString { get { return string.Join(c_splitter, _cellsX); } set { _cellsX = value.Split(c_splitterAsCharArr); } }
        public string CellsYAsString { get { return string.Join(c_splitter, _cellsY); } set { _cellsY = value.Split(c_splitterAsCharArr); } }
        public bool IsSingleCell { get { return _cellsX.Length == 1 && _cellsY.Length == 1; } }
    };
}
