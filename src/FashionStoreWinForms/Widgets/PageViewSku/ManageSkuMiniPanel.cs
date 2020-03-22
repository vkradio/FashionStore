using System;
using System.Data;
using System.Windows.Forms;

using ApplicationCoreLegacy.Entities;
using FashionStoreWinForms.Forms;
using FashionStoreWinForms.Properties;
using FashionStoreWinForms.Sys;
using FashionStoreWinForms.Widgets.Net;
using DalLegacy;
using System.Globalization;

namespace FashionStoreWinForms.Widgets.PageViewSku
{
    public partial class ManageSkuMiniPanel : UserControl
    {
        readonly string[] c_months = DateTimeFormatInfo.CurrentInfo.MonthNames;

        readonly DataRow _source;
        SkuInStock _skuInStock;
        CellInStock _selectedCell;

        void TryUpdateMarginTotal()
        {
            if (int.TryParse(T_Amount.Text, out int amount) &&
                int.TryParse(T_PriceOfSell.Text, out int price))
            {
                T_MarginTotal.Text = (amount * (price - _skuInStock.Article.PriceOfPurchase)).ToString();
            }
            else
            {
                T_MarginTotal.Text = "0";
            }
        }

        void AmountTextBox_TextChanged(object sender, System.EventArgs e) { TryUpdateMarginTotal(); }
        void PriceOfSellTextBox_TextChanged(object sender, System.EventArgs e) { TryUpdateMarginTotal(); }

        void B_Net_Click(object sender, System.EventArgs e)
        {
            Cursor oldCur = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                _skuInStock = SkuInStock.Restore(_skuInStock.Article, _skuInStock.PointOfSale); // Force re-calculating of size cells in stock
                using (FRM_CellSelector frmSelector = new FRM_CellSelector(_skuInStock))
                {
                    this.Cursor = oldCur;

                    if (frmSelector.ShowDialog(this) != DialogResult.OK)
                        return;

                    _selectedCell = frmSelector.SelectedCell;
                }

                T_CellX.Text = _selectedCell.X;
                T_CellY.Text = _selectedCell.Y;
                if (T_Amount.Text == string.Empty || T_Amount.Text == "0")
                    T_Amount.Text = "1";
                else
                {
                    if (!int.TryParse(T_Amount.Text, out int oldAmount) || oldAmount > _selectedCell.Amount)
                        T_Amount.Text = _selectedCell.Amount.ToString();
                }
            }
            finally
            {
                if (this.Cursor != oldCur)
                    this.Cursor = oldCur;
            }
        }
        void B_Sell_Click(object sender, System.EventArgs e)
        {
            if (!int.TryParse(T_Amount.Text, out int amount) || amount < 1)
            {
                MessageBox.Show(this, Resources.INVALID_SKU_QTY_IS_ENTERED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (amount > _selectedCell.Amount)
            {
                MessageBox.Show(this, Resources.QTY_TOO_BIG_FOR_GIVEN_CELL, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!int.TryParse(T_PriceOfSell.Text, out int unitPrice) || unitPrice < 0)
            {
                MessageBox.Show(this, Resources.INVALID_UNIT_PRICE_OF_SELL, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DateTime dateOfSale = DateTime.Today;
            bool dateNotSelected = true;
            bool paymentByCard = false;
            while (dateNotSelected)
            {
                using (FRM_SelectDateOfSale frmDateSelector = new FRM_SelectDateOfSale(_skuInStock.Article.Name, amount, unitPrice))
                {
                    if (frmDateSelector.ShowDialog(this) != DialogResult.OK)
                        return;
                    dateOfSale = frmDateSelector.Date;

                    DateTime maxOldDate = DateTime.Today.AddDays(-19);
                    if (maxOldDate.CompareTo(dateOfSale) > 0)
                    {
                        string month = c_months[dateOfSale.Month - 1].ToUpper();
                        if (MessageBox.Show(this, string.Format(Resources.CONFIRM_SELECTED_DATE, dateOfSale.Day, month), Resources.QUESTION_ABOUT_DATE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            continue;
                    }

                    Settings.Default.LastDateOfSale = frmDateSelector.Date;
                    Settings.Default.Save();
                    dateNotSelected = false;

                    paymentByCard = frmDateSelector.PaymentByCard;
                }
            }

            // TODO: Implement transaction
            _selectedCell = _skuInStock[_selectedCell.X, _selectedCell.Y]; // This is because of whilst render of the size chart, it's current cell can detach itself from the SKU on the object level (see comment about re-calculating above)
            _selectedCell.Amount -= amount;
            _skuInStock.Flush();
            DocSale sale = DocSale.CreateNew(dateOfSale, _skuInStock.Article, _skuInStock.PointOfSale, unitPrice, amount, _selectedCell, paymentByCard);
            sale.Flush();

            ((PanelViewSku)Parent.Parent.Parent).UpdateWOldSearch();
        }
        void B_Delete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, string.Format(Resources.ASK_DELETE_SKU, _skuInStock.Article.Name, _skuInStock.Article.PriceOfPurchase), Resources.CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Cursor oldCursor = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // To delete SkuInStock it is enough to delete all it's cells, because there is no corresponding
                // table in database, only a set of cells.
                // TODO: Wrap into transaction
                foreach (CellInStock cell in _skuInStock.Cells)
                {
                    cell.Amount = 0;
                    cell.Flush();
                }
                if (_skuInStock.Article.ContstrainedByCellsInStock() == 0 &&
                    _skuInStock.Article.ContstrainedByDocSale() == 0)
                {
                    string err = Article.Delete(_skuInStock.Article);
                    if (err != null)
                        throw new ApplicationException(string.Format("Article.Delete for id {0} returned: {1}", _skuInStock.Article.Id, err));
                }

            }
            finally
            {
                this.Cursor = oldCursor;
            }

            ((PanelViewSku)Parent.Parent.Parent).UpdateWOldSearch();
        }
        void B_Move_Click(object sender, System.EventArgs e)
        {
            if (!int.TryParse(T_Amount.Text, out int amount) || amount < 1)
            {
                MessageBox.Show(this, Resources.INVALID_SKU_QTY_IS_ENTERED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (amount > _selectedCell.Amount)
            {
                MessageBox.Show(this, Resources.QTY_TOO_BIG_FOR_GIVEN_CELL, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PointOfSale pointOfSale = PointOfSale.Restore(((LiteBizItem)CB_PointsOfSale.SelectedItem).Value);
            if (MessageBox.Show(this, string.Format(Resources.CONFIRM_MOVING_GOODS, _skuInStock.Article.Name, amount, pointOfSale.Name), Resources.CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // TODO: Implement transaction
            SkuInStock skuInDestination = SkuInStock.Restore(_skuInStock.Article, pointOfSale);
            skuInDestination[_selectedCell.X, _selectedCell.Y].Amount += amount;
            skuInDestination.Flush();
            _skuInStock[_selectedCell.X, _selectedCell.Y].Amount -= amount;
            _skuInStock.Flush();
            //_selectedCell.Flush();

            ((PanelViewSku)Parent.Parent.Parent).UpdateWOldSearch();
            MessageBox.Show(this, Resources.GOODS_HAS_BEEN_MOVED, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public ManageSkuMiniPanel()
        {
            InitializeComponent();
        }
        public ManageSkuMiniPanel(DataRow in_source, Control in_parent, DataTable in_pointsOfSale) : this()
        {
            _source = in_source;
            _skuInStock = SkuInStock.Restore(Article.Restore((int)(long)_source["id"]), Registry.CurrentPointOfSale);
            Parent = in_parent;

            T_Name.Text             = _skuInStock.Article.Name;
            T_PriceOfSell.Text      = _skuInStock.Article.PriceOfSell.ToString();
            T_InStock.Text          = _skuInStock.TotalAmount.ToString();
            T_PriceOfPurchase.Text  = _skuInStock.Article.PriceOfPurchase.ToString();
            T_PriceOfSellPlan.Text  = _skuInStock.Article.PriceOfSell.ToString();
            T_Amount.Text           = "0";

            CB_PointsOfSale.Items.AddRange(LiteBizItem.FromTable(in_pointsOfSale).ToArray());
            if (CB_PointsOfSale.Items.Count != 0)
                CB_PointsOfSale.SelectedIndex = 0;
            else
            {
                CB_PointsOfSale.Enabled = false;
                B_Move.Enabled = false;
            }

            if (_skuInStock.Article.Matrix.IsSingleCell)
            {
                _selectedCell = _skuInStock[0, 0];

                T_CellX.Visible = false;
                T_CellY.Visible = false;
                B_Net.Visible = false;

                PAN_NoNet.Visible = true;
            }
        }
    }
}
