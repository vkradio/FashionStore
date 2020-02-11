using System;
using System.Windows.Forms;

using ApplicationCore.Entities;
using FashionStoreWinForms.Properties;
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

        bool _canUpdateTable = true;

        void RefillTable() { RefillTable(DT_DateBegin.Value.Date, DT_DateEnd.Value.Date, DocSale.ShowRows.All); }
        void RefillTable(DateTime in_dateBegin, DateTime in_dateEnd, DocSale.ShowRows in_paymentTypes)
        {
            Cursor oldCur = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DGV_SalesJournal.DataSource = DocSale.ReadSalesJournal(Registry.CurrentPointOfSale, in_dateBegin, in_dateEnd, in_paymentTypes);

                // Re-order columns manually; I was unable to figure out why they began to be shown disordered after translating UI to English
                DGV_SalesJournal.Columns["COL_Date"].DisplayIndex = 0;
                DGV_SalesJournal.Columns["COL_ArticleName"].DisplayIndex = 1;
                DGV_SalesJournal.Columns["COL_Y"].DisplayIndex = 2;
                DGV_SalesJournal.Columns["COL_X"].DisplayIndex = 3;
                DGV_SalesJournal.Columns["COL_Units"].DisplayIndex = 4;
                DGV_SalesJournal.Columns["COL_PriceOfPurchase"].DisplayIndex = 5;
                DGV_SalesJournal.Columns["COL_PricePlan"].DisplayIndex = 6;
                DGV_SalesJournal.Columns["COL_Price"].DisplayIndex = 7;
                DGV_SalesJournal.Columns["COL_PriceSum"].DisplayIndex = 8;
                DGV_SalesJournal.Columns["COL_Id"].DisplayIndex = 9;

                DocSale.Summary summary = DocSale.GetSummary(Registry.CurrentPointOfSale, in_dateBegin, in_dateEnd, in_paymentTypes);
                L_Summary.Text = string.Format(Resources.SALES_SUMMARY_LABEL, summary.Count, summary.Sum, summary.Profit);

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
                MessageBox.Show(this, Resources.NO_ROW_SELECTED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataGridViewRow row = DGV_SalesJournal.SelectedRows[0];
            int id = (int)(long)row.Cells["COL_Id"].Value;
            string name = (string)row.Cells["COL_ArticleName"].Value;
            int sum = (int)(long)row.Cells["COL_PriceSum"].Value;
            
            if (MessageBox.Show(this, string.Format(Resources.ASK_CANCEL_SALE, name, sum), Resources.CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            // TODO: Implement transaction
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
                MessageBox.Show(this, Resources.NO_ROW_SELECTED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(this, Resources.NEW_DATE_IS_SAME_AS_OLD_ONE_SO_CHANGE_NOTHING, Resources.CANCELATION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
