namespace dress.su.Forms
{
    partial class FRM_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MNU_Main = new System.Windows.Forms.MenuStrip();
            this.MI_Cards = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_DressMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Card_PointOfSale = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Sql = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_UserSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Backup = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_AddSku = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_SearchSku = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_SalesJournal = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.PAN_Workplace = new System.Windows.Forms.Panel();
            this.PAN_PointsOfSale = new dress.su.Widgets.PointOfSaleSelector.PointOfSalePanel();
            this.MI_Rep_PerPos = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Rep_Total = new System.Windows.Forms.ToolStripMenuItem();
            this.MNU_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // MNU_Main
            // 
            this.MNU_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Cards,
            this.MI_Settings,
            this.MI_AddSku,
            this.MI_SearchSku,
            this.MI_SalesJournal,
            this.MI_Report});
            this.MNU_Main.Location = new System.Drawing.Point(0, 0);
            this.MNU_Main.Name = "MNU_Main";
            this.MNU_Main.Size = new System.Drawing.Size(670, 24);
            this.MNU_Main.TabIndex = 10;
            // 
            // MI_Cards
            // 
            this.MI_Cards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_DressMatrix,
            this.MI_Card_PointOfSale});
            this.MI_Cards.Name = "MI_Cards";
            this.MI_Cards.Size = new System.Drawing.Size(71, 20);
            this.MI_Cards.Text = "Карточки";
            // 
            // MI_DressMatrix
            // 
            this.MI_DressMatrix.Name = "MI_DressMatrix";
            this.MI_DressMatrix.Size = new System.Drawing.Size(152, 22);
            this.MI_DressMatrix.Text = "Тип сетки";
            this.MI_DressMatrix.Click += new System.EventHandler(this.MI_DressMatrix_Click);
            // 
            // MI_Card_PointOfSale
            // 
            this.MI_Card_PointOfSale.Name = "MI_Card_PointOfSale";
            this.MI_Card_PointOfSale.Size = new System.Drawing.Size(152, 22);
            this.MI_Card_PointOfSale.Text = "Точка продаж";
            this.MI_Card_PointOfSale.Click += new System.EventHandler(this.MI_Card_PointOfSale_Click);
            // 
            // MI_Settings
            // 
            this.MI_Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Sql,
            this.MI_UserSettings,
            this.MI_Backup});
            this.MI_Settings.Name = "MI_Settings";
            this.MI_Settings.Size = new System.Drawing.Size(79, 20);
            this.MI_Settings.Text = "Настройки";
            // 
            // MI_Sql
            // 
            this.MI_Sql.Name = "MI_Sql";
            this.MI_Sql.Size = new System.Drawing.Size(237, 22);
            this.MI_Sql.Text = "Команды SQL";
            this.MI_Sql.Click += new System.EventHandler(this.MI_Sql_Click);
            // 
            // MI_UserSettings
            // 
            this.MI_UserSettings.Name = "MI_UserSettings";
            this.MI_UserSettings.Size = new System.Drawing.Size(237, 22);
            this.MI_UserSettings.Text = "Пользовательские настройки";
            this.MI_UserSettings.Click += new System.EventHandler(this.MI_UserSettings_Click);
            // 
            // MI_Backup
            // 
            this.MI_Backup.Name = "MI_Backup";
            this.MI_Backup.Size = new System.Drawing.Size(237, 22);
            this.MI_Backup.Text = "Резервное копирование";
            this.MI_Backup.Click += new System.EventHandler(this.MI_Backup_Click);
            // 
            // MI_AddSku
            // 
            this.MI_AddSku.Name = "MI_AddSku";
            this.MI_AddSku.Size = new System.Drawing.Size(105, 20);
            this.MI_AddSku.Text = "Добавить товар";
            this.MI_AddSku.Click += new System.EventHandler(this.MI_AddSku_Click);
            // 
            // MI_SearchSku
            // 
            this.MI_SearchSku.Name = "MI_SearchSku";
            this.MI_SearchSku.Size = new System.Drawing.Size(54, 20);
            this.MI_SearchSku.Text = "Поиск";
            this.MI_SearchSku.Click += new System.EventHandler(this.MI_SearchSku_Click);
            // 
            // MI_SalesJournal
            // 
            this.MI_SalesJournal.Name = "MI_SalesJournal";
            this.MI_SalesJournal.Size = new System.Drawing.Size(108, 20);
            this.MI_SalesJournal.Text = "Журнал продаж";
            this.MI_SalesJournal.Click += new System.EventHandler(this.MI_SalesJournal_Click);
            // 
            // MI_Report
            // 
            this.MI_Report.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Rep_PerPos,
            this.MI_Rep_Total});
            this.MI_Report.Name = "MI_Report";
            this.MI_Report.Size = new System.Drawing.Size(51, 20);
            this.MI_Report.Text = "Отчет";
            // 
            // PAN_Workplace
            // 
            this.PAN_Workplace.AutoScroll = true;
            this.PAN_Workplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Workplace.Location = new System.Drawing.Point(0, 76);
            this.PAN_Workplace.Name = "PAN_Workplace";
            this.PAN_Workplace.Size = new System.Drawing.Size(670, 377);
            this.PAN_Workplace.TabIndex = 12;
            // 
            // PAN_PointsOfSale
            // 
            this.PAN_PointsOfSale.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_PointsOfSale.Location = new System.Drawing.Point(0, 24);
            this.PAN_PointsOfSale.Name = "PAN_PointsOfSale";
            this.PAN_PointsOfSale.PaddingLeft = 10;
            this.PAN_PointsOfSale.PaddingTop = 10;
            this.PAN_PointsOfSale.Size = new System.Drawing.Size(670, 52);
            this.PAN_PointsOfSale.TabIndex = 11;
            this.PAN_PointsOfSale.PointOfSaleChanged += new System.EventHandler<dress.su.Widgets.PointOfSaleSelector.PointOfSalePanel.PointOfSaleEventArgs>(this.PAN_PointsOfSale_PointOfSaleChanged);
            this.PAN_PointsOfSale.BeforePointOfSaleChanged += new System.EventHandler<dress.su.Widgets.PointOfSaleSelector.PointOfSalePanel.BeforePointOfSaleChangedEventArgs>(this.PAN_PointsOfSale_BeforePointOfSaleChanged);
            // 
            // MI_Rep_PerPos
            // 
            this.MI_Rep_PerPos.Name = "MI_Rep_PerPos";
            this.MI_Rep_PerPos.Size = new System.Drawing.Size(178, 22);
            this.MI_Rep_PerPos.Text = "Остатки по точкам";
            this.MI_Rep_PerPos.Click += new System.EventHandler(this.MI_Rep_PerPos_Click);
            // 
            // MI_Rep_Total
            // 
            this.MI_Rep_Total.Name = "MI_Rep_Total";
            this.MI_Rep_Total.Size = new System.Drawing.Size(178, 22);
            this.MI_Rep_Total.Text = "Остатки всего";
            this.MI_Rep_Total.Click += new System.EventHandler(this.MI_Rep_Total_Click);
            // 
            // FRM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 453);
            this.Controls.Add(this.PAN_Workplace);
            this.Controls.Add(this.PAN_PointsOfSale);
            this.Controls.Add(this.MNU_Main);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MNU_Main;
            this.Name = "FRM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRM_Main_Load);
            this.MNU_Main.ResumeLayout(false);
            this.MNU_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MNU_Main;
        private System.Windows.Forms.ToolStripMenuItem MI_Cards;
        private System.Windows.Forms.ToolStripMenuItem MI_Card_PointOfSale;
        private System.Windows.Forms.ToolStripMenuItem MI_Settings;
        private System.Windows.Forms.ToolStripMenuItem MI_Sql;
        private System.Windows.Forms.ToolStripMenuItem MI_UserSettings;
        private System.Windows.Forms.ToolStripMenuItem MI_DressMatrix;
        private Widgets.PointOfSaleSelector.PointOfSalePanel PAN_PointsOfSale;
        private System.Windows.Forms.Panel PAN_Workplace;
        private System.Windows.Forms.ToolStripMenuItem MI_AddSku;
        private System.Windows.Forms.ToolStripMenuItem MI_SearchSku;
        private System.Windows.Forms.ToolStripMenuItem MI_SalesJournal;
        private System.Windows.Forms.ToolStripMenuItem MI_Backup;
        private System.Windows.Forms.ToolStripMenuItem MI_Report;
        private System.Windows.Forms.ToolStripMenuItem MI_Rep_PerPos;
        private System.Windows.Forms.ToolStripMenuItem MI_Rep_Total;
    }
}

