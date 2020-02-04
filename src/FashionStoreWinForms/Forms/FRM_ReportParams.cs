using System;
using System.Windows.Forms;

using FashionStoreWinForms.Properties;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_ReportParams : Form
    {
        bool _initState;

        public FRM_ReportParams()
        {
            InitializeComponent();
        }

        public bool MakeReport { get; private set; }
        public string ArticlePrefix { get { return T_ArticlePrefix.Text.Trim(); } }

        void B_MakeReport_Click(object sender, EventArgs e)
        {
            MakeReport = true;
            Close();
        }

        void SettingsChanged(object sender, EventArgs e)
        {
            if (!_initState)
            {
                Settings.Default.LastRepArticlePrefix = T_ArticlePrefix.Text.Trim();
                Settings.Default.LastRepShowPriceOfPurchase = CHK_ShowPriceOfPurchase.Checked;
                Settings.Default.LastRepShowPriceOfSale = CHK_ShowPriceOfSale.Checked;
                Settings.Default.LastRepShowPriceOfStock = CHK_ShowPriceOfStock.Checked;
                Settings.Default.LastRepShowSizes = CHK_ShowSizes.Checked;
                Settings.Default.Save();
            }
        }

        void FRM_ReportParams_Load(object sender, EventArgs e)
        {
            _initState = true;
            T_ArticlePrefix.Text = Settings.Default.LastRepArticlePrefix ?? string.Empty;
            CHK_ShowPriceOfPurchase.Checked = Settings.Default.LastRepShowPriceOfPurchase;
            CHK_ShowPriceOfSale.Checked = Settings.Default.LastRepShowPriceOfSale;
            CHK_ShowPriceOfStock.Checked = Settings.Default.LastRepShowPriceOfStock;
            CHK_ShowSizes.Checked = Settings.Default.LastRepShowSizes;
            _initState = false;
        }
    }
}
