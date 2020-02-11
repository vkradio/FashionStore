namespace FashionStoreWinForms.Forms
{
    partial class FRM_ReportParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_ReportParams));
            this.T_ArticlePrefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.B_MakeReport = new System.Windows.Forms.Button();
            this.CHK_ShowPriceOfPurchase = new System.Windows.Forms.CheckBox();
            this.CHK_ShowPriceOfSale = new System.Windows.Forms.CheckBox();
            this.CHK_ShowPriceOfStock = new System.Windows.Forms.CheckBox();
            this.CHK_ShowSizes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // T_ArticlePrefix
            // 
            resources.ApplyResources(this.T_ArticlePrefix, "T_ArticlePrefix");
            this.T_ArticlePrefix.Name = "T_ArticlePrefix";
            this.T_ArticlePrefix.TextChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // B_MakeReport
            // 
            resources.ApplyResources(this.B_MakeReport, "B_MakeReport");
            this.B_MakeReport.Name = "B_MakeReport";
            this.B_MakeReport.UseVisualStyleBackColor = true;
            this.B_MakeReport.Click += new System.EventHandler(this.B_MakeReport_Click);
            // 
            // CHK_ShowPriceOfPurchase
            // 
            resources.ApplyResources(this.CHK_ShowPriceOfPurchase, "CHK_ShowPriceOfPurchase");
            this.CHK_ShowPriceOfPurchase.Name = "CHK_ShowPriceOfPurchase";
            this.CHK_ShowPriceOfPurchase.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfPurchase.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowPriceOfSale
            // 
            resources.ApplyResources(this.CHK_ShowPriceOfSale, "CHK_ShowPriceOfSale");
            this.CHK_ShowPriceOfSale.Name = "CHK_ShowPriceOfSale";
            this.CHK_ShowPriceOfSale.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfSale.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowPriceOfStock
            // 
            resources.ApplyResources(this.CHK_ShowPriceOfStock, "CHK_ShowPriceOfStock");
            this.CHK_ShowPriceOfStock.Name = "CHK_ShowPriceOfStock";
            this.CHK_ShowPriceOfStock.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfStock.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowSizes
            // 
            resources.ApplyResources(this.CHK_ShowSizes, "CHK_ShowSizes");
            this.CHK_ShowSizes.Name = "CHK_ShowSizes";
            this.CHK_ShowSizes.UseVisualStyleBackColor = true;
            this.CHK_ShowSizes.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // FRM_ReportParams
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CHK_ShowSizes);
            this.Controls.Add(this.CHK_ShowPriceOfStock);
            this.Controls.Add(this.CHK_ShowPriceOfSale);
            this.Controls.Add(this.CHK_ShowPriceOfPurchase);
            this.Controls.Add(this.B_MakeReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.T_ArticlePrefix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_ReportParams";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FRM_ReportParams_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_ArticlePrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_MakeReport;
        private System.Windows.Forms.CheckBox CHK_ShowPriceOfPurchase;
        private System.Windows.Forms.CheckBox CHK_ShowPriceOfSale;
        private System.Windows.Forms.CheckBox CHK_ShowPriceOfStock;
        private System.Windows.Forms.CheckBox CHK_ShowSizes;
    }
}