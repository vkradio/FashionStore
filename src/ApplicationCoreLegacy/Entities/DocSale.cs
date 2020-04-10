using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

using DalLegacy;
using DalLegacy.Time;

namespace ApplicationCoreLegacy.Entities
{
    [
        SPNames
        (
            Add="insert into doc_sale" +
               " (" +
                 " id," +
                 " time_sold," +
                 " art_id," +
                 " art_name," +
                 " art_price_of_purchase," +
                 " art_price_of_sell," +
                 " point_of_sale_id," +
                 " point_of_sale_name," +
                 " unit_price," +
                 " unit_count," +
                 " cell_x," +
                 " cell_y," +
                 " payment_by_card" +
               " )" +
               " values" +
               " (" +
                 " @in_docId," +
                 " @in_timeSold," +
                 " @in_articleId," +
                 " @in_articleName," +
                 " @in_articlePriceOfPurchase," +
                 " @in_articlePriceOfSell," +
                 " @in_pointOfSaleId," +
                 " @in_pointOfSaleName," +
                 " @in_unitPrice," +
                 " @in_unitCount," +
                 " @in_cellX," +
                 " @in_cellY," +
                 " @in_paymentByCard" +
               " )",
            Read="select * from doc_sale where id = @in_id",
            //Update="update doc_sale set \"time_cancelled\" = @in_timeCancelled where id = @in_id",
            //Delete="delete from doc_sale where id = @in_id",
            //DeleteParamName="@in_id",
            NoUseOfName=true
        )
    ]
    public class DocSale: BizObject<DocSale>
    {
        public struct Summary
        {
            public int Count;
            public int Sum;
            public int Profit;
        };
        /// <summary>
        /// Режим отображения строк журнала продаж
        /// </summary>
        public enum ShowRows
        {
            /// <summary>
            /// Показывать все продажи
            /// </summary>
            All,
            /// <summary>
            /// Показывать только продажи за наличный расчет
            /// </summary>
            OnlyCash,
            /// <summary>
            /// Показывать только продажи, оплаченные банковской картой
            /// </summary>
            OnlyCard
        };

        Doc         _doc;
        DateTime    _timeSold;
	    int         _articleId;
	    string      _articleName;
	    int         _articlePriceOfPurchase;
	    int         _articlePriceOfSell;
	    int         _pointOfSaleId;
	    string      _pointOfSaleName;
	    int         _unitPrice;
	    int         _unitCount;
	    string      _cellX;
	    string      _cellY;
        bool        _paymentByCard;

        protected override void FillProps(System.Data.DataRow in_row)
        {
            base.FillProps(in_row);
            _doc = Doc.Restore(Id);
            _timeSold = UnixEpoch.ToDateTime((long)in_row["time_sold"]);
            _articleId = (int)(long)in_row["art_id"];
            _articleName = (string)in_row["art_name"];
            _articlePriceOfPurchase = (int)(long)in_row["art_price_of_purchase"];
            _articlePriceOfSell = (int)(long)in_row["art_price_of_sell"];
            _pointOfSaleId = (int)(long)in_row["point_of_sale_id"];
            _pointOfSaleName = (string)in_row["point_of_sale_name"];
            _unitPrice = (int)(long)in_row["unit_price"];
            _unitCount = (int)(long)in_row["unit_count"];
            _cellX = (string)in_row["cell_x"];
            _cellY = (string)in_row["cell_y"];
            _paymentByCard = (long)in_row["payment_by_card"] == 1;
        }
        protected override void FillUpdateParams(CustomSqlCommand in_sp)
        {
            base.FillUpdateParams(in_sp);
            Id = _doc.Id;
            in_sp.AddParameter("@in_docId", Id);
            in_sp.AddParameter("@in_timeSold", UnixEpoch.FromDateTime(_timeSold));
            in_sp.AddParameter("@in_articleId", _articleId);
            in_sp.AddParameter("@in_articleName", _articleName);
            in_sp.AddParameter("@in_articlePriceOfPurchase", _articlePriceOfPurchase);
            in_sp.AddParameter("@in_articlePriceOfSell", _articlePriceOfSell);
            in_sp.AddParameter("@in_pointOfSaleId", _pointOfSaleId);
            in_sp.AddParameter("@in_pointOfSaleName", _pointOfSaleName);
            in_sp.AddParameter("@in_unitPrice", _unitPrice);
            in_sp.AddParameter("@in_unitCount", _unitCount);
            in_sp.AddParameter("@in_cellX", _cellX);
            in_sp.AddParameter("@in_cellY", _cellY);
            in_sp.AddParameter("@in_paymentByCard", _paymentByCard ? 1 : 0);
        }

        // TODO: Implement changing journal record.
        public override void Flush()
        {
            Debug.Assert(Validate() == null, "Debug.Assert(Validate() == null)");

            _isNew = _doc == null;

            if (!_isNew)
                throw new InvalidOperationException("Cannot update DocSale - operation not allowed.");

            // TODO: Parent doc should be created in transaction.
            _doc = Doc.CreateNew(Doc.DocType.Sale);
            _doc.Flush();
            Id = _doc.Id;

            CustomSqlCommand sp = new AddObjectSql();

            FillUpdateParams(sp);

            sp.Execute();

            SetStorageConsistency();

            _cache.Add(this);
            _isNew = false;
        }

        public static DocSale CreateNew(DateTime in_timeSold, Article in_article, PointOfSale in_pointOfSale, int in_unitPrice, int in_unitCount, CellInStock in_cell, bool in_paymentByCard) =>
            new DocSale
            {
                _timeSold = in_timeSold,
                _articleId = in_article.Id,
                _articleName = in_article.Name,
                _articlePriceOfPurchase = in_article.PriceOfPurchase,
                _articlePriceOfSell = in_article.PriceOfSell,
                _pointOfSaleId = in_pointOfSale.Id,
                _pointOfSaleName = in_pointOfSale.Name,
                _unitPrice = in_unitPrice,
                _unitCount = in_unitCount,
                _cellX = in_cell.X,
                _cellY = in_cell.Y,
                _paymentByCard = in_paymentByCard
            };
        public static DataTable ReadSalesJournal(PointOfSale in_pointOfSale, DateTime in_dateBegin, DateTime in_dateEnd, ShowRows in_displayMode)
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_pointOfSale.Id));
            cmd.Parameters.Add(new SQLiteParameter("@in_dateBegin", UnixEpoch.FromDateTime(in_dateBegin)));
            cmd.Parameters.Add(new SQLiteParameter("@in_dateEndNext", UnixEpoch.FromDateTime(in_dateEnd.AddDays(1))));
            string wherePayedByCard = string.Empty;
            if (in_displayMode == ShowRows.OnlyCash || in_displayMode == ShowRows.OnlyCard)
            {
                cmd.Parameters.Add(new SQLiteParameter("@in_payedByCard", in_displayMode == ShowRows.OnlyCash ? 0 : 1));
                wherePayedByCard = " and s.payment_by_card = @in_payedByCard";
            }

            cmd.CommandText = "select" +
                               " s.id," +
                               " date(s.time_sold, 'unixepoch', 'localtime') as time_sold," +
                               " s.art_name," +
                               " s.art_price_of_purchase," +
                               " s.art_price_of_sell," +
                               " s.unit_price," +
                               " s.unit_count," +
                               " (s.unit_price * s.unit_count) as price_sum," +
                               " s.cell_x," +
                               " s.cell_y" +
                             " from doc_sale s" +
                               " join doc d on d.id = s.id and d.time_cancelled is null" +
                             " where s.point_of_sale_id = @in_pointOfSaleId" +
                               " and @in_dateBegin <= s.time_sold and s.time_sold < @in_dateEndNext" +
                               wherePayedByCard +
                             " order by s.time_sold desc;";

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            return table;
        }
        public static Summary GetSummary(PointOfSale in_pointOfSale, DateTime in_dateBegin, DateTime in_dateEnd, ShowRows in_displayMode)
        {
            using SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection();
            using SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_pointOfSale.Id));
            cmd.Parameters.Add(new SQLiteParameter("@in_dateBegin", UnixEpoch.FromDateTime(in_dateBegin)));
            cmd.Parameters.Add(new SQLiteParameter("@in_dateEndNext", UnixEpoch.FromDateTime(in_dateEnd.AddDays(1))));
            string wherePayedByCard = string.Empty;
            if (in_displayMode == ShowRows.OnlyCash || in_displayMode == ShowRows.OnlyCard)
            {
                cmd.Parameters.Add(new SQLiteParameter("@in_payedByCard", in_displayMode == ShowRows.OnlyCash ? 0 : 1));
                wherePayedByCard = " and s.payment_by_card = @in_payedByCard";
            }

            cmd.CommandText = "select" +
                               " COUNT(s.id) as cnt," +
                               " IFNULL(SUM(s.unit_price * s.unit_count), 0) as sm," +
                               " IFNULL(SUM(s.unit_price * s.unit_count - s.art_price_of_purchase * s.unit_count), 0) as profit" +
                             " from doc_sale s" +
                               " join doc d on d.id = s.id and d.time_cancelled is null" +
                             " where s.point_of_sale_id = @in_pointOfSaleId" +
                               " and @in_dateBegin <= s.time_sold and s.time_sold < @in_dateEndNext" +
                               wherePayedByCard;

            DataTable table = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                a.Fill(table);

            DataRow row = table.Rows[0];
            return new Summary()
            {
                Count = (int)(long)row["cnt"],
                Sum = (int)(long)row["sm"],
                Profit = (int)(long)row["profit"]
            };
        }

        public Doc Doc { get { return _doc; } }
        public DateTime TimeSold { get { return _timeSold; } set { Modify(); _timeSold = value; } }
	    public int ArticleId { get { return _articleId; } }
	    public string ArticleName { get { return _articleName; } }
	    public int ArticlePriceOfPurchase { get { return _articlePriceOfPurchase; } }
	    public int ArticlePriceOfSell { get { return _articlePriceOfSell; } }
	    public int PointOfSaleId { get { return _pointOfSaleId; } }
	    public string PointOfSaleName { get { return _pointOfSaleName; } }
	    public int UnitPrice { get { return _unitPrice; } }
	    public int UnitCount { get { return _unitCount; } }
	    public string CellX { get { return _cellX; } }
	    public string CellY { get { return _cellY; } }
        public bool PaymentByCard { get { return _paymentByCard; } }
    };
}
