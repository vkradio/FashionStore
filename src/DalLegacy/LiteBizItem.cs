using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DalLegacy
{
    public class LiteBizItem
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public override string ToString() => Text;

        public LiteBizItem()
        {
            Value = 0;
            Text = string.Empty;
        }

        public LiteBizItem(int id, string text) => (Value, Text) = (id, text);

        public LiteBizItem(DataRow row, string fieldId, string fieldText) : this((int)(long)row[fieldId], (string)row[fieldText]) { }

        public LiteBizItem(DataRow row) : this(row, "Id", "Name") { }

        public static List<LiteBizItem> FromTable(DataTable table) => table
            .Rows
            .Cast<DataRow>()
            .Select(row => new LiteBizItem(row))
            .Where(item => item.Text != "Центр.Рынок") // HACK: Check out this dirty tweak was fixed and then remove it.
            .ToList();
    }
}
