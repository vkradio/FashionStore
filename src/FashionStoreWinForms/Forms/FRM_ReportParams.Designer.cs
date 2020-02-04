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
            this.T_ArticlePrefix.Location = new System.Drawing.Point(12, 27);
            this.T_ArticlePrefix.Name = "T_ArticlePrefix";
            this.T_ArticlePrefix.Size = new System.Drawing.Size(235, 20);
            this.T_ArticlePrefix.TabIndex = 0;
            this.T_ArticlePrefix.TextChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Первые буквы названия товара:";
            // 
            // B_MakeReport
            // 
            this.B_MakeReport.Location = new System.Drawing.Point(172, 145);
            this.B_MakeReport.Name = "B_MakeReport";
            this.B_MakeReport.Size = new System.Drawing.Size(75, 23);
            this.B_MakeReport.TabIndex = 2;
            this.B_MakeReport.Text = "Отчет";
            this.B_MakeReport.UseVisualStyleBackColor = true;
            this.B_MakeReport.Click += new System.EventHandler(this.B_MakeReport_Click);
            // 
            // CHK_ShowPriceOfPurchase
            // 
            this.CHK_ShowPriceOfPurchase.AutoSize = true;
            this.CHK_ShowPriceOfPurchase.Location = new System.Drawing.Point(12, 53);
            this.CHK_ShowPriceOfPurchase.Name = "CHK_ShowPriceOfPurchase";
            this.CHK_ShowPriceOfPurchase.Size = new System.Drawing.Size(159, 17);
            this.CHK_ShowPriceOfPurchase.TabIndex = 3;
            this.CHK_ShowPriceOfPurchase.Text = "Показывать цену закупки";
            this.CHK_ShowPriceOfPurchase.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfPurchase.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowPriceOfSale
            // 
            this.CHK_ShowPriceOfSale.AutoSize = true;
            this.CHK_ShowPriceOfSale.Location = new System.Drawing.Point(12, 76);
            this.CHK_ShowPriceOfSale.Name = "CHK_ShowPriceOfSale";
            this.CHK_ShowPriceOfSale.Size = new System.Drawing.Size(162, 17);
            this.CHK_ShowPriceOfSale.TabIndex = 4;
            this.CHK_ShowPriceOfSale.Text = "Показывать цену продажи";
            this.CHK_ShowPriceOfSale.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfSale.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowPriceOfStock
            // 
            this.CHK_ShowPriceOfStock.AutoSize = true;
            this.CHK_ShowPriceOfStock.Location = new System.Drawing.Point(12, 99);
            this.CHK_ShowPriceOfStock.Name = "CHK_ShowPriceOfStock";
            this.CHK_ShowPriceOfStock.Size = new System.Drawing.Size(220, 17);
            this.CHK_ShowPriceOfStock.TabIndex = 5;
            this.CHK_ShowPriceOfStock.Text = "Показывать цену складского остатка";
            this.CHK_ShowPriceOfStock.UseVisualStyleBackColor = true;
            this.CHK_ShowPriceOfStock.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CHK_ShowSizes
            // 
            this.CHK_ShowSizes.AutoSize = true;
            this.CHK_ShowSizes.Location = new System.Drawing.Point(12, 122);
            this.CHK_ShowSizes.Name = "CHK_ShowSizes";
            this.CHK_ShowSizes.Size = new System.Drawing.Size(209, 17);
            this.CHK_ShowSizes.TabIndex = 6;
            this.CHK_ShowSizes.Text = "Показывать разбивку по размерам";
            this.CHK_ShowSizes.UseVisualStyleBackColor = true;
            this.CHK_ShowSizes.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // FRM_ReportParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 177);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры отчета";
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