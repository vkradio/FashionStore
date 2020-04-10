using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

using DalLegacy;
using DalLegacy.Time;

namespace ApplicationCoreLegacy.Entities
{
    public class SkuInStock
    {
        Article         _article;
        PointOfSale     _pointOfSale;
        CellInStock[,]  _cells;

        SkuInStock() {}

        void SetCells(CellInStock[,] in_cells) { _cells = in_cells; }

        //static int StrToIdx(DressMatrix in_matrix, string in_string, bool in_isX)
        //{
        //    if (in_isX && in_matrix.CellsX.Contains(in_string))
        //        return in_matrix.CellsX.IndexOf(in_string);
        //    else if (!in_isX && in_matrix.CellsY.Contains(in_string))
        //        return in_matrix.CellsY.IndexOf(in_string);
        //    else
        //        throw new IndexOutOfRangeException(string.Format("DressMatrix {0} does not contain {1} index \"{2}\"", in_matrix.Id, in_isX ? "X" : "Y", in_string));
        //}
        //static int XStrToIdx(DressMatrix in_matrix, string in_string)
        //{
        //    return StrToIdx(in_matrix, in_string, true);
        //}
        //static int YStrToIdx(DressMatrix in_matrix, string in_string)
        //{
        //    return StrToIdx(in_matrix, in_string, false);
        //}

        public void Flush()
        {
            foreach (CellInStock cell in _cells)
                cell.Flush();
        }

        public static SkuInStock CreateNew(Article in_article, PointOfSale in_pointOfSale)
        {
            SkuInStock result = new SkuInStock()
            {
                _article = in_article,
                _pointOfSale = in_pointOfSale
            };
            DressMatrix mtx = in_article.Matrix;
            result._cells = new CellInStock[mtx.CellsX.Count, mtx.CellsY.Count];
            for (int x = 0; x < mtx.CellsX.Count; x++)
                for (int y = 0; y < mtx.CellsY.Count; y++)
                    result._cells[x, y] = CellInStock.Restore(0, DateTime.Now, result, mtx.CellsX[x], mtx.CellsY[y], 0);
            return result;
        }
        public static SkuInStock Restore(Article in_article, PointOfSale in_pointOfSale)
        {
            SkuInStock skuInStock = new SkuInStock()
            {
                Article = in_article,
                PointOfSale = in_pointOfSale
            };

            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_articleId", in_article.Id));
            cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_pointOfSale.Id));

            cmd.CommandText = "select *" +
                             " from cell_in_stock" +
                             " where point_of_sale_id = @in_pointOfSaleId" +
                             "   and article_id = @in_articleId";

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            DressMatrix mtx = in_article.Matrix;
            CellInStock[,] cells = new CellInStock[mtx.CellsX.Count, mtx.CellsY.Count];

            foreach (DataRow row in table.Rows)
            {
                int x = mtx.CellsX.IndexOf((string)row["x"]);
                int y = mtx.CellsY.IndexOf((string)row["y"]);
                cells[x, y] = CellInStock.Restore((int)(long)row["id"], UnixEpoch.ToDateTime((long)row["modified"]), skuInStock, (string)row["x"], (string)row["y"], (int)(long)row["amount"]);
            }
            for (int x = 0; x < mtx.CellsX.Count; x++)
                for (int y = 0; y < mtx.CellsY.Count; y++)
                    if (cells[x, y] == null)
                        cells[x, y] = CellInStock.Restore(0, DateTime.Now, skuInStock, mtx.CellsX[x], mtx.CellsY[y], 0);

            skuInStock.SetCells(cells);

            return skuInStock;
        }
        public static DataTable RestoreAllByNameBeginning(int in_pointOfSaleId, string in_nameBeginning)
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select a.id, a.name, a.price_of_purchase, a.price_of_sell, IFNULL(SUM(c.amount), 0) as totalAmount" +
                             " from article a" +
                             "   join cell_in_stock c on c.article_id = a.id" +
                             " where c.point_of_sale_id = @in_pointOfSaleId" +
                             "   and LIKECI(a.name, @in_nameBeginning) = 1" +
                             " group by a.id, a.name, a.price_of_purchase, a.price_of_sell" +
                             " order by a.name, a.price_of_purchase";

            cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_pointOfSaleId));
            cmd.Parameters.Add(new SQLiteParameter("@in_nameBeginning", in_nameBeginning));

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            return table;
        }

        public Article Article { get { return _article; } set { _article = value; } }
        public PointOfSale PointOfSale { get { return _pointOfSale; } set { _pointOfSale = value; } }
        public CellInStock this[string in_x, string in_y]
        {
            get
            {
                DressMatrix mtx = _article.Matrix;
                return _cells[mtx.CellsX.IndexOf(in_x), mtx.CellsY.IndexOf(in_y)];
            }
        }
        public CellInStock this[int in_x, int in_y] { get { return _cells[in_x, in_y]; } }
        public IEnumerable Cells { get { return _cells; } }
        public int TotalAmount
        {
            get
            {
                int totalAmount = 0;
                foreach (CellInStock cell in _cells)
                    totalAmount += cell.Amount;
                return totalAmount;
            }
        }
    };
}
