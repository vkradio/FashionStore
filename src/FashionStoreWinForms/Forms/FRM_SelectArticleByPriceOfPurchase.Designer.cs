namespace FashionStoreWinForms.Forms
{
    partial class FRM_SelectArticleByPriceOfPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_SelectArticleByPriceOfPurchase));
            this.L_Question = new System.Windows.Forms.Label();
            this.L_Article = new System.Windows.Forms.Label();
            this.B_Choose = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.LST_Prices = new System.Windows.Forms.ListBox();
            this.T_NewPrice = new System.Windows.Forms.TextBox();
            this.B_NewPrice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // L_Question
            // 
            resources.ApplyResources(this.L_Question, "L_Question");
            this.L_Question.Name = "L_Question";
            // 
            // L_Article
            // 
            resources.ApplyResources(this.L_Article, "L_Article");
            this.L_Article.Name = "L_Article";
            // 
            // B_Choose
            // 
            resources.ApplyResources(this.B_Choose, "B_Choose");
            this.B_Choose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Choose.Name = "B_Choose";
            this.B_Choose.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            resources.ApplyResources(this.B_Cancel, "B_Cancel");
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // LST_Prices
            // 
            resources.ApplyResources(this.LST_Prices, "LST_Prices");
            this.LST_Prices.FormattingEnabled = true;
            this.LST_Prices.Name = "LST_Prices";
            this.LST_Prices.DoubleClick += new System.EventHandler(this.LST_Prices_DoubleClick);
            // 
            // T_NewPrice
            // 
            resources.ApplyResources(this.T_NewPrice, "T_NewPrice");
            this.T_NewPrice.Name = "T_NewPrice";
            this.T_NewPrice.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.T_NewPrice_PreviewKeyDown);
            // 
            // B_NewPrice
            // 
            resources.ApplyResources(this.B_NewPrice, "B_NewPrice");
            this.B_NewPrice.Name = "B_NewPrice";
            this.B_NewPrice.UseVisualStyleBackColor = true;
            this.B_NewPrice.Click += new System.EventHandler(this.B_NewPrice_Click);
            // 
            // FRM_SelectArticleByPriceOfPurchase
            // 
            this.AcceptButton = this.B_Choose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.Controls.Add(this.B_NewPrice);
            this.Controls.Add(this.T_NewPrice);
            this.Controls.Add(this.LST_Prices);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Choose);
            this.Controls.Add(this.L_Article);
            this.Controls.Add(this.L_Question);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_SelectArticleByPriceOfPurchase";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label L_Question;
        private System.Windows.Forms.Label L_Article;
        private System.Windows.Forms.Button B_Choose;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.ListBox LST_Prices;
        private System.Windows.Forms.TextBox T_NewPrice;
        private System.Windows.Forms.Button B_NewPrice;
    }
}