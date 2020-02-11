using System.Windows.Forms;

using DalLegacy;

namespace FashionStoreWinForms.Utils
{
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
    }
}
