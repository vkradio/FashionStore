namespace dress.su.Forms
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
            this.L_Question.Location = new System.Drawing.Point(12, 38);
            this.L_Question.Name = "L_Question";
            this.L_Question.Size = new System.Drawing.Size(156, 44);
            this.L_Question.TabIndex = 0;
            this.L_Question.Text = "Выберите существующую закупочную цену или задайте новую.";
            // 
            // L_Article
            // 
            this.L_Article.AutoSize = true;
            this.L_Article.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.L_Article.Location = new System.Drawing.Point(12, 9);
            this.L_Article.Name = "L_Article";
            this.L_Article.Size = new System.Drawing.Size(72, 17);
            this.L_Article.TabIndex = 1;
            this.L_Article.Text = "L_Article";
            // 
            // B_Choose
            // 
            this.B_Choose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Choose.Location = new System.Drawing.Point(12, 194);
            this.B_Choose.Name = "B_Choose";
            this.B_Choose.Size = new System.Drawing.Size(75, 23);
            this.B_Choose.TabIndex = 3;
            this.B_Choose.Text = "Выбрать";
            this.B_Choose.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(93, 194);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 4;
            this.B_Cancel.Text = "Отмена";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // LST_Prices
            // 
            this.LST_Prices.FormattingEnabled = true;
            this.LST_Prices.Location = new System.Drawing.Point(12, 96);
            this.LST_Prices.Name = "LST_Prices";
            this.LST_Prices.ScrollAlwaysVisible = true;
            this.LST_Prices.Size = new System.Drawing.Size(156, 95);
            this.LST_Prices.TabIndex = 5;
            this.LST_Prices.DoubleClick += new System.EventHandler(this.LST_Prices_DoubleClick);
            // 
            // T_NewPrice
            // 
            this.T_NewPrice.Location = new System.Drawing.Point(12, 255);
            this.T_NewPrice.Name = "T_NewPrice";
            this.T_NewPrice.Size = new System.Drawing.Size(100, 20);
            this.T_NewPrice.TabIndex = 6;
            this.T_NewPrice.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.T_NewPrice_PreviewKeyDown);
            // 
            // B_NewPrice
            // 
            this.B_NewPrice.Location = new System.Drawing.Point(12, 281);
            this.B_NewPrice.Name = "B_NewPrice";
            this.B_NewPrice.Size = new System.Drawing.Size(75, 23);
            this.B_NewPrice.TabIndex = 7;
            this.B_NewPrice.Text = "Новая цена";
            this.B_NewPrice.UseVisualStyleBackColor = true;
            this.B_NewPrice.Click += new System.EventHandler(this.B_NewPrice_Click);
            // 
            // FRM_SelectArticleByPriceOfPurchase
            // 
            this.AcceptButton = this.B_Choose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(181, 315);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Закупочная цена";
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