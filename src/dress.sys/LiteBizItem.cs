using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MainLibrary.database.orm
{
    public class LiteBizItem
    {
        public int Value { get; set; }
        public string Text { get; set; }

        public override string ToString() { return Text; }

        public LiteBizItem() { Value = 0; Text = string.Empty; }
        public LiteBizItem(int in_id, string in_text) { Value = in_id; Text = in_text; }
        public LiteBizItem(DataRow in_row, string in_fieldId, string in_fieldText)
            : this((int)(long)in_row[in_fieldId], (string)in_row[in_fieldText]) {}
        public LiteBizItem(DataRow in_row) : this(in_row, "Id", "Name") {}

        public static List<LiteBizItem> FromTable(DataTable in_table)
        {
            List<LiteBizItem> result = new List<LiteBizItem>(in_table.Rows.Count);
            foreach (DataRow row in in_table.Rows)
            {
                LiteBizItem o = new LiteBizItem(row);
                if (o.Text != "Центр.Рынок") // HACK: в будущем убрать, а фильтрацию делать нормальным образом.
                    result.Add(o);
            }
            return result;
        }
    };

    public static class ComboBoxLightBizItemExtension
    {
        public static void SelectBizItem(this ComboBox in_comboBox, int in_id)
        {
            for (int i = 0; i < in_comboBox.Items.Count; i++)
            {
                LiteBizItem item = in_comboBox.Items[i] as LiteBizItem;
                if (item.Value == in_id)
                {
                    in_comboBox.SelectedIndex = i;
                    break;
                }
            }
        }
    };
}
