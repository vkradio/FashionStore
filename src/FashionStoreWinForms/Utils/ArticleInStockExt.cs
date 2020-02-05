using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

using DalLegacy;

namespace FashionStoreWinForms.Utils
{
    public class ArticleInStockExt
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int Count { get; set; }
        public int PriceOfPurchase { get; set; }
        public int PriceOfSell { get; set; }

        public static List<ArticleInStockExt> ReadTable(int? in_pointOfSaleId = null, string in_articlePrefix = null)
        {
            List<ArticleInStockExt> result = new List<ArticleInStockExt>();

            using (var conn = ConnectionRegistry.Instance.OpenNewConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;

                string where = in_pointOfSaleId.HasValue ? " where c.point_of_sale_id = @in_pointOfSaleId" : string.Empty;
                if (!string.IsNullOrEmpty(in_articlePrefix))
                {
                    var t = new List<string>();
                    var articles = in_articlePrefix
                        .Split(new char[] { ',' })
                        .Select(s => s.Trim())
                        .ToList();
                    var whereArticles = " (";
                    for (var i = 0; i < articles.Count; i++)
                    {
                        if (i != 0)
                            whereArticles += " or";
                        whereArticles += $" LIKECI(a.name, @in_articlePrefix{i}) = 1";

                        cmd.Parameters.Add(new SQLiteParameter($"@in_articlePrefix{i}", articles[i]));
                    }
                    whereArticles += ")";

                    where += (where != string.Empty ? " and" : " where");
                    where += whereArticles; // " LIKECI(a.name, @in_articlePrefix) = 1";
                    //cmd.Parameters.Add(new SQLiteParameter("@in_articlePrefix", in_articlePrefix));
                }
                cmd.CommandText = "select a.id, a.name, a.price_of_purchase, a.price_of_sell, SUM(c.amount) as cnt" +
                                 " from cell_in_stock c" +
                                   " join article a on a.id = c.article_id" +
                                 where +
                                 " group by a.id, a.name, a.price_of_purchase, a.price_of_sell" +
                                 " order by 2";

                if (in_pointOfSaleId.HasValue)
                    cmd.Parameters.Add(new SQLiteParameter("@in_pointOfSaleId", in_pointOfSaleId.Value));

                using (DataTable table = new DataTable())
                using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                {
                    a.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        ArticleInStockExt record = new ArticleInStockExt()
                        {
                            ArticleId = (int)(long)row["id"],
                            ArticleName = (string)row["name"],
                            Count = (int)(long)row["cnt"],
                            PriceOfPurchase = (int)(long)row["price_of_purchase"],
                            PriceOfSell = (int)(long)row["price_of_sell"]
                        };
                        result.Add(record);
                    }
                }
            }

            return result;
        }
    };
}
