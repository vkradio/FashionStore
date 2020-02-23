namespace FashionStoreWinForms.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_Main));
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
            this.MI_Rep_PerPos = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Rep_Total = new System.Windows.Forms.ToolStripMenuItem();
            this.PAN_Workplace = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.warehouseSelector1 = new FashionStoreWinForms.Widgets.WarehouseSelector();
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
            resources.ApplyResources(this.MNU_Main, "MNU_Main");
            this.MNU_Main.Name = "MNU_Main";
            // 
            // MI_Cards
            // 
            this.MI_Cards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_DressMatrix,
            this.MI_Card_PointOfSale});
            this.MI_Cards.Name = "MI_Cards";
            resources.ApplyResources(this.MI_Cards, "MI_Cards");
            // 
            // MI_DressMatrix
            // 
            this.MI_DressMatrix.Name = "MI_DressMatrix";
            resources.ApplyResources(this.MI_DressMatrix, "MI_DressMatrix");
            this.MI_DressMatrix.Click += new System.EventHandler(this.MI_DressMatrix_Click);
            // 
            // MI_Card_PointOfSale
            // 
            this.MI_Card_PointOfSale.Name = "MI_Card_PointOfSale";
            resources.ApplyResources(this.MI_Card_PointOfSale, "MI_Card_PointOfSale");
            this.MI_Card_PointOfSale.Click += new System.EventHandler(this.MI_Card_PointOfSale_Click);
            // 
            // MI_Settings
            // 
            this.MI_Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Sql,
            this.MI_UserSettings,
            this.MI_Backup});
            this.MI_Settings.Name = "MI_Settings";
            resources.ApplyResources(this.MI_Settings, "MI_Settings");
            // 
            // MI_Sql
            // 
            this.MI_Sql.Name = "MI_Sql";
            resources.ApplyResources(this.MI_Sql, "MI_Sql");
            this.MI_Sql.Click += new System.EventHandler(this.MI_Sql_Click);
            // 
            // MI_UserSettings
            // 
            this.MI_UserSettings.Name = "MI_UserSettings";
            resources.ApplyResources(this.MI_UserSettings, "MI_UserSettings");
            this.MI_UserSettings.Click += new System.EventHandler(this.MI_UserSettings_Click);
            // 
            // MI_Backup
            // 
            this.MI_Backup.Name = "MI_Backup";
            resources.ApplyResources(this.MI_Backup, "MI_Backup");
            this.MI_Backup.Click += new System.EventHandler(this.MI_Backup_Click);
            // 
            // MI_AddSku
            // 
            this.MI_AddSku.Name = "MI_AddSku";
            resources.ApplyResources(this.MI_AddSku, "MI_AddSku");
            this.MI_AddSku.Click += new System.EventHandler(this.MI_AddSku_Click);
            // 
            // MI_SearchSku
            // 
            this.MI_SearchSku.Name = "MI_SearchSku";
            resources.ApplyResources(this.MI_SearchSku, "MI_SearchSku");
            this.MI_SearchSku.Click += new System.EventHandler(this.MI_SearchSku_Click);
            // 
            // MI_SalesJournal
            // 
            this.MI_SalesJournal.Name = "MI_SalesJournal";
            resources.ApplyResources(this.MI_SalesJournal, "MI_SalesJournal");
            this.MI_SalesJournal.Click += new System.EventHandler(this.MI_SalesJournal_Click);
            // 
            // MI_Report
            // 
            this.MI_Report.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Rep_PerPos,
            this.MI_Rep_Total});
            this.MI_Report.Name = "MI_Report";
            resources.ApplyResources(this.MI_Report, "MI_Report");
            // 
            // MI_Rep_PerPos
            // 
            this.MI_Rep_PerPos.Name = "MI_Rep_PerPos";
            resources.ApplyResources(this.MI_Rep_PerPos, "MI_Rep_PerPos");
            this.MI_Rep_PerPos.Click += new System.EventHandler(this.MI_Rep_PerPos_Click);
            // 
            // MI_Rep_Total
            // 
            this.MI_Rep_Total.Name = "MI_Rep_Total";
            resources.ApplyResources(this.MI_Rep_Total, "MI_Rep_Total");
            this.MI_Rep_Total.Click += new System.EventHandler(this.MI_Rep_Total_Click);
            // 
            // PAN_Workplace
            // 
            resources.ApplyResources(this.PAN_Workplace, "PAN_Workplace");
            this.PAN_Workplace.Name = "PAN_Workplace";
            // 
            // elementHost1
            // 
            resources.ApplyResources(this.elementHost1, "elementHost1");
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Child = this.warehouseSelector1;
            // 
            // FRM_Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.PAN_Workplace);
            this.Controls.Add(this.MNU_Main);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MNU_Main;
            this.Name = "FRM_Main";
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
        private System.Windows.Forms.Panel PAN_Workplace;
        private System.Windows.Forms.ToolStripMenuItem MI_AddSku;
        private System.Windows.Forms.ToolStripMenuItem MI_SearchSku;
        private System.Windows.Forms.ToolStripMenuItem MI_SalesJournal;
        private System.Windows.Forms.ToolStripMenuItem MI_Backup;
        private System.Windows.Forms.ToolStripMenuItem MI_Report;
        private System.Windows.Forms.ToolStripMenuItem MI_Rep_PerPos;
        private System.Windows.Forms.ToolStripMenuItem MI_Rep_Total;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Widgets.WarehouseSelector warehouseSelector1;
    }
}

