using System;
using System.Data;
using System.Data.SQLite;

using DalLegacy;
using DalLegacy.Time;

namespace ApplicationCoreLegacy.Entities
{
    [
        SPNames
        (
            Add="insert into cell_in_stock (id, modified, article_id, point_of_sale_id, x, y, amount) values (null, @in_modified, @in_articleId, @in_pointOfSaleId, @in_x, @in_y, @in_amount)",
            //Read="select * from cell_in_stock where id = @in_id",
            Update="update cell_in_stock set modified = @in_modified, article_id = @in_articleId, point_of_sale_id = @in_pointOfSaleId, x = @in_x, y = @in_y, amount = @in_amount where id = @in_id",
            Delete="delete from cell_in_stock where id = @in_id",
            DeleteParamName="@in_id",
            NoUseOfName=true
        )
    ]
    public class CellInStock: BizObject<CellInStock>
    {
        DateTime    _modified;
        SkuInStock  _parent;
        string      _x;
        string      _y;
        int         _amount;
        int         _amountOld;

        public CellInStock() { _modified = DateTime.Now; }

        public override void Modify()
        {
            base.Modify();
            _modified = DateTime.Now;
        }


        public static CellInStock Restore(int in_id, DateTime in_modified, SkuInStock in_parent, string in_x, string in_y, int in_amount)
        {
            return Restore(in_id, in_modified, in_parent, in_x, in_y, in_amount, false);
        }
        public static CellInStock Restore(int in_id, DateTime in_modified, SkuInStock in_parent, string in_x, string in_y, int in_amount, bool in_isNew)
        {
            return new CellInStock()
            {
                Id          = in_id,
                _modified   = in_modified,
                _parent     = in_parent,
                _x          = in_x,
                _y          = in_y,
                _amount     = in_amount,
                _amountOld  = in_isNew ? 0 : in_amount
            };
        }
        public static CellInStock Restore(string in_x, string in_y, SkuInStock in_skuInStock)
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *" +
                             " from cell_in_stock" +
                             " where point_of_sale_id = @in_pointOfSaleId" +
                               " and article_id = @in_articleId" +
                               " and x = @in_x" +
                               " and y = @in_y";

            cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_skuInStock.PointOfSale.Id));
            cmd.Parameters.Add(new SQLiteParameter("@in_articleId", in_skuInStock.Article.Id));
            cmd.Parameters.Add(new SQLiteParameter("@in_x", in_x));
            cmd.Parameters.Add(new SQLiteParameter("@in_y", in_y));

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            if (table.Rows.Count == 0)
                return null;
            else if (table.Rows.Count == 1)
            {
                DataRow row = table.Rows[0];
                return CellInStock.Restore((int)(long)row["id"], UnixEpoch.ToDateTime((long)row["modified"]), in_skuInStock, in_x, in_y, (int)(long)row["amount"]);
            }
            else
                throw new ApplicationException("CellInStock.Restore(string in_x, string in_y, SkuInStock in_skuInStock) returned more than 1 row.");
        }

        protected override void FillProps(System.Data.DataRow in_row)
        {
            //base.FillProps(in_row);
            //Article article = Article.Restore((int)(long)in_row["article_id"]);
            //PointOfSale pointOfSale = PointOfSale.Restore((int)(long)in_row["point_of_sale_id"]);
            //_parent = SkuInStock.Restore(article, pointOfSale); // Не понятно, что делать с загрузкой всех ячеек для SkuInStock.
            throw new InvalidOperationException("Restore for CellInStock not supported by standard BizObject<T> means.");
        }
        protected override void FillUpdateParams(CustomSqlCommand in_sp)
        {
            base.FillUpdateParams(in_sp);
            _modified = DateTime.Now;
            in_sp.AddParameter("@in_modified", UnixEpoch.FromDateTime(_modified));
            in_sp.AddParameter("@in_articleId", _parent.Article.Id);
            in_sp.AddParameter("@in_pointOfSaleId", _parent.PointOfSale.Id);
            in_sp.AddParameter("@in_x", _x);
            in_sp.AddParameter("@in_y", _y);
            in_sp.AddParameter("@in_amount", _amount);
        }

        public override void Flush()
        {
            if (_amount == 0 && _amountOld == 0)
                return;

            _modified = DateTime.Now;

            if (_amountOld == 0)
            {
                // insert:

                CustomSqlCommand sp = new AddObjectSql();
                FillUpdateParams(sp);
                sp.Execute();

                Id = ((AddObjectSql)sp).NewId;

                SetStorageConsistency();

                _cache.Add(this);
                _isNew = false;

                _amountOld = _amount;
            }
            else if (_amount == 0)
            {
                // delete:

                CellInStock.Delete(this);
                Id = 0;
                _isNew = true;
                _amountOld = 0;
            }
            else
            {
                // update:

                CustomSqlCommand sp = new UpdateObjectSql(this);
                FillUpdateParams(sp);
                sp.Execute();

                SetStorageConsistency();

                _amountOld = _amount;
            }
        }

        public SkuInStock ParentSkuInStock { get { return _parent; } /*set { _parent = value; }*/ }
        public string X { get { return _x; } /*set { _x = value; }*/ } // TODO: It is possible to "bind" changing parent on set X and Y and so to accelerate SkuInStock.TotalAmount.
        public string Y { get { return _y; } /*set { _y = value; }*/ }
        public int Amount { get { return _amount; } set { Modify(); _amount = value; } }
        public DateTime Modified { get { return _modified; } }
    };
}
