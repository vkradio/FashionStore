using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ApplicationCore.Entities;
using DalLegacy;
using FashionStoreWinForms.Properties;
using FashionStoreWinForms.Sys;
using FashionStoreWinForms.Utils;
using FashionStoreWinForms.Widgets.PageAddSku;
using FashionStoreWinForms.Widgets.PageViewSku;
using FashionStoreWinForms.Widgets.PointOfSaleSelector;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_Main : Form
    {
        bool AskForSaveIfNeeded()
        {
            if (PAN_Workplace.Controls.Count != 0)
            {
                PanelAddSku panAddSku = PAN_Workplace.Controls[0] as PanelAddSku;
                if (panAddSku != null)
                {
                    if (panAddSku.Modified)
                    {
                        if (MessageBox.Show(this, Resources.ASK_PROCEED_AND_ABANDON_FORM_DATA, Resources.QUESTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                            return false;
                    }
                }
            }

            return true;
        }

        void FRM_Main_Load(object sender, EventArgs e)
        {
            Text = Program.GetVersionString();

            // TODO: Legacy ConnectionString used; delete it sometime
            if (Settings.Default.DbPath == string.Empty)
            {
                if (Settings.Default.ConnectionString == string.Empty)
                {
                    MessageBox.Show(this, Resources.PATH_TO_DB_NOT_SET, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string[] connStringParts = Settings.Default.ConnectionString.Split(new char[] { '=' });
                Settings.Default.DbPath = connStringParts[1];
                Settings.Default.Save();
            }
            ConnectionRegistry.Init("Data Source=" + Settings.Default.DbPath);
            GlobalController.Init(this);

            IList<PointOfSale> poses = PointOfSale.RestoreAll();
            foreach (PointOfSale pos in poses)
            {
                if (!pos.Deleted)
                    PAN_PointsOfSale.PointsOfSale.Add(pos);
            }
            PAN_PointsOfSale.UpdatePointsOfSale();
            if (PAN_PointsOfSale.Controls.Count > 0)
            {
                PushButtonCheap firstButton = (PushButtonCheap)PAN_PointsOfSale.Controls[0];
                Registry.CurrentPointOfSale = firstButton.PointOfSale;
                firstButton.Active = true;
            }
        }
        void MI_Card_PointOfSale_Click(object sender, EventArgs e)
        {
            using (FRM_Card_PointOfSale frm = new FRM_Card_PointOfSale())
                frm.ShowDialog(this);
        }
        void MI_DressMatrix_Click(object sender, EventArgs e)
        {
            using (FRM_DressMatrix frm = new FRM_DressMatrix())
                frm.ShowDialog(this);
        }
        void MI_Sql_Click(object sender, EventArgs e)
        {
            using (FRM_Sql frm = new FRM_Sql())
                frm.ShowDialog(this);
        }
        void MI_UserSettings_Click(object sender, EventArgs e)
        {
            using (FRM_UserSettings frm = new FRM_UserSettings())
                frm.ShowDialog(this);
        }
        void PAN_PointsOfSale_PointOfSaleChanged(object in_sender, PointOfSalePanel.PointOfSaleEventArgs in_ea)
        {
            PointOfSale oldPointOfSale = Registry.CurrentPointOfSale;
            Registry.CurrentPointOfSale = in_ea.PointOfSale;
            if (oldPointOfSale.Id != Registry.CurrentPointOfSale.Id)
                PAN_Workplace.Controls.Clear();
        }
        void PAN_PointsOfSale_BeforePointOfSaleChanged(object sender, PointOfSalePanel.BeforePointOfSaleChangedEventArgs e)
        {
            e.AllowChange = AskForSaveIfNeeded();
        }
        void MI_AddSku_Click(object sender, EventArgs e) { AddSku(); }
        void MI_SearchSku_Click(object sender, EventArgs e) { SearchSku(); }
        void MI_SalesJournal_Click(object sender, EventArgs e)
        {
            PAN_Workplace.Controls.Clear();

            using (FRM_SalesJournal frmSalesJournal = new FRM_SalesJournal())
                frmSalesJournal.ShowDialog(this);
        }
        void MI_Backup_Click(object sender, EventArgs e)
        {
            // 1. Checking out is there a path to backup folder (indirect sign of attached Flash stick)
            if (Settings.Default.BackupFolder == string.Empty)
            {
                MessageBox.Show(this, Resources.BACKUP_FOLDER_NOT_SET, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DirectoryInfo di = new DirectoryInfo(Settings.Default.BackupFolder);
            if (!di.Exists)
            {
                MessageBox.Show(this, Resources.BACKUP_FOLDER_NOT_EXISTS, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor oldCur = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // 2. Read the last backup number of current date, if any, and increment by 1. Else current number is 1.
                int maxNumber = 0;
                DateTime today = DateTime.Today;
                foreach (FileInfo fi in di.GetFiles())
                {
                    BackupParams fileParams;
                    if (!BackupParams.TryParse(fi.Name, out fileParams) || fileParams.Date != today)
                        continue;
                    if (fileParams.Number > maxNumber)
                        maxNumber = fileParams.Number;
                }
                int newNumber = maxNumber + 1;

                // 3. Create backup archive
                // TODO: Currently simply copying database file; in future use a Backup API: http://www.sqlite.org/backup.html
                // TODO: Add 7zip compression or something similar
                File.Copy(Settings.Default.DbPath, Path.Combine(Settings.Default.BackupFolder, string.Format("{0:0000}{1:00}{2:00}{3:00}.db3", today.Year, today.Month, today.Day, newNumber)));
            }
            finally
            {
                this.Cursor = oldCur;
            }

            // 4. Message to user about backup completion
            MessageBox.Show(this, Resources.BACKUP_COMPLETED, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public FRM_Main()
        {
            InitializeComponent();
        }

        public void AddSku()
        {
            if (Registry.CurrentPointOfSale == null)
            {
                MessageBox.Show(this, Resources.CURRENT_WAREHOUSE_NOT_SELECTED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!AskForSaveIfNeeded())
                return;

            PAN_Workplace.Controls.Clear();
            PanelAddSku panAddSku = new PanelAddSku();
            PAN_Workplace.Controls.Add(panAddSku);
            panAddSku.T_NameExt.Focus();
        }
        public void SearchSku()
        {
            if (Registry.CurrentPointOfSale == null)
            {
                MessageBox.Show(this, Resources.CURRENT_WAREHOUSE_NOT_SELECTED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!AskForSaveIfNeeded())
                return;

            PAN_Workplace.Controls.Clear();
            PanelViewSku panViewSku = new PanelViewSku();
            panViewSku.Parent = PAN_Workplace;
            panViewSku.Dock = DockStyle.Fill;
            panViewSku.T_NamePartExt.Focus();
        }

        void MakeReport(PointOfSale in_pos = null)
        {
            string articlePrefix = null;
            bool
                showPriceOfPurchase = true,
                showPriceOfSale = true,
                showPriceOfStock = true,
                showSizes = false;

            if (in_pos != null)
            {
                using (FRM_ReportParams frmParams = new FRM_ReportParams())
                {
                    frmParams.ShowDialog(this);
                    if (!frmParams.MakeReport)
                        return;
                    articlePrefix = frmParams.ArticlePrefix != string.Empty ? frmParams.ArticlePrefix : null;
                    showPriceOfPurchase = Settings.Default.LastRepShowPriceOfPurchase;
                    showPriceOfSale = Settings.Default.LastRepShowPriceOfSale;
                    showPriceOfStock = Settings.Default.LastRepShowPriceOfStock;
                    showSizes = Settings.Default.LastRepShowSizes;
                }
            }

            List<string> textLines = new List<string>();
            string firstLine = $"{Resources.SKU};{Resources.QTY_SHORT}";
            if (showPriceOfPurchase)
                firstLine += $";{Resources.COST_OF_PURCHASE_SHORT}";
            if (showPriceOfSale)
                firstLine += $";{Resources.SELL_PRICE_SHORT}";
            if (showPriceOfStock)
                firstLine += $";{Resources.PRICE_OF_STOCKS_SHORT}";
            if (showSizes && in_pos != null)
                firstLine += $";{Resources.SIZES}";
            textLines.Add(firstLine);

            List<ArticleInStockExt> records = ArticleInStockExt.ReadTable(in_pos != null ? (int?)in_pos.Id : null, articlePrefix);
            int
                totalPriceOfPurchase = 0,
                totalCount = 0;
            foreach (ArticleInStockExt row in records)
            {
                int priceOfPurchaseInStock = row.Count * row.PriceOfPurchase;
                totalPriceOfPurchase += priceOfPurchaseInStock;
                totalCount += row.Count;
                string line = string.Format("\"{0}\";{1}", row.ArticleName.Replace("\"", "\"\""), row.Count);
                if (showPriceOfPurchase)
                    line += ";" + row.PriceOfPurchase.ToString();
                if (showPriceOfSale)
                    line += ";" + row.PriceOfSell.ToString();
                if (showPriceOfStock)
                    line += ";" + priceOfPurchaseInStock.ToString();
                if (showSizes && in_pos != null)
                {
                    Article article = Article.Restore(row.ArticleId);
                    if (!article.Matrix.IsSingleCell)
                    {
                        string sizeCounts = string.Empty;
                        SkuInStock stock = SkuInStock.Restore(article, in_pos);
                        for (int i = 0; i < article.Matrix.CellsY.Count; i++)
                        {
                            for (int j = 0; j < article.Matrix.CellsX.Count; j++)
                            {
                                CellInStock cell = stock[j, i];
                                if (cell.Amount == 0)
                                    continue;
                                string sizeCount = string.Empty;
                                if (!string.IsNullOrEmpty(cell.Y))
                                    sizeCount = cell.Y;
                                if (article.Matrix.CellsX.Count != 1 && !string.IsNullOrEmpty(cell.X))
                                {
                                    if (sizeCount != string.Empty)
                                        sizeCount += " ";
                                    sizeCount += cell.X;
                                }
                                sizeCount += ": " + cell.Amount.ToString();
                                if (sizeCounts != string.Empty)
                                    sizeCounts += ", ";
                                sizeCounts += sizeCount;
                            }
                        }
                        line += ";" + sizeCounts;
                    }
                }
                textLines.Add(line);
            }

            string totalsLine = $"\"{Resources.TOTALS_CAP} (" + (in_pos != null ? in_pos.Name : Resources.ALL_WAREHOUSES_SHORT) + "):\";" + totalCount.ToString();
            if (showPriceOfPurchase)
                totalsLine += ";";
            if (showPriceOfSale)
                totalsLine += ";";
            if (showPriceOfStock)
                totalsLine += ";" + totalPriceOfPurchase.ToString();
            if (showSizes && in_pos != null)
                totalsLine += ";";
            textLines.Add(totalsLine);

            // Saving file
            // WARNING: We use warehouse name as in database; it can contain characters invalid for using as file name!
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), (in_pos != null ? in_pos.Name : Resources.TOTAL) + ".csv");
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    MessageBox.Show(this, Resources.UNABLE_TO_CREATE_REPORT_LONG, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            using (StreamWriter file = new StreamWriter(filePath, false, Encoding.Default))
            {
                foreach (string str in textLines)
                {
                    file.WriteLine(str);
                }
            }

            // Open file
            System.Diagnostics.Process.Start(filePath);
        }

        void MI_Rep_PerPos_Click(object sender, EventArgs e)
        {
            if (Registry.CurrentPointOfSale == null)
            {
                MessageBox.Show(this, Resources.CURRENT_WAREHOUSE_NOT_SELECTED, Resources.FAILURE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MakeReport(Registry.CurrentPointOfSale);
        }

        void MI_Rep_Total_Click(object sender, EventArgs e)
        {
            MakeReport();
        }
    }
}
