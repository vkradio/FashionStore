using System;
using System.Data;
using System.Windows.Forms;

using dress.su.domain.Model;
using FashionStoreWinForms.Sys;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_SalesJournal : Form
    {
        enum TimeTemplate
        {
            Today,
            Yesterday,
            CurrentMonth,
            DuringMonth,
            CustomRange
        };

        const string c_captionConfirm = "Подтверждение";
        const string c_captionFault = "Отказ";
        const string c_askWillVoidSale = "Отменить продажу \"{0}\" на сумму {1} р.?";
        const string c_noWayNoRowsSelected = "Не выбрана строка.";

        bool _canUpdateTable = true;

        void RefillTable() { RefillTable(DT_DateBegin.Value.Date, DT_DateEnd.Value.Date, DocSale.ShowRows.All); }
        void RefillTable(DateTime in_dateBegin, DateTime in_dateEnd, DocSale.ShowRows in_paymentTypes)
        {
            Cursor oldCur = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DGV_SalesJournal.DataSource = DocSale.ReadSalesJournal(Registry.CurrentPointOfSale, in_dateBegin, in_dateEnd, in_paymentTypes);
                DocSale.Summary summary = DocSale.GetSummary(Registry.CurrentPointOfSale, in_dateBegin, in_dateEnd, in_paymentTypes);
                L_Summary.Text = string.Format("Продаж: {0}  Сумма: {1}  Прибыль: {2}", summary.Count, summary.Sum, summary.Profit);

                B_VoidSale.Enabled = DGV_SalesJournal.Rows.Count != 0;
            }
            finally
            {
                this.Cursor = oldCur;
            }
        }

        void FRM_SalesJournal_Load(object sender, EventArgs e)
        {
            Text += ": " + Registry.CurrentPointOfSale.Name;
            CB_RangeTemplate.SelectedIndex = (int)TimeTemplate.Today;
            DL_PaymentTypes.SelectedIndex = 0;

            #region Make sure we fit to parent window (in case of too small screen resolution)
            if (Size.Width > Owner.Size.Width)
            {
                Location = new System.Drawing.Point(Owner.Location.X, Location.Y);
                Size = new System.Drawing.Size(Owner.Size.Width, Size.Height);
            }
            if (Size.Height > Owner.Size.Height)
            {
                Location = new System.Drawing.Point(Location.Y, Owner.Location.Y);
                Size = new System.Drawing.Size(Size.Width, Owner.Size.Height);
            }
            #endregion
        }
        void CB_RangeTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            switch ((TimeTemplate)CB_RangeTemplate.SelectedIndex)
            {
                case TimeTemplate.Today:
                    _canUpdateTable = false;
                    DT_DateBegin.Value = today;
                    DT_DateEnd.Value = new DateTime(1900, 1, 1);
                    _canUpdateTable = true;
                    DT_DateEnd.Value = today;
                    break;
                case TimeTemplate.Yesterday:
                    _canUpdateTable = false;
                    DT_DateBegin.Value = today.AddDays(-1);
                    DT_DateEnd.Value = new DateTime(1900, 1, 1);
                    _canUpdateTable = true;
                    DT_DateEnd.Value = today.AddDays(-1);
                    break;
                case TimeTemplate.CurrentMonth:
                    _canUpdateTable = false;
                    DT_DateBegin.Value = new DateTime(today.Year, today.Month, 1);
                    DT_DateEnd.Value = new DateTime(1900, 1, 1);
                    _canUpdateTable = true;
                    DT_DateEnd.Value = today;
                    break;
                case TimeTemplate.DuringMonth:
                    _canUpdateTable = false;
                    DT_DateBegin.Value = today.AddMonths(-1).AddDays(1);
                    DT_DateEnd.Value = new DateTime(1900, 1, 1);
                    _canUpdateTable = true;
                    DT_DateEnd.Value = today;
                    break;
            }
        }
        void DT_Date_ValueChanged(object sender, EventArgs e)
        {
            if (_canUpdateTable)
                RefillTable(DT_DateBegin.Value.Date, DT_DateEnd.Value.Date, (DocSale.ShowRows)DL_PaymentTypes.SelectedIndex);
        }
        void DT_Date_CloseUp(object sender, EventArgs e)
        {
            CB_RangeTemplate.SelectedIndex = (int)TimeTemplate.CustomRange;
        }
        void B_VoidSale_Click(object sender, EventArgs e)
        {
            if (DGV_SalesJournal.Rows.GetRowCount(DataGridViewElementStates.Selected) <= 0)
            {
                MessageBox.Show(this, c_noWayNoRowsSelected, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataGridViewRow row = DGV_SalesJournal.SelectedRows[0];
            int id = (int)(long)row.Cells["COL_Id"].Value;
            string name = (string)row.Cells["COL_ArticleName"].Value;
            int sum = (int)(long)row.Cells["COL_PriceSum"].Value;
            
            if (MessageBox.Show(this, string.Format(c_askWillVoidSale, name, sum), c_captionConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            // TODO: Обернуть в транзакцию.
            DocSale sale = DocSale.Restore(id);
            sale.Doc.TimeCancelled = DateTime.Now;
            SkuInStock skuInStock = SkuInStock.Restore(Article.Restore(sale.ArticleId), PointOfSale.Restore(sale.PointOfSaleId));
            CellInStock cell = CellInStock.Restore(sale.CellX, sale.CellY, skuInStock);
            if (cell == null)
                cell = CellInStock.Restore(0, DateTime.Now, skuInStock, sale.CellX, sale.CellY, sale.UnitCount, true);
            else
                cell.Amount += sale.UnitCount;
            sale.Doc.Flush();
            cell.Flush();

            RefillTable();
        }
        void B_ChangeDate_Click(object sender, EventArgs e)
        {
            if (DGV_SalesJournal.Rows.GetRowCount(DataGridViewElementStates.Selected) <= 0)
            {
                MessageBox.Show(this, c_noWayNoRowsSelected, c_captionFault, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataGridViewRow row = DGV_SalesJournal.SelectedRows[0];
            DateTime oldDate = DateTime.Parse((string)row.Cells["COL_Date"].Value);

            DateTime newDate;
            using (FRM_SelectDateOfSale frmSelectDate = new FRM_SelectDateOfSale("-", -1, -1))
            {
                if (frmSelectDate.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                    return;

                newDate = frmSelectDate.Date;
            }

            if (newDate == oldDate)
                MessageBox.Show(this, "Новая дата совпадает со старой, оставляем все без изменений.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                DocSale sale = DocSale.Restore((int)(long)row.Cells["COL_Id"].Value);
                sale.TimeSold = newDate;
                sale.Flush();
            }
        }

        public FRM_SalesJournal()
        {
            InitializeComponent();
        }
    }
}
