using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ApplicationCore.Entities;
using DalLegacy;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_SelectArticleByPriceOfPurchase : Form
    {
        const string c_errInvalidNewPrice   = "Цена закупки задана неверно. Допустимо целое неотрицательное число.";
        const string c_errDuplicateValue    = "Такая цена уже есть в списке. Введите другую цену или выберите такую цену из списка.";

        int?            _newPrice;
        List<Article>   _articles;

        void LST_Prices_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void B_NewPrice_Click(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(T_NewPrice.Text, out value) || value < 0)
            {
                MessageBox.Show(this, c_errInvalidNewPrice, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                T_NewPrice.Focus();
                return;
            }

            foreach (Article article in _articles)
                if (article.PriceOfPurchase == value)
                {
                    MessageBox.Show(this, c_errInvalidNewPrice, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    T_NewPrice.Focus();
                    return;
                }

            _newPrice = value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void T_NewPrice_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                B_NewPrice_Click(sender, null);
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
