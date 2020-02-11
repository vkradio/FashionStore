namespace FashionStoreWinForms.Widgets.PageViewSku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSkuMiniPanel));
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
            resources.ApplyResources(this.T_Name, "T_Name");
            this.T_Name.Name = "T_Name";
            this.T_Name.ReadOnly = true;
            // 
            // T_CellX
            // 
            resources.ApplyResources(this.T_CellX, "T_CellX");
            this.T_CellX.Name = "T_CellX";
            this.T_CellX.ReadOnly = true;
            // 
            // T_CellY
            // 
            resources.ApplyResources(this.T_CellY, "T_CellY");
            this.T_CellY.Name = "T_CellY";
            this.T_CellY.ReadOnly = true;
            // 
            // B_Net
            // 
            resources.ApplyResources(this.B_Net, "B_Net");
            this.B_Net.Name = "B_Net";
            this.B_Net.UseVisualStyleBackColor = true;
            this.B_Net.Click += new System.EventHandler(this.B_Net_Click);
            // 
            // T_Amount
            // 
            resources.ApplyResources(this.T_Amount, "T_Amount");
            this.T_Amount.Name = "T_Amount";
            this.T_Amount.TextChanged += new System.EventHandler(this.T_Amount_TextChanged);
            // 
            // T_PriceOfSell
            // 
            resources.ApplyResources(this.T_PriceOfSell, "T_PriceOfSell");
            this.T_PriceOfSell.Name = "T_PriceOfSell";
            this.T_PriceOfSell.TextChanged += new System.EventHandler(this.T_PriceOfSell_TextChanged);
            // 
            // T_MarginTotal
            // 
            resources.ApplyResources(this.T_MarginTotal, "T_MarginTotal");
            this.T_MarginTotal.Name = "T_MarginTotal";
            this.T_MarginTotal.ReadOnly = true;
            // 
            // T_InStock
            // 
            resources.ApplyResources(this.T_InStock, "T_InStock");
            this.T_InStock.Name = "T_InStock";
            this.T_InStock.ReadOnly = true;
            // 
            // T_PriceOfPurchase
            // 
            resources.ApplyResources(this.T_PriceOfPurchase, "T_PriceOfPurchase");
            this.T_PriceOfPurchase.Name = "T_PriceOfPurchase";
            this.T_PriceOfPurchase.ReadOnly = true;
            // 
            // T_PriceOfSellPlan
            // 
            resources.ApplyResources(this.T_PriceOfSellPlan, "T_PriceOfSellPlan");
            this.T_PriceOfSellPlan.Name = "T_PriceOfSellPlan";
            this.T_PriceOfSellPlan.ReadOnly = true;
            // 
            // B_Delete
            // 
            resources.ApplyResources(this.B_Delete, "B_Delete");
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // CB_PointsOfSale
            // 
            resources.ApplyResources(this.CB_PointsOfSale, "CB_PointsOfSale");
            this.CB_PointsOfSale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PointsOfSale.FormattingEnabled = true;
            this.CB_PointsOfSale.Name = "CB_PointsOfSale";
            // 
            // B_Move
            // 
            resources.ApplyResources(this.B_Move, "B_Move");
            this.B_Move.Name = "B_Move";
            this.B_Move.UseVisualStyleBackColor = true;
            this.B_Move.Click += new System.EventHandler(this.B_Move_Click);
            // 
            // B_Sell
            // 
            resources.ApplyResources(this.B_Sell, "B_Sell");
            this.B_Sell.Name = "B_Sell";
            this.B_Sell.UseVisualStyleBackColor = true;
            this.B_Sell.Click += new System.EventHandler(this.B_Sell_Click);
            // 
            // PAN_NoNet
            // 
            resources.ApplyResources(this.PAN_NoNet, "PAN_NoNet");
            this.PAN_NoNet.Controls.Add(this.label1);
            this.PAN_NoNet.Name = "PAN_NoNet";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ManageSkuMiniPanel
            // 
            resources.ApplyResources(this, "$this");
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
