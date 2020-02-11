using System;
using System.Windows.Forms;

using FashionStoreWinForms.Properties;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_SelectDateOfSale : Form
    {
        public FRM_SelectDateOfSale()
        {
            InitializeComponent();
        }
        public FRM_SelectDateOfSale(string in_articleName, int in_amount, int in_price) : this()
        {
            string description = string.Format(Resources.WILL_BE_SOLD_DETAILS, in_amount, in_articleName, in_price);
            if (in_amount == 0)
                description += Environment.NewLine + Resources.ATTENTION_ZERO_PRICE;
            L_Desctription.Text = description;

            if (Settings.Default.LastDateOfSale == new DateTime(1900, 1, 1))
            {
                Settings.Default.LastDateOfSale = DateTime.Today;
                Settings.Default.Save();
            }
            DAT_Date.Value = Settings.Default.LastDateOfSale;
        }

        public DateTime Date { get { return DAT_Date.Value.Date; } }
        public bool PaymentByCard { get { return CHK_PaymentByCard.CheckState == CheckState.Checked; } }
    }
}
