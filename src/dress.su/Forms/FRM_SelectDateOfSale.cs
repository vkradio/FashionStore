using System;
using System.Windows.Forms;

using dress.su.Properties;

namespace dress.su.Forms
{
    public partial class FRM_SelectDateOfSale : Form
    {
        const string c_description      = "Будет продано {0} ед. товара \"{1}\" по цене {2} р. за ед.";
        const string c_warnZeroPrice    = "ВНИМАНИЕ! Цена 0, т.е. товар продается бесплатно.";

        public FRM_SelectDateOfSale()
        {
            InitializeComponent();
        }
        public FRM_SelectDateOfSale(string in_articleName, int in_amount, int in_price) : this()
        {
            string description = string.Format(c_description, in_amount, in_articleName, in_price);
            if (in_amount == 0)
                description += Environment.NewLine + c_warnZeroPrice;
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
