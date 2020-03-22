using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ApplicationCoreLegacy.Entities;
using DalLegacy;
using FashionStoreWinForms.Forms;
using FashionStoreWinForms.Properties;
using FashionStoreWinForms.Sys;
using FashionStoreWinForms.Utils;
using FashionStoreWinForms.Widgets.Net;

namespace FashionStoreWinForms.Widgets.PageAddSku
{
    public partial class PanelAddSku : UserControl
    {
        readonly string c_errInvalidCostOfPurchase = Resources.INVALID_COST_OF_PURCHASE + " " + Resources.ERR_PART_PRICE;
        readonly string c_errInvalidSellPriceOfSell = Resources.INVALID_SELL_PRICE + " " + Resources.ERR_PART_PRICE;
        readonly string c_errInvalidQty = Resources.INVALID_SKU_QTY_IS_ENTERED + " " + Resources.ERR_PART_CELL_QTY;
        readonly string c_errInvalidCellQty = Resources.INVALID_SKU_QTY_IS_ENTERED_IN_CELL + " " + Resources.ERR_PART_CELL_QTY;

        readonly int _defaultHeight;
        readonly int _defaultWidth;

        int _oldNetTypeIndex = -1;
        bool _modified;
        
        Article     _article;
        SkuInStock  _skuInStock;

        bool TryParsePrices(out int out_priceOfPurchase, out int out_priceOfSell)
        {
            _ = out_priceOfSell = 0;
            if (!int.TryParse(T_PriceOfPurchase.Text, out out_priceOfPurchase) || out_priceOfPurchase < 0)
            {
                T_PriceOfPurchase.Focus();
                MessageBox.Show(this, c_errInvalidCostOfPurchase, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!int.TryParse(T_PriceOfSell.Text, out out_priceOfSell) || out_priceOfSell < 0)
            {
                T_PriceOfSell.Focus();
                MessageBox.Show(this, c_errInvalidSellPriceOfSell, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        bool CreateNewArticle()
        {
            bool success = TryParsePrices(out int priceOfPurchase, out int priceOfSell);
            if (!success)
                return false;

            _article = new Article()
            {
                Name = T_Name.Text.Trim(),
                Matrix = DressMatrix.Restore(((LiteBizItem)CB_NetType.SelectedItem).Value),
                PriceOfPurchase = priceOfPurchase,
                PriceOfSell = priceOfSell
            };
            _article.Flush();
            _skuInStock = SkuInStock.CreateNew(_article, Registry.CurrentPointOfSale);

            return true;
        }
        bool UpdateArticle()
        {
            bool success = TryParsePrices(out int priceOfPurchase, out int priceOfSell);
            if (!success)
                return false;

            _article.PriceOfPurchase = priceOfPurchase;
            _article.PriceOfSell = priceOfSell;
            _article.Flush();

            return true;
        }
        bool SaveCells()
        {
            string err = FormToNet();

            if (err != null)
            {
                MessageBox.Show(this, err, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _skuInStock.Flush();
            return true;
        }
        void NetToForm()
        {
            T_PriceOfPurchase.Text = _article.PriceOfPurchase.ToString();
            T_PriceOfSell.Text = _article.PriceOfSell.ToString();
            CB_NetType.SelectBizItem(_article.Matrix.Id);

            RedrawNet(_skuInStock.Article.Matrix);

            if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
                T_Amount.Text = _skuInStock[0, 0].ToString();
            else
            {
                NetPanel netPan = (NetPanel)PAN_Net.Controls[0];

                for (int i = 0; i < _skuInStock.Article.Matrix.CellsX.Count; i++)
                    for (int j = 0; j < _skuInStock.Article.Matrix.CellsY.Count; j++)
                        netPan[i, j] = _skuInStock[i, j].Amount;
            }
        }
        string TextBoxToCell(TextBox in_textBox, CellInStock in_cell)
        {
            if (in_textBox.Text == string.Empty)
            {
                in_cell.Amount = 0;
                return null;
            }

            if (!int.TryParse(in_textBox.Text, out int value) || value < 0)
                return string.Format(c_errInvalidCellQty, in_cell.X, in_cell.Y);
            else
            {
                in_cell.Amount = value;
                return null;
            }
        }
        string FormToNet()
        {
            string err;

            if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
            {
                err = TextBoxToCell(T_Amount, _skuInStock[0, 0]);
                if (err != null)
                {
                    T_Amount.Focus();
                    return c_errInvalidQty;
                }
            }
            else
            {
                NetPanel netPan = (NetPanel)PAN_Net.Controls[0];

                for (int i = 0; i < _skuInStock.Article.Matrix.CellsX.Count; i++)
                    for (int j = 0; j < _skuInStock.Article.Matrix.CellsY.Count; j++)
                    {
                        TextBox txtBox = netPan.FindCellTextBox(i, j);
                        err = TextBoxToCell(txtBox, _skuInStock[i, j]);
                        if (err != null)
                        {
                            txtBox.Focus();
                            return err;
                        }
                    }
            }

            return null;
        }
        void OnNetValueChange()
        {
            _modified = true;
            T_Amount.Text = ((NetPanel)PAN_Net.Controls[0]).TotalCount.ToString();
        }
        void RedrawNet(DressMatrix in_netType)
        {
            PAN_Net.Controls.Clear();
            T_Amount.Text = string.Empty;
            if (in_netType.CellsX.Count == 1 && in_netType.CellsY.Count == 1)
            {
                T_Amount.ReadOnly = false;
                Width = _defaultWidth;
                Height = _defaultHeight;
            }
            else
            {
                T_Amount.ReadOnly = true;
                NetPanel net = new NetPanel(in_netType) { Parent = PAN_Net };
                Width = Math.Max(_defaultWidth, net.TotalWidth);
                Height = Math.Max(_defaultHeight, net.TotalHeight + PAN_SkuWONet.Height);
                net.Size = new System.Drawing.Size(Math.Min(net.TotalHeight, PAN_Net.Width), PAN_Net.Height);
                if (_defaultWidth > net.TotalWidth)
                    net.Location = new System.Drawing.Point(_defaultWidth / 2 - net.TotalWidth / 2, 0);
                net.ValueChanged += (s, e) => OnNetValueChange();
            }
        }
        void RedrawNet()
        {
            DressMatrix mtx = DressMatrix.Restore(((LiteBizItem)CB_NetType.SelectedItem).Value);
            RedrawNet(mtx);
        }
        void TryUpdateMargin()
        {
            if (int.TryParse(T_PriceOfPurchase.Text, out int priceOfPurchase) &&
                int.TryParse(T_PriceOfSell.Text, out int priceOfSale))
            {
                T_Margin.Text = (priceOfSale - priceOfPurchase).ToString();
            }
        }

        void LU_Search_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalController.SearchSkuAction();
        }
        void PriceOfPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            _modified = true;
            TryUpdateMargin();
        }
        void PriceOfSellTextBox_TextChanged(object sender, EventArgs e)
        {
            _modified = true;
            TryUpdateMargin();
        }
        void NameTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                B_Search_Click(sender, null);
        }
        /// <summary>
        /// Selection of size chart type
        /// <remarks>Enabled only for newly added SKUs</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CB_NetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_NetType.SelectedIndex == _oldNetTypeIndex)
                return;

            RedrawNet();
            _modified = true;

            _oldNetTypeIndex = CB_NetType.SelectedIndex;
        }
        /// <summary>
        /// Search of SKU
        /// <remarks>Form is switching to warehouse stock edit mode.</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Search_Click(object sender, EventArgs e)
        {
            bool canSearch = true;
            
            if (string.IsNullOrWhiteSpace(T_Name.Text))
            {
                MessageBox.Show(this, Resources.NAME_NOT_SET, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                T_Name.Focus();
                return;
            }

            Cursor oldCursor = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                B_Search.Enabled = false;
                Application.DoEvents();

                List<Article> articles = Article.RestoreByName(T_Name.Text.Trim());
                if (articles.Count == 0)
                {
                    _article = null;

                    // Ask whether to create new SKU
                    if (MessageBox.Show(this, Resources.ASK_WHETHER_TO_CREATE_NEW_SKU, Resources.QUESTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _modified = true;

                        T_Name.ReadOnly = true;
                        B_Search.Enabled = false;
                        canSearch = false;
                        PAN_SkuParams.Enabled = true;
                        T_Amount.Focus();
                    }
                    else
                    {
                        T_Name.Focus();
                        return;
                    }
                }
                //else if (articles.Count > 1)
                //{
                //    // Display form for selection SKU by cost of purchase.
                //}
                else
                {
                    using (FRM_SelectArticleByPriceOfPurchase frm = new FRM_SelectArticleByPriceOfPurchase(articles))
                    {
                        if (frm.ShowDialog(this) != DialogResult.OK)
                        {
                            T_Name.Focus();
                            return;
                        }

                        _modified = true;

                        if (frm.NewPriceEntered)
                        {
                            _article = new Article()
                            {
                                Name = articles[0].Name,
                                PriceOfPurchase = frm.NewPrice,
                                Matrix = articles[0].Matrix
                            };
                        }
                        else
                        {
                            _article = frm.SelectedArticle;
                        }

                        // Display a size chart and let user to fill it.
                        T_Name.ReadOnly = true;
                        T_PriceOfPurchase.ReadOnly = true;
                        B_Search.Enabled = false;
                        canSearch = false;
                        B_Reset.Enabled = false;
                        PAN_SkuParams.Enabled = true;

                        CB_NetType.SelectBizItem(_article.Matrix.Id);
                        CB_NetType.Enabled = false;

                        _skuInStock = SkuInStock.Restore(_article, Registry.CurrentPointOfSale);
                        NetToForm();
                        if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
                            T_Amount.Focus();
                        else
                            T_PriceOfPurchase.Focus();
                    }
                }
            }
            finally
            {
                B_Search.Enabled = canSearch;
                this.Cursor = oldCursor;
            }
        }
        /// <summary>
        /// Save entered data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Save_Click(object sender, EventArgs e)
        {
            bool success;
            try
            {
                B_Save.Enabled = false;
                Application.DoEvents();

                if (_article == null || _article.Id == 0)
                    success = CreateNewArticle();
                else
                    success = UpdateArticle();
                if (!success)
                    return;

                if (SaveCells())
                {
                    B_Reset.Enabled = false;
                    _modified = false;

                    MessageBox.Show(this, Resources.SKU_HAS_BEEN_SAVED, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                B_Save.Enabled = true;
            }
        }
        /// <summary>
        /// Reset the form to search mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Resources.ASK_RESET_ALL_ENTERED_VALUES, Resources.QUESTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _article = null;
            _skuInStock = null;
            foreach (Control ctrl in PAN_SkuParams.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Text = string.Empty;
                    continue;
                }
                if (ctrl is ComboBox cb)
                {
                    if (cb.Items.Count != 0)
                        cb.SelectedIndex = 0;
                    continue;
                }
            }

            PAN_SkuParams.Enabled = false;
            T_Name.ReadOnly = false;
            B_Search.Enabled = true;
            T_PriceOfPurchase.ReadOnly = false;

            _modified = false;
        }

        public PanelAddSku()
        {
            InitializeComponent();

            PAN_SkuParams.Enabled = false;

            _defaultWidth = Width;
            _defaultHeight = Height;

            LB_Sku.Text = string.Format(Resources.SKU_IN_STOCK, Registry.CurrentPointOfSale.Name);
            LU_Search.Text = string.Format(Resources.CLICK_TO_VIEW_SELL_MOVE_SKU, Registry.CurrentPointOfSale.Name);

            Width = Math.Max(Math.Max(LB_Sku.Width, LU_Search.Width), Width);

            CB_NetType.Items.AddRange(LiteBizItem.FromTable(DressMatrix.ReadAll()).ToArray<LiteBizItem>());
            if (CB_NetType.Items.Count != 0)
                CB_NetType.SelectedIndex = 0;

            _modified = false;
        }

        public TextBox NameTextBoxExt { get { return T_Name; } }
        public bool Modified { get { return _modified; } }
    }
}
