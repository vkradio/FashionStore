using System;
using System.Data;
using System.Windows.Forms;

using ApplicationCore.Entities;
using FashionStoreWinForms.Forms;
using FashionStoreWinForms.Properties;
using FashionStoreWinForms.Sys;
using FashionStoreWinForms.Widgets.Net;
using DalLegacy;

namespace FashionStoreWinForms.Widgets.PageViewSku
{
    public partial class ManageSkuMiniPanel : UserControl
    {
        const string c_captionFault         = "Отказ";
        const string c_captionConfirmation  = "Подтверждение";
        const string c_captionMessage       = "Сообщение";
        const string c_errInvalidAmount     = "Указано неверное количество.";
        const string c_errNoAmountInCell    = "Указано слишком большое количество, в ячейке столько нет.";
        const string c_errInvalidUnitPrice  = "Указана неверная цена продажи.";
        const string c_askDeleteSku         = "ВНИМАНИЕ! Действительно удалить весь товар \"{0}\" с закупочной ценой {1}? Также будет удален артикул, если он больше нигде не используется.";
        const string c_askMove              = "Перебросить товар \"{0}\" в количестве {1} ед. на точку {2}?";
        const string c_msgSkuMoved          = "Товар перемещен.";

        readonly string[] c_months = new string[]
        {
            "январь",
            "февраль",
            "март",
            "апрель",
            "май",
            "июнь",
            "июль",
            "август",
            "сентябрь",
            "октябрь",
            "ноябрь",
            "декабрь"
        };

        DataRow     _source;
        SkuInStock  _skuInStock;
        CellInStock _selectedCell;

        void TryUpdateMarginTotal()
        {
            int amount, price;
            if (int.TryParse(T_Amount.Text, out amount) &&
                int.TryParse(T_PriceOfSell.Text, out price))
            {
                T_MarginTotal.Text = (amount * (price - _skuInStock.Article.PriceOfPurchase)).ToString();
            }
            else
            {
                T_MarginTotal.Text = "0";
            }
        }

        void T_Amount_TextChanged(object sender, System.EventArgs e) { TryUpdateMarginTotal(); }
        void T_PriceOfSell_TextChanged(object sender, System.EventArgs e) { TryUpdateMarginTotal(); }

        void B_Net_Click(object sender, System.EventArgs e)
        {
            Cursor oldCur = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                _skuInStock = SkuInStock.Restore(_skuInStock.Article, _skuInStock.PointOfSale); // Это сделано для принудительного пересчитывания содержимого сетки.
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
                    int oldAmount;
                    if (!int.TryParse(T_Amount.Text, out oldAmount) || oldAmount > _selectedCell.Amount)
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
            int amount, unitPrice;

            if (!int.TryParse(T_Amount.Text, out amount) || amount < 1)
            {
                MessageBox.Show(this, c_errInvalidAmount, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (amount > _selectedCell.Amount)
            {
                MessageBox.Show(this, c_errNoAmountInCell, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!int.TryParse(T_PriceOfSell.Text, out unitPrice) || unitPrice < 0)
            {
                MessageBox.Show(this, c_errInvalidUnitPrice, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        if (MessageBox.Show(this, string.Format("Выбрана дата: {0} {1}\nВы уверены?", dateOfSale.Day, month), "Вопрос по дате", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            continue;
                    }

                    Settings.Default.LastDateOfSale = frmDateSelector.Date;
                    Settings.Default.Save();
                    dateNotSelected = false;

                    paymentByCard = frmDateSelector.PaymentByCard;
                }
            }

            // TODO: Придумать механизм транзакций и обернуть в него.
            _selectedCell = _skuInStock[_selectedCell.X, _selectedCell.Y]; // Это сделано из-за того, что при выводе сетки текущая ячейка может отсоединиться от товара на объектном уровне (см. комментарий в пересчитывании товара выше).
            _selectedCell.Amount -= amount;
            _skuInStock.Flush();
            DocSale sale = DocSale.CreateNew(dateOfSale, _skuInStock.Article, _skuInStock.PointOfSale, unitPrice, amount, _selectedCell, paymentByCard);
            sale.Flush();

            ((PanelViewSku)Parent.Parent.Parent).UpdateWOldSearch();
        }
        void B_Delete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, string.Format(c_askDeleteSku, _skuInStock.Article.Name, _skuInStock.Article.PriceOfPurchase), c_captionConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Cursor oldCursor = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Для удаления SkuInStock достаточно удалить всего его ячейки, т.к. в БД нет соответствующего
                // ему понятия, а есть только набор его ячеек.
                // TODO: Обернуть в транзакцию.
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
                        throw new ApplicationException(string.Format("Article.Delete для id {0} вернула: {1}", _skuInStock.Article.Id, err));
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
            int amount;
            if (!int.TryParse(T_Amount.Text, out amount) || amount < 1)
            {
                MessageBox.Show(this, c_errInvalidAmount, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (amount > _selectedCell.Amount)
            {
                MessageBox.Show(this, c_errNoAmountInCell, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PointOfSale pointOfSale = PointOfSale.Restore(((LiteBizItem)CB_PointsOfSale.SelectedItem).Value);
            if (MessageBox.Show(this, string.Format(c_askMove, _skuInStock.Article.Name, amount, pointOfSale.Name), c_captionConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // TODO: Сюда транзакцию.
            SkuInStock skuInDestination = SkuInStock.Restore(_skuInStock.Article, pointOfSale);
            skuInDestination[_selectedCell.X, _selectedCell.Y].Amount += amount;
            skuInDestination.Flush();
            _skuInStock[_selectedCell.X, _selectedCell.Y].Amount -= amount;
            _skuInStock.Flush();
            //_selectedCell.Flush();

            ((PanelViewSku)Parent.Parent.Parent).UpdateWOldSearch();
            MessageBox.Show(this, c_msgSkuMoved, c_captionMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
