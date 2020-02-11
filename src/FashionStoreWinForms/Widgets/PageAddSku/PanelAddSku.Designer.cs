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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelAddSku));
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
            resources.ApplyResources(this.PAN_SkuWONet, "PAN_SkuWONet");
            this.PAN_SkuWONet.Name = "PAN_SkuWONet";
            // 
            // B_Search
            // 
            resources.ApplyResources(this.B_Search, "B_Search");
            this.B_Search.Name = "B_Search";
            this.B_Search.UseVisualStyleBackColor = true;
            this.B_Search.Click += new System.EventHandler(this.B_Search_Click);
            // 
            // T_Name
            // 
            this.T_Name.AcceptsReturn = true;
            resources.ApplyResources(this.T_Name, "T_Name");
            this.T_Name.Name = "T_Name";
            this.T_Name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.T_Name_PreviewKeyDown);
            // 
            // L_Name
            // 
            resources.ApplyResources(this.L_Name, "L_Name");
            this.L_Name.Name = "L_Name";
            // 
            // LU_Search
            // 
            resources.ApplyResources(this.LU_Search, "LU_Search");
            this.LU_Search.Name = "LU_Search";
            this.LU_Search.TabStop = true;
            this.LU_Search.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LU_Search_LinkClicked);
            // 
            // LB_Sku
            // 
            resources.ApplyResources(this.LB_Sku, "LB_Sku");
            this.LB_Sku.Name = "LB_Sku";
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
            resources.ApplyResources(this.PAN_SkuParams, "PAN_SkuParams");
            this.PAN_SkuParams.Name = "PAN_SkuParams";
            // 
            // CB_NetType
            // 
            this.CB_NetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_NetType.FormattingEnabled = true;
            resources.ApplyResources(this.CB_NetType, "CB_NetType");
            this.CB_NetType.Name = "CB_NetType";
            this.CB_NetType.SelectedIndexChanged += new System.EventHandler(this.CB_NetType_SelectedIndexChanged);
            // 
            // L_NetType
            // 
            resources.ApplyResources(this.L_NetType, "L_NetType");
            this.L_NetType.Name = "L_NetType";
            // 
            // B_Reset
            // 
            resources.ApplyResources(this.B_Reset, "B_Reset");
            this.B_Reset.Name = "B_Reset";
            this.B_Reset.UseVisualStyleBackColor = true;
            this.B_Reset.Click += new System.EventHandler(this.B_Reset_Click);
            // 
            // B_Save
            // 
            resources.ApplyResources(this.B_Save, "B_Save");
            this.B_Save.Name = "B_Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // T_Margin
            // 
            resources.ApplyResources(this.T_Margin, "T_Margin");
            this.T_Margin.Name = "T_Margin";
            this.T_Margin.ReadOnly = true;
            // 
            // L_Margin
            // 
            resources.ApplyResources(this.L_Margin, "L_Margin");
            this.L_Margin.Name = "L_Margin";
            // 
            // T_PriceOfSell
            // 
            resources.ApplyResources(this.T_PriceOfSell, "T_PriceOfSell");
            this.T_PriceOfSell.Name = "T_PriceOfSell";
            this.T_PriceOfSell.TextChanged += new System.EventHandler(this.T_PriceOfSell_TextChanged);
            // 
            // L_PriceOfSell
            // 
            resources.ApplyResources(this.L_PriceOfSell, "L_PriceOfSell");
            this.L_PriceOfSell.Name = "L_PriceOfSell";
            // 
            // T_PriceOfPurchase
            // 
            resources.ApplyResources(this.T_PriceOfPurchase, "T_PriceOfPurchase");
            this.T_PriceOfPurchase.Name = "T_PriceOfPurchase";
            this.T_PriceOfPurchase.TextChanged += new System.EventHandler(this.T_PriceOfPurchase_TextChanged);
            // 
            // L_PriceOfPurchase
            // 
            resources.ApplyResources(this.L_PriceOfPurchase, "L_PriceOfPurchase");
            this.L_PriceOfPurchase.Name = "L_PriceOfPurchase";
            // 
            // T_Amount
            // 
            resources.ApplyResources(this.T_Amount, "T_Amount");
            this.T_Amount.Name = "T_Amount";
            this.T_Amount.ReadOnly = true;
            // 
            // L_Amount
            // 
            resources.ApplyResources(this.L_Amount, "L_Amount");
            this.L_Amount.Name = "L_Amount";
            // 
            // PAN_Net
            // 
            resources.ApplyResources(this.PAN_Net, "PAN_Net");
            this.PAN_Net.Name = "PAN_Net";
            // 
            // PanelAddSku
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PAN_Net);
            this.Controls.Add(this.PAN_SkuWONet);
            this.Name = "PanelAddSku";
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
