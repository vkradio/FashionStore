using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ApplicationCoreLegacy.Entities;
using DalLegacy;
using FashionStoreWinForms.Properties;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_SelectArticleByPriceOfPurchase : Form
    {
        readonly List<Article> _articles;

        int? _newPrice;

        void PricesList_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void NewPriceButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(T_NewPrice.Text, out int value) || value < 0)
            {
                MessageBox.Show(this, Resources.INVALID_COST_OF_PURCHASE_EXPLAINED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                T_NewPrice.Focus();
                return;
            }

            foreach (Article article in _articles)
                if (article.PriceOfPurchase == value)
                {
                    MessageBox.Show(this, Resources.INVALID_COST_OF_PURCHASE, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    T_NewPrice.Focus();
                    return;
                }

            _newPrice = value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void NewPriceTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NewPriceButton_Click(sender, null);
            }
        }

        public FRM_SelectArticleByPriceOfPurchase()
        {
            InitializeComponent();
        }
        public FRM_SelectArticleByPriceOfPurchase(List<Article> in_articles) : this()
        {
            _articles = in_articles;
            L_Article.Text = in_articles[0].Name;
            foreach (Article article in in_articles)
                LST_Prices.Items.Add(new LiteBizItem(article.Id, article.PriceOfPurchase.ToString()));
            LST_Prices.SelectedIndex = LST_Prices.Items.Count - 1;
        }

        public bool NewPriceEntered { get { return _newPrice.HasValue; } }
        public int NewPrice { get { return _newPrice.Value; } }
        public Article SelectedArticle { get { return Article.Restore(((LiteBizItem)LST_Prices.SelectedItem).Value); } }
    }
}
