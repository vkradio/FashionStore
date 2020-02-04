namespace FashionStoreWinForms.Widgets.PageAddSku
{
    partial class PanelAddSku
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PAN_SkuWONet = new System.Windows.Forms.Panel();
            this.B_Search = new System.Windows.Forms.Button();
            this.T_Name = new System.Windows.Forms.TextBox();
            this.L_Name = new System.Windows.Forms.Label();
            this.LU_Search = new System.Windows.Forms.LinkLabel();
            this.LB_Sku = new System.Windows.Forms.Label();
            this.PAN_SkuParams = new System.Windows.Forms.Panel();
            this.CB_NetType = new System.Windows.Forms.ComboBox();
            this.L_NetType = new System.Windows.Forms.Label();
            this.B_Reset = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.T_Margin = new System.Windows.Forms.TextBox();
            this.L_Margin = new System.Windows.Forms.Label();
            this.T_PriceOfSell = new System.Windows.Forms.TextBox();
            this.L_PriceOfSell = new System.Windows.Forms.Label();
            this.T_PriceOfPurchase = new System.Windows.Forms.TextBox();
            this.L_PriceOfPurchase = new System.Windows.Forms.Label();
            this.T_Amount = new System.Windows.Forms.TextBox();
            this.L_Amount = new System.Windows.Forms.Label();
            this.PAN_Net = new System.Windows.Forms.Panel();
            this.PAN_SkuWONet.SuspendLayout();
            this.PAN_SkuParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // PAN_SkuWONet
            // 
            this.PAN_SkuWONet.Controls.Add(this.B_Search);
            this.PAN_SkuWONet.Controls.Add(this.T_Name);
            this.PAN_SkuWONet.Controls.Add(this.L_Name);
            this.PAN_SkuWONet.Controls.Add(this.LU_Search);
            this.PAN_SkuWONet.Controls.Add(this.LB_Sku);
            this.PAN_SkuWONet.Controls.Add(this.PAN_SkuParams);
            this.PAN_SkuWONet.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_SkuWONet.Location = new System.Drawing.Point(0, 0);
            this.PAN_SkuWONet.Name = "PAN_SkuWONet";
            this.PAN_SkuWONet.Size = new System.Drawing.Size(550, 233);
            this.PAN_SkuWONet.TabIndex = 16;
            // 
            // B_Search
            // 
            this.B_Search.Location = new System.Drawing.Point(404, 67);
            this.B_Search.Name = "B_Search";
            this.B_Search.Size = new System.Drawing.Size(75, 23);
            this.B_Search.TabIndex = 32;
            this.B_Search.Text = "Найти";
            this.B_Search.UseVisualStyleBackColor = true;
            this.B_Search.Click += new System.EventHandler(this.B_Search_Click);
            // 
            // T_Name
            // 
            this.T_Name.AcceptsReturn = true;
            this.T_Name.Location = new System.Drawing.Point(73, 69);
            this.T_Name.Name = "T_Name";
            this.T_Name.Size = new System.Drawing.Size(325, 20);
            this.T_Name.TabIndex = 19;
            this.T_Name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.T_Name_PreviewKeyDown);
            // 
            // L_Name
            // 
            this.L_Name.AutoSize = true;
            this.L_Name.Location = new System.Drawing.Point(10, 72);
            this.L_Name.Name = "L_Name";
            this.L_Name.Size = new System.Drawing.Size(57, 13);
            this.L_Name.TabIndex = 18;
            this.L_Name.Text = "Название";
            // 
            // LU_Search
            // 
            this.LU_Search.AutoSize = true;
            this.LU_Search.Location = new System.Drawing.Point(6, 33);
            this.LU_Search.Name = "LU_Search";
            this.LU_Search.Size = new System.Drawing.Size(61, 13);
            this.LU_Search.TabIndex = 17;
            this.LU_Search.TabStop = true;
            this.LU_Search.Text = "LU_Search";
            this.LU_Search.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LU_Search_LinkClicked);
            // 
            // LB_Sku
            // 
            this.LB_Sku.AutoSize = true;
            this.LB_Sku.Location = new System.Drawing.Point(7, 10);
            this.LB_Sku.Name = "LB_Sku";
            this.LB_Sku.Size = new System.Drawing.Size(45, 13);
            this.LB_Sku.TabIndex = 16;
            this.LB_Sku.Text = "LB_Sku";
            // 
            // PAN_SkuParams
            // 
            this.PAN_SkuParams.Controls.Add(this.CB_NetType);
            this.PAN_SkuParams.Controls.Add(this.L_NetType);
            this.PAN_SkuParams.Controls.Add(this.B_Reset);
            this.PAN_SkuParams.Controls.Add(this.B_Save);
            this.PAN_SkuParams.Controls.Add(this.T_Margin);
            this.PAN_SkuParams.Controls.Add(this.L_Margin);
            this.PAN_SkuParams.Controls.Add(this.T_PriceOfSell);
            this.PAN_SkuParams.Controls.Add(this.L_PriceOfSell);
            this.PAN_SkuParams.Controls.Add(this.T_PriceOfPurchase);
            this.PAN_SkuParams.Controls.Add(this.L_PriceOfPurchase);
            this.PAN_SkuParams.Controls.Add(this.T_Amount);
            this.PAN_SkuParams.Controls.Add(this.L_Amount);
            this.PAN_SkuParams.Location = new System.Drawing.Point(0, 89);
            this.PAN_SkuParams.Name = "PAN_SkuParams";
            this.PAN_SkuParams.Size = new System.Drawing.Size(479, 144);
            this.PAN_SkuParams.TabIndex = 33;
            // 
            // CB_NetType
            // 
            this.CB_NetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_NetType.FormattingEnabled = true;
            this.CB_NetType.Location = new System.Drawing.Point(179, 116);
            this.CB_NetType.Name = "CB_NetType";
            this.CB_NetType.Size = new System.Drawing.Size(132, 21);
            this.CB_NetType.TabIndex = 31;
            this.CB_NetType.SelectedIndexChanged += new System.EventHandler(this.CB_NetType_SelectedIndexChanged);
            // 
            // L_NetType
            // 
            this.L_NetType.AutoSize = true;
            this.L_NetType.Location = new System.Drawing.Point(116, 119);
            this.L_NetType.Name = "L_NetType";
            this.L_NetType.Size = new System.Drawing.Size(58, 13);
            this.L_NetType.TabIndex = 30;
            this.L_NetType.Text = "Тип сетки";
            // 
            // B_Reset
            // 
            this.B_Reset.Location = new System.Drawing.Point(404, 111);
            this.B_Reset.Name = "B_Reset";
            this.B_Reset.Size = new System.Drawing.Size(75, 23);
            this.B_Reset.TabIndex = 29;
            this.B_Reset.Text = "Сброс";
            this.B_Reset.UseVisualStyleBackColor = true;
            this.B_Reset.Click += new System.EventHandler(this.B_Reset_Click);
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(404, 82);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 28;
            this.B_Save.Text = "Сохранить";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // T_Margin
            // 
            this.T_Margin.Location = new System.Drawing.Point(179, 89);
            this.T_Margin.Name = "T_Margin";
            this.T_Margin.ReadOnly = true;
            this.T_Margin.Size = new System.Drawing.Size(100, 20);
            this.T_Margin.TabIndex = 27;
            // 
            // L_Margin
            // 
            this.L_Margin.AutoSize = true;
            this.L_Margin.Location = new System.Drawing.Point(15, 92);
            this.L_Margin.Name = "L_Margin";
            this.L_Margin.Size = new System.Drawing.Size(158, 13);
            this.L_Margin.TabIndex = 26;
            this.L_Margin.Text = "Маржа плановая с 1 единицы";
            // 
            // T_PriceOfSell
            // 
            this.T_PriceOfSell.Location = new System.Drawing.Point(179, 63);
            this.T_PriceOfSell.Name = "T_PriceOfSell";
            this.T_PriceOfSell.Size = new System.Drawing.Size(100, 20);
            this.T_PriceOfSell.TabIndex = 25;
            this.T_PriceOfSell.TextChanged += new System.EventHandler(this.T_PriceOfSell_TextChanged);
            // 
            // L_PriceOfSell
            // 
            this.L_PriceOfSell.AutoSize = true;
            this.L_PriceOfSell.Location = new System.Drawing.Point(13, 66);
            this.L_PriceOfSell.Name = "L_PriceOfSell";
            this.L_PriceOfSell.Size = new System.Drawing.Size(160, 13);
            this.L_PriceOfSell.TabIndex = 24;
            this.L_PriceOfSell.Text = "Стоимость плановой продажи";
            // 
            // T_PriceOfPurchase
            // 
            this.T_PriceOfPurchase.Location = new System.Drawing.Point(179, 36);
            this.T_PriceOfPurchase.Name = "T_PriceOfPurchase";
            this.T_PriceOfPurchase.Size = new System.Drawing.Size(100, 20);
            this.T_PriceOfPurchase.TabIndex = 23;
            this.T_PriceOfPurchase.TextChanged += new System.EventHandler(this.T_PriceOfPurchase_TextChanged);
            // 
            // L_PriceOfPurchase
            // 
            this.L_PriceOfPurchase.AutoSize = true;
            this.L_PriceOfPurchase.Location = new System.Drawing.Point(67, 39);
            this.L_PriceOfPurchase.Name = "L_PriceOfPurchase";
            this.L_PriceOfPurchase.Size = new System.Drawing.Size(106, 13);
            this.L_PriceOfPurchase.TabIndex = 22;
            this.L_PriceOfPurchase.Text = "Стоимость закупки";
            // 
            // T_Amount
            // 
            this.T_Amount.Location = new System.Drawing.Point(179, 10);
            this.T_Amount.Name = "T_Amount";
            this.T_Amount.ReadOnly = true;
            this.T_Amount.Size = new System.Drawing.Size(100, 20);
            this.T_Amount.TabIndex = 21;
            // 
            // L_Amount
            // 
            this.L_Amount.AutoSize = true;
            this.L_Amount.Location = new System.Drawing.Point(107, 13);
            this.L_Amount.Name = "L_Amount";
            this.L_Amount.Size = new System.Drawing.Size(66, 13);
            this.L_Amount.TabIndex = 20;
            this.L_Amount.Text = "Количество";
            // 
            // PAN_Net
            // 
            this.PAN_Net.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Net.Location = new System.Drawing.Point(0, 233);
            this.PAN_Net.Name = "PAN_Net";
            this.PAN_Net.Size = new System.Drawing.Size(550, 19);
            this.PAN_Net.TabIndex = 17;
            // 
            // PanelAddSku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PAN_Net);
            this.Controls.Add(this.PAN_SkuWONet);
            this.Name = "PanelAddSku";
            this.Size = new System.Drawing.Size(550, 252);
            this.PAN_SkuWONet.ResumeLayout(false);
            this.PAN_SkuWONet.PerformLayout();
            this.PAN_SkuParams.ResumeLayout(false);
            this.PAN_SkuParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PAN_SkuWONet;
        private System.Windows.Forms.ComboBox CB_NetType;
        private System.Windows.Forms.Label L_NetType;
        private System.Windows.Forms.Button B_Reset;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.TextBox T_Margin;
        private System.Windows.Forms.Label L_Margin;
        private System.Windows.Forms.TextBox T_PriceOfSell;
        private System.Windows.Forms.Label L_PriceOfSell;
        private System.Windows.Forms.TextBox T_PriceOfPurchase;
        private System.Windows.Forms.Label L_PriceOfPurchase;
        private System.Windows.Forms.TextBox T_Amount;
        private System.Windows.Forms.Label L_Amount;
        private System.Windows.Forms.TextBox T_Name;
        private System.Windows.Forms.Label L_Name;
        private System.Windows.Forms.LinkLabel LU_Search;
        private System.Windows.Forms.Label LB_Sku;
        private System.Windows.Forms.Panel PAN_Net;
        private System.Windows.Forms.Button B_Search;
        private System.Windows.Forms.Panel PAN_SkuParams;

    }
}
