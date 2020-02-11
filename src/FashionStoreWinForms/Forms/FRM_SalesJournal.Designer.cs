namespace FashionStoreWinForms.Forms
{
    partial class FRM_SalesJournal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_SalesJournal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DT_DateBegin = new System.Windows.Forms.DateTimePicker();
            this.CB_RangeTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DT_DateEnd = new System.Windows.Forms.DateTimePicker();
            this.DGV_SalesJournal = new System.Windows.Forms.DataGridView();
            this.B_VoidSale = new System.Windows.Forms.Button();
            this.B_CloseWindow = new System.Windows.Forms.Button();
            this.L_Summary = new System.Windows.Forms.Label();
            this.B_ChangeDate = new System.Windows.Forms.Button();
            this.DL_PaymentTypes = new System.Windows.Forms.ComboBox();
            this.COL_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_ArticleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_Units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_PriceOfPurchase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_PricePlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_PriceSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SalesJournal)).BeginInit();
            this.SuspendLayout();
            // 
            // DT_DateBegin
            // 
            resources.ApplyResources(this.DT_DateBegin, "DT_DateBegin");
            this.DT_DateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_DateBegin.Name = "DT_DateBegin";
            this.DT_DateBegin.CloseUp += new System.EventHandler(this.DT_Date_CloseUp);
            this.DT_DateBegin.ValueChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // CB_RangeTemplate
            // 
            resources.ApplyResources(this.CB_RangeTemplate, "CB_RangeTemplate");
            this.CB_RangeTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_RangeTemplate.FormattingEnabled = true;
            this.CB_RangeTemplate.Items.AddRange(new object[] {
            resources.GetString("CB_RangeTemplate.Items"),
            resources.GetString("CB_RangeTemplate.Items1"),
            resources.GetString("CB_RangeTemplate.Items2"),
            resources.GetString("CB_RangeTemplate.Items3"),
            resources.GetString("CB_RangeTemplate.Items4")});
            this.CB_RangeTemplate.Name = "CB_RangeTemplate";
            this.CB_RangeTemplate.SelectedIndexChanged += new System.EventHandler(this.CB_RangeTemplate_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // DT_DateEnd
            // 
            resources.ApplyResources(this.DT_DateEnd, "DT_DateEnd");
            this.DT_DateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_DateEnd.Name = "DT_DateEnd";
            this.DT_DateEnd.ValueChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // DGV_SalesJournal
            // 
            resources.ApplyResources(this.DGV_SalesJournal, "DGV_SalesJournal");
            this.DGV_SalesJournal.AllowUserToAddRows = false;
            this.DGV_SalesJournal.AllowUserToDeleteRows = false;
            this.DGV_SalesJournal.AllowUserToOrderColumns = true;
            this.DGV_SalesJournal.AllowUserToResizeRows = false;
            this.DGV_SalesJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_SalesJournal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COL_Date,
            this.COL_ArticleName,
            this.COL_Y,
            this.COL_X,
            this.COL_Units,
            this.COL_PriceOfPurchase,
            this.COL_PricePlan,
            this.COL_Price,
            this.COL_PriceSum,
            this.COL_Id});
            this.DGV_SalesJournal.MultiSelect = false;
            this.DGV_SalesJournal.Name = "DGV_SalesJournal";
            this.DGV_SalesJournal.ReadOnly = true;
            this.DGV_SalesJournal.RowHeadersVisible = false;
            this.DGV_SalesJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // B_VoidSale
            // 
            resources.ApplyResources(this.B_VoidSale, "B_VoidSale");
            this.B_VoidSale.Name = "B_VoidSale";
            this.B_VoidSale.UseVisualStyleBackColor = true;
            this.B_VoidSale.Click += new System.EventHandler(this.B_VoidSale_Click);
            // 
            // B_CloseWindow
            // 
            resources.ApplyResources(this.B_CloseWindow, "B_CloseWindow");
            this.B_CloseWindow.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_CloseWindow.Name = "B_CloseWindow";
            this.B_CloseWindow.UseVisualStyleBackColor = true;
            // 
            // L_Summary
            // 
            resources.ApplyResources(this.L_Summary, "L_Summary");
            this.L_Summary.Name = "L_Summary";
            // 
            // B_ChangeDate
            // 
            resources.ApplyResources(this.B_ChangeDate, "B_ChangeDate");
            this.B_ChangeDate.Name = "B_ChangeDate";
            this.B_ChangeDate.UseVisualStyleBackColor = true;
            this.B_ChangeDate.Click += new System.EventHandler(this.B_ChangeDate_Click);
            // 
            // DL_PaymentTypes
            // 
            resources.ApplyResources(this.DL_PaymentTypes, "DL_PaymentTypes");
            this.DL_PaymentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DL_PaymentTypes.FormattingEnabled = true;
            this.DL_PaymentTypes.Items.AddRange(new object[] {
            resources.GetString("DL_PaymentTypes.Items"),
            resources.GetString("DL_PaymentTypes.Items1"),
            resources.GetString("DL_PaymentTypes.Items2")});
            this.DL_PaymentTypes.Name = "DL_PaymentTypes";
            this.DL_PaymentTypes.SelectedIndexChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // COL_Date
            // 
            this.COL_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_Date.DataPropertyName = "time_sold";
            dataGridViewCellStyle3.Format = "dd.MM.yyyy";
            this.COL_Date.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.COL_Date, "COL_Date");
            this.COL_Date.Name = "COL_Date";
            this.COL_Date.ReadOnly = true;
            // 
            // COL_ArticleName
            // 
            this.COL_ArticleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.COL_ArticleName.DataPropertyName = "art_name";
            resources.ApplyResources(this.COL_ArticleName, "COL_ArticleName");
            this.COL_ArticleName.Name = "COL_ArticleName";
            this.COL_ArticleName.ReadOnly = true;
            // 
            // COL_Y
            // 
            this.COL_Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_Y.DataPropertyName = "cell_y";
            resources.ApplyResources(this.COL_Y, "COL_Y");
            this.COL_Y.Name = "COL_Y";
            this.COL_Y.ReadOnly = true;
            // 
            // COL_X
            // 
            this.COL_X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_X.DataPropertyName = "cell_x";
            resources.ApplyResources(this.COL_X, "COL_X");
            this.COL_X.Name = "COL_X";
            this.COL_X.ReadOnly = true;
            // 
            // COL_Units
            // 
            this.COL_Units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.COL_Units.DataPropertyName = "unit_count";
            resources.ApplyResources(this.COL_Units, "COL_Units");
            this.COL_Units.Name = "COL_Units";
            this.COL_Units.ReadOnly = true;
            // 
            // COL_PriceOfPurchase
            // 
            this.COL_PriceOfPurchase.DataPropertyName = "art_price_of_purchase";
            resources.ApplyResources(this.COL_PriceOfPurchase, "COL_PriceOfPurchase");
            this.COL_PriceOfPurchase.Name = "COL_PriceOfPurchase";
            this.COL_PriceOfPurchase.ReadOnly = true;
            // 
            // COL_PricePlan
            // 
            this.COL_PricePlan.DataPropertyName = "art_price_of_sell";
            resources.ApplyResources(this.COL_PricePlan, "COL_PricePlan");
            this.COL_PricePlan.Name = "COL_PricePlan";
            this.COL_PricePlan.ReadOnly = true;
            // 
            // COL_Price
            // 
            this.COL_Price.DataPropertyName = "unit_price";
            resources.ApplyResources(this.COL_Price, "COL_Price");
            this.COL_Price.Name = "COL_Price";
            this.COL_Price.ReadOnly = true;
            // 
            // COL_PriceSum
            // 
            this.COL_PriceSum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_PriceSum.DataPropertyName = "price_sum";
            resources.ApplyResources(this.COL_PriceSum, "COL_PriceSum");
            this.COL_PriceSum.Name = "COL_PriceSum";
            this.COL_PriceSum.ReadOnly = true;
            // 
            // COL_Id
            // 
            this.COL_Id.DataPropertyName = "id";
            resources.ApplyResources(this.COL_Id, "COL_Id");
            this.COL_Id.Name = "COL_Id";
            this.COL_Id.ReadOnly = true;
            // 
            // FRM_SalesJournal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_CloseWindow;
            this.Controls.Add(this.DL_PaymentTypes);
            this.Controls.Add(this.B_ChangeDate);
            this.Controls.Add(this.L_Summary);
            this.Controls.Add(this.B_CloseWindow);
            this.Controls.Add(this.B_VoidSale);
            this.Controls.Add(this.DGV_SalesJournal);
            this.Controls.Add(this.DT_DateEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_RangeTemplate);
            this.Controls.Add(this.DT_DateBegin);
            this.MinimizeBox = false;
            this.Name = "FRM_SalesJournal";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FRM_SalesJournal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SalesJournal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DT_DateBegin;
        private System.Windows.Forms.ComboBox CB_RangeTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DT_DateEnd;
        private System.Windows.Forms.DataGridView DGV_SalesJournal;
        private System.Windows.Forms.Button B_VoidSale;
        private System.Windows.Forms.Button B_CloseWindow;
        private System.Windows.Forms.Label L_Summary;
        private System.Windows.Forms.Button B_ChangeDate;
        private System.Windows.Forms.ComboBox DL_PaymentTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_ArticleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_Units;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_PriceOfPurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_PricePlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_PriceSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_Id;
    }
}