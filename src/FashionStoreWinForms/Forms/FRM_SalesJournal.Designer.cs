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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DT_DateBegin = new System.Windows.Forms.DateTimePicker();
            this.CB_RangeTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DT_DateEnd = new System.Windows.Forms.DateTimePicker();
            this.DGV_SalesJournal = new System.Windows.Forms.DataGridView();
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
            this.B_VoidSale = new System.Windows.Forms.Button();
            this.B_CloseWindow = new System.Windows.Forms.Button();
            this.L_Summary = new System.Windows.Forms.Label();
            this.B_ChangeDate = new System.Windows.Forms.Button();
            this.DL_PaymentTypes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SalesJournal)).BeginInit();
            this.SuspendLayout();
            // 
            // DT_DateBegin
            // 
            this.DT_DateBegin.CustomFormat = "dd MMM yyyy";
            this.DT_DateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_DateBegin.Location = new System.Drawing.Point(227, 12);
            this.DT_DateBegin.Name = "DT_DateBegin";
            this.DT_DateBegin.Size = new System.Drawing.Size(105, 20);
            this.DT_DateBegin.TabIndex = 0;
            this.DT_DateBegin.CloseUp += new System.EventHandler(this.DT_Date_CloseUp);
            this.DT_DateBegin.ValueChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // CB_RangeTemplate
            // 
            this.CB_RangeTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_RangeTemplate.FormattingEnabled = true;
            this.CB_RangeTemplate.Items.AddRange(new object[] {
            "Сегодня",
            "Вчера",
            "Текущий месяц",
            "В течение месяца",
            "Произвольный диапазон"});
            this.CB_RangeTemplate.Location = new System.Drawing.Point(12, 12);
            this.CB_RangeTemplate.Name = "CB_RangeTemplate";
            this.CB_RangeTemplate.Size = new System.Drawing.Size(180, 21);
            this.CB_RangeTemplate.TabIndex = 1;
            this.CB_RangeTemplate.SelectedIndexChanged += new System.EventHandler(this.CB_RangeTemplate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "с";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "по";
            // 
            // DT_DateEnd
            // 
            this.DT_DateEnd.CustomFormat = "dd MMM yyyy";
            this.DT_DateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_DateEnd.Location = new System.Drawing.Point(367, 12);
            this.DT_DateEnd.Name = "DT_DateEnd";
            this.DT_DateEnd.Size = new System.Drawing.Size(105, 20);
            this.DT_DateEnd.TabIndex = 4;
            this.DT_DateEnd.ValueChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // DGV_SalesJournal
            // 
            this.DGV_SalesJournal.AllowUserToAddRows = false;
            this.DGV_SalesJournal.AllowUserToDeleteRows = false;
            this.DGV_SalesJournal.AllowUserToOrderColumns = true;
            this.DGV_SalesJournal.AllowUserToResizeRows = false;
            this.DGV_SalesJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.DGV_SalesJournal.Location = new System.Drawing.Point(12, 39);
            this.DGV_SalesJournal.MultiSelect = false;
            this.DGV_SalesJournal.Name = "DGV_SalesJournal";
            this.DGV_SalesJournal.ReadOnly = true;
            this.DGV_SalesJournal.RowHeadersVisible = false;
            this.DGV_SalesJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_SalesJournal.Size = new System.Drawing.Size(658, 399);
            this.DGV_SalesJournal.TabIndex = 5;
            // 
            // COL_Date
            // 
            this.COL_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_Date.DataPropertyName = "time_sold";
            dataGridViewCellStyle1.Format = "dd.MM.yyyy";
            this.COL_Date.DefaultCellStyle = dataGridViewCellStyle1;
            this.COL_Date.HeaderText = "Дата";
            this.COL_Date.Name = "COL_Date";
            this.COL_Date.ReadOnly = true;
            this.COL_Date.Width = 5;
            // 
            // COL_ArticleName
            // 
            this.COL_ArticleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.COL_ArticleName.DataPropertyName = "art_name";
            this.COL_ArticleName.HeaderText = "Товар";
            this.COL_ArticleName.Name = "COL_ArticleName";
            this.COL_ArticleName.ReadOnly = true;
            this.COL_ArticleName.Width = 200;
            // 
            // COL_Y
            // 
            this.COL_Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_Y.DataPropertyName = "cell_y";
            this.COL_Y.HeaderText = "Размер";
            this.COL_Y.Name = "COL_Y";
            this.COL_Y.ReadOnly = true;
            this.COL_Y.Width = 5;
            // 
            // COL_X
            // 
            this.COL_X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_X.DataPropertyName = "cell_x";
            this.COL_X.HeaderText = "Рост";
            this.COL_X.Name = "COL_X";
            this.COL_X.ReadOnly = true;
            this.COL_X.Width = 5;
            // 
            // COL_Units
            // 
            this.COL_Units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.COL_Units.DataPropertyName = "unit_count";
            this.COL_Units.HeaderText = "Кол-во";
            this.COL_Units.Name = "COL_Units";
            this.COL_Units.ReadOnly = true;
            this.COL_Units.Width = 30;
            // 
            // COL_PriceOfPurchase
            // 
            this.COL_PriceOfPurchase.DataPropertyName = "art_price_of_purchase";
            this.COL_PriceOfPurchase.HeaderText = "Себестоим.";
            this.COL_PriceOfPurchase.Name = "COL_PriceOfPurchase";
            this.COL_PriceOfPurchase.ReadOnly = true;
            this.COL_PriceOfPurchase.Width = 50;
            // 
            // COL_PricePlan
            // 
            this.COL_PricePlan.DataPropertyName = "art_price_of_sell";
            this.COL_PricePlan.HeaderText = "Цена план.";
            this.COL_PricePlan.Name = "COL_PricePlan";
            this.COL_PricePlan.ReadOnly = true;
            this.COL_PricePlan.Width = 50;
            // 
            // COL_Price
            // 
            this.COL_Price.DataPropertyName = "unit_price";
            this.COL_Price.HeaderText = "Цена ед.";
            this.COL_Price.Name = "COL_Price";
            this.COL_Price.ReadOnly = true;
            this.COL_Price.Width = 50;
            // 
            // COL_PriceSum
            // 
            this.COL_PriceSum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.COL_PriceSum.DataPropertyName = "price_sum";
            this.COL_PriceSum.HeaderText = "Сумма";
            this.COL_PriceSum.Name = "COL_PriceSum";
            this.COL_PriceSum.ReadOnly = true;
            this.COL_PriceSum.Width = 5;
            // 
            // COL_Id
            // 
            this.COL_Id.DataPropertyName = "id";
            this.COL_Id.HeaderText = "Id";
            this.COL_Id.Name = "COL_Id";
            this.COL_Id.ReadOnly = true;
            this.COL_Id.Visible = false;
            // 
            // B_VoidSale
            // 
            this.B_VoidSale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_VoidSale.Location = new System.Drawing.Point(429, 453);
            this.B_VoidSale.Name = "B_VoidSale";
            this.B_VoidSale.Size = new System.Drawing.Size(115, 23);
            this.B_VoidSale.TabIndex = 6;
            this.B_VoidSale.Text = "Отменить продажу";
            this.B_VoidSale.UseVisualStyleBackColor = true;
            this.B_VoidSale.Click += new System.EventHandler(this.B_VoidSale_Click);
            // 
            // B_CloseWindow
            // 
            this.B_CloseWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_CloseWindow.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_CloseWindow.Location = new System.Drawing.Point(553, 453);
            this.B_CloseWindow.Name = "B_CloseWindow";
            this.B_CloseWindow.Size = new System.Drawing.Size(115, 23);
            this.B_CloseWindow.TabIndex = 7;
            this.B_CloseWindow.Text = "Закрыть окно";
            this.B_CloseWindow.UseVisualStyleBackColor = true;
            // 
            // L_Summary
            // 
            this.L_Summary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.L_Summary.AutoSize = true;
            this.L_Summary.Location = new System.Drawing.Point(12, 458);
            this.L_Summary.Name = "L_Summary";
            this.L_Summary.Size = new System.Drawing.Size(62, 13);
            this.L_Summary.TabIndex = 8;
            this.L_Summary.Text = "L_Summary";
            // 
            // B_ChangeDate
            // 
            this.B_ChangeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_ChangeDate.Enabled = false;
            this.B_ChangeDate.Location = new System.Drawing.Point(302, 453);
            this.B_ChangeDate.Name = "B_ChangeDate";
            this.B_ChangeDate.Size = new System.Drawing.Size(115, 23);
            this.B_ChangeDate.TabIndex = 9;
            this.B_ChangeDate.Text = "Изменить дату";
            this.B_ChangeDate.UseVisualStyleBackColor = true;
            this.B_ChangeDate.Click += new System.EventHandler(this.B_ChangeDate_Click);
            // 
            // DL_PaymentTypes
            // 
            this.DL_PaymentTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DL_PaymentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DL_PaymentTypes.FormattingEnabled = true;
            this.DL_PaymentTypes.Items.AddRange(new object[] {
            "Любые оплаты",
            "Только наличные",
            "Только карта"});
            this.DL_PaymentTypes.Location = new System.Drawing.Point(490, 11);
            this.DL_PaymentTypes.MaxDropDownItems = 3;
            this.DL_PaymentTypes.Name = "DL_PaymentTypes";
            this.DL_PaymentTypes.Size = new System.Drawing.Size(180, 21);
            this.DL_PaymentTypes.TabIndex = 10;
            this.DL_PaymentTypes.SelectedIndexChanged += new System.EventHandler(this.DT_Date_ValueChanged);
            // 
            // FRM_SalesJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_CloseWindow;
            this.ClientSize = new System.Drawing.Size(683, 488);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Журнал продаж";
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
        private System.Windows.Forms.ComboBox DL_PaymentTypes;
    }
}