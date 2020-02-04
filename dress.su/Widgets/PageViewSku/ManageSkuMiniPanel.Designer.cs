namespace dress.su.Widgets.PageViewSku
{
    partial class ManageSkuMiniPanel
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
            this.T_Name = new System.Windows.Forms.TextBox();
            this.T_CellX = new System.Windows.Forms.TextBox();
            this.T_CellY = new System.Windows.Forms.TextBox();
            this.B_Net = new System.Windows.Forms.Button();
            this.T_Amount = new System.Windows.Forms.TextBox();
            this.T_PriceOfSell = new System.Windows.Forms.TextBox();
            this.T_MarginTotal = new System.Windows.Forms.TextBox();
            this.T_InStock = new System.Windows.Forms.TextBox();
            this.T_PriceOfPurchase = new System.Windows.Forms.TextBox();
            this.T_PriceOfSellPlan = new System.Windows.Forms.TextBox();
            this.B_Delete = new System.Windows.Forms.Button();
            this.CB_PointsOfSale = new System.Windows.Forms.ComboBox();
            this.B_Move = new System.Windows.Forms.Button();
            this.B_Sell = new System.Windows.Forms.Button();
            this.PAN_NoNet = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PAN_NoNet.SuspendLayout();
            this.SuspendLayout();
            // 
            // T_Name
            // 
            this.T_Name.Location = new System.Drawing.Point(3, 3);
            this.T_Name.Name = "T_Name";
            this.T_Name.ReadOnly = true;
            this.T_Name.Size = new System.Drawing.Size(96, 20);
            this.T_Name.TabIndex = 0;
            // 
            // T_CellY
            // 
            this.T_CellY.Location = new System.Drawing.Point(105, 3);
            this.T_CellY.Name = "T_CellY";
            this.T_CellY.ReadOnly = true;
            this.T_CellY.Size = new System.Drawing.Size(48, 20);
            this.T_CellY.TabIndex = 1;
            // 
            // T_CellX
            // 
            this.T_CellX.Location = new System.Drawing.Point(159, 3);
            this.T_CellX.Name = "T_CellX";
            this.T_CellX.ReadOnly = true;
            this.T_CellX.Size = new System.Drawing.Size(45, 20);
            this.T_CellX.TabIndex = 2;
            // 
            // B_Net
            // 
            this.B_Net.Location = new System.Drawing.Point(117, 28);
            this.B_Net.Name = "B_Net";
            this.B_Net.Size = new System.Drawing.Size(75, 21);
            this.B_Net.TabIndex = 3;
            this.B_Net.Text = "Сетка";
            this.B_Net.UseVisualStyleBackColor = true;
            this.B_Net.Click += new System.EventHandler(this.B_Net_Click);
            // 
            // T_Amount
            // 
            this.T_Amount.Location = new System.Drawing.Point(210, 3);
            this.T_Amount.Name = "T_Amount";
            this.T_Amount.Size = new System.Drawing.Size(54, 20);
            this.T_Amount.TabIndex = 4;
            this.T_Amount.TextChanged += new System.EventHandler(this.T_Amount_TextChanged);
            // 
            // T_PriceOfSell
            // 
            this.T_PriceOfSell.Location = new System.Drawing.Point(270, 3);
            this.T_PriceOfSell.Name = "T_PriceOfSell";
            this.T_PriceOfSell.Size = new System.Drawing.Size(53, 20);
            this.T_PriceOfSell.TabIndex = 5;
            this.T_PriceOfSell.TextChanged += new System.EventHandler(this.T_PriceOfSell_TextChanged);
            // 
            // T_MarginTotal
            // 
            this.T_MarginTotal.Location = new System.Drawing.Point(410, 2);
            this.T_MarginTotal.Name = "T_MarginTotal";
            this.T_MarginTotal.ReadOnly = true;
            this.T_MarginTotal.Size = new System.Drawing.Size(51, 20);
            this.T_MarginTotal.TabIndex = 6;
            // 
            // T_InStock
            // 
            this.T_InStock.Location = new System.Drawing.Point(467, 2);
            this.T_InStock.Name = "T_InStock";
            this.T_InStock.ReadOnly = true;
            this.T_InStock.Size = new System.Drawing.Size(53, 20);
            this.T_InStock.TabIndex = 7;
            // 
            // T_PriceOfPurchase
            // 
            this.T_PriceOfPurchase.Location = new System.Drawing.Point(526, 2);
            this.T_PriceOfPurchase.Name = "T_PriceOfPurchase";
            this.T_PriceOfPurchase.ReadOnly = true;
            this.T_PriceOfPurchase.Size = new System.Drawing.Size(58, 20);
            this.T_PriceOfPurchase.TabIndex = 8;
            // 
            // T_PriceOfSellPlan
            // 
            this.T_PriceOfSellPlan.Location = new System.Drawing.Point(590, 2);
            this.T_PriceOfSellPlan.Name = "T_PriceOfSellPlan";
            this.T_PriceOfSellPlan.ReadOnly = true;
            this.T_PriceOfSellPlan.Size = new System.Drawing.Size(61, 20);
            this.T_PriceOfSellPlan.TabIndex = 9;
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(657, 2);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(59, 23);
            this.B_Delete.TabIndex = 10;
            this.B_Delete.Text = "Удалить";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // CB_PointsOfSale
            // 
            this.CB_PointsOfSale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PointsOfSale.FormattingEnabled = true;
            this.CB_PointsOfSale.Location = new System.Drawing.Point(722, 3);
            this.CB_PointsOfSale.Name = "CB_PointsOfSale";
            this.CB_PointsOfSale.Size = new System.Drawing.Size(121, 21);
            this.CB_PointsOfSale.TabIndex = 11;
            // 
            // B_Move
            // 
            this.B_Move.Location = new System.Drawing.Point(849, 2);
            this.B_Move.Name = "B_Move";
            this.B_Move.Size = new System.Drawing.Size(85, 23);
            this.B_Move.TabIndex = 12;
            this.B_Move.Text = "Перебросить";
            this.B_Move.UseVisualStyleBackColor = true;
            this.B_Move.Click += new System.EventHandler(this.B_Move_Click);
            // 
            // B_Sell
            // 
            this.B_Sell.Location = new System.Drawing.Point(329, 3);
            this.B_Sell.Name = "B_Sell";
            this.B_Sell.Size = new System.Drawing.Size(75, 23);
            this.B_Sell.TabIndex = 13;
            this.B_Sell.Text = "Продать";
            this.B_Sell.UseVisualStyleBackColor = true;
            this.B_Sell.Click += new System.EventHandler(this.B_Sell_Click);
            // 
            // PAN_NoNet
            // 
            this.PAN_NoNet.Controls.Add(this.label1);
            this.PAN_NoNet.Location = new System.Drawing.Point(105, 0);
            this.PAN_NoNet.Name = "PAN_NoNet";
            this.PAN_NoNet.Size = new System.Drawing.Size(99, 52);
            this.PAN_NoNet.TabIndex = 14;
            this.PAN_NoNet.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "нет сетки";
            // 
            // ManageSkuMiniPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PAN_NoNet);
            this.Controls.Add(this.B_Sell);
            this.Controls.Add(this.B_Move);
            this.Controls.Add(this.CB_PointsOfSale);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.T_PriceOfSellPlan);
            this.Controls.Add(this.T_PriceOfPurchase);
            this.Controls.Add(this.T_InStock);
            this.Controls.Add(this.T_MarginTotal);
            this.Controls.Add(this.T_PriceOfSell);
            this.Controls.Add(this.T_Amount);
            this.Controls.Add(this.B_Net);
            this.Controls.Add(this.T_CellY);
            this.Controls.Add(this.T_CellX);
            this.Controls.Add(this.T_Name);
            this.Name = "ManageSkuMiniPanel";
            this.Size = new System.Drawing.Size(941, 52);
            this.PAN_NoNet.ResumeLayout(false);
            this.PAN_NoNet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_Name;
        private System.Windows.Forms.TextBox T_CellX;
        private System.Windows.Forms.TextBox T_CellY;
        private System.Windows.Forms.Button B_Net;
        private System.Windows.Forms.TextBox T_Amount;
        private System.Windows.Forms.TextBox T_PriceOfSell;
        private System.Windows.Forms.TextBox T_MarginTotal;
        private System.Windows.Forms.TextBox T_InStock;
        private System.Windows.Forms.TextBox T_PriceOfPurchase;
        private System.Windows.Forms.TextBox T_PriceOfSellPlan;
        private System.Windows.Forms.Button B_Delete;
        private System.Windows.Forms.ComboBox CB_PointsOfSale;
        private System.Windows.Forms.Button B_Move;
        private System.Windows.Forms.Button B_Sell;
        private System.Windows.Forms.Panel PAN_NoNet;
        private System.Windows.Forms.Label label1;
    }
}
