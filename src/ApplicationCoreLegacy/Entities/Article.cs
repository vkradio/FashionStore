using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

using DalLegacy;
using DalLegacy.Time;

namespace ApplicationCoreLegacy.Entities
{
    [
        SPNames
        (
            Add="insert into article (id, modified, name, matrix_id, price_of_purchase, price_of_sell) values (null, @in_modified, @in_name, @in_matrixId, @in_priceOfPurchase, @in_priceOfSell)",
            Read="select * from article where id = @in_id",
            Update="update article set modified = @in_modified, name = @in_name, matrix_id = @in_matrixId, price_of_purchase = @in_priceOfPurchase, price_of_sell = @in_priceOfSell where id = @in_id",
            Delete="delete from article where id = @in_id",
            DeleteParamName="@in_id"
        )
    ]
    public class Article: BizObject<Article>
    {
        DateTime    _modified;
        int         _matrixId;
        DressMatrix _matrix;
        int         _priceOfPurchase;
        int         _priceOfSell;

        public Article() { _modified = DateTime.Now; }

        public override void Modify()
        {
            base.Modify();
            _modified = DateTime.Now;
        }

        protected override void FillProps(System.Data.DataRow in_row)
        {
            base.FillProps(in_row);
            _modified = UnixEpoch.ToDateTime((long)in_row["modified"]);
            _matrixId = (int)(long)in_row["matrix_id"];
            _priceOfPurchase = (int)(long)in_row["price_of_purchase"];
            _priceOfSell = (int)(long)in_row["price_of_sell"];
        }
        protected override void FillUpdateParams(CustomSqlCommand in_sp)
        {
            base.FillUpdateParams(in_sp);
            _modified = DateTime.Now;
            in_sp.AddParameter("@in_modified", UnixEpoch.FromDateTime(_modified));
            in_sp.AddParameter("@in_matrixId", _matrixId);
            in_sp.AddParameter("@in_priceOfPurchase", _priceOfPurchase);
            in_sp.AddParameter("@in_priceOfSell", _priceOfSell);
        }

        public static Article Restore(string in_name, int in_priceOfPurchase)
        {
            string name = in_name.ToUpper();
            foreach (Article o in _cache)
                if (o.Name.ToUpper() == name && o.PriceOfPurchase == in_priceOfPurchase)
                    return o;

            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select * from article where name = @in_name and price_of_purchase = @in_priceOfPurchase";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_name", in_name));
            cmd.Parameters.Add(new SQLiteParameter("@in_priceOfPurchase", in_priceOfPurchase));

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            if (table.Rows.Count == 0)
                return null;
            else
            {
                Article o = new Article();
                _cache.Add(o);
                o.FillProps(table.Rows[0]);
                o.SetStorageConsistency();
                return o;
            }
        }
        public static List<Article> RestoreByName(string in_name)
        {
            List<Article> result = new List<Article>();

            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select * from article where name = @in_name order by price_of_purchase";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_name", in_name));

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            foreach (DataRow row in table.Rows)
                result.Add(Article.Restore((int)(long)row["id"]));

            return result;
        }

        public int ContstrainedByCellsInStock()
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select COUNT(id) from cell_in_stock where article_id = @in_articleId";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_articleId", Id));

            return (int)(long)cmd.ExecuteScalar();
        }
        public int ContstrainedByDocSale()
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select COUNT(s.id)" +
                            " from doc_sale s" +
                            " join doc d on d.id = s.id and d.time_cancelled is null" +
                            " where s.art_id = @in_articleId";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_articleId", Id));

            return (int)(long)cmd.ExecuteScalar();
        }

        public DressMatrix Matrix
        {
            get
            {
                if (_matrix == null)
                    _matrix = DressMatrix.Restore(_matrixId);
                return _matrix;
            }
            set
            {
                if (value.Id != _matrixId)
                {
                    Modify();
                    _matrix = value;
                    _matrixId = _matrix.Id;
                }
            }
        }
        public int PriceOfPurchase { get { return _priceOfPurchase; } set { Modify(); _priceOfPurchase = value; } }
        public int PriceOfSell { get { return _priceOfSell; } set { Modify(); _priceOfSell = value; } }
        public DateTime Modified { get { return _modified; } }
    };
}
