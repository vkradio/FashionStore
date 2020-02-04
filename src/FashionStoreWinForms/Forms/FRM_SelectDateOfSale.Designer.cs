namespace FashionStoreWinForms.Forms
{
    partial class FRM_SelectDateOfSale
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
            this.DAT_Date = new System.Windows.Forms.DateTimePicker();
            this.L_Desctription = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.B_Sell = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.CHK_PaymentByCard = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DAT_Date
            // 
            this.DAT_Date.CustomFormat = "dd MMMM yyyy";
            this.DAT_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DAT_Date.Location = new System.Drawing.Point(12, 93);
            this.DAT_Date.Name = "DAT_Date";
            this.DAT_Date.Size = new System.Drawing.Size(146, 20);
            this.DAT_Date.TabIndex = 0;
            // 
            // L_Desctription
            // 
            this.L_Desctription.Location = new System.Drawing.Point(9, 9);
            this.L_Desctription.Name = "L_Desctription";
            this.L_Desctription.Size = new System.Drawing.Size(198, 68);
            this.L_Desctription.TabIndex = 1;
            this.L_Desctription.Text = "L_Desctription";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата продажи";
            // 
            // B_Sell
            // 
            this.B_Sell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Sell.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Sell.Location = new System.Drawing.Point(51, 170);
            this.B_Sell.Name = "B_Sell";
            this.B_Sell.Size = new System.Drawing.Size(75, 23);
            this.B_Sell.TabIndex = 3;
            this.B_Sell.Text = "Продать";
            this.B_Sell.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(132, 170);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 4;
            this.B_Cancel.Text = "Отмена";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // CHK_PaymentByCard
            // 
            this.CHK_PaymentByCard.AutoSize = true;
            this.CHK_PaymentByCard.Location = new System.Drawing.Point(12, 129);
            this.CHK_PaymentByCard.Name = "CHK_PaymentByCard";
            this.CHK_PaymentByCard.Size = new System.Drawing.Size(101, 17);
            this.CHK_PaymentByCard.TabIndex = 5;
            this.CHK_PaymentByCard.Text = "Оплата картой";
            this.CHK_PaymentByCard.UseVisualStyleBackColor = true;
            // 
            // FRM_SelectDateOfSale
            // 
            this.AcceptButton = this.B_Sell;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(219, 205);
            this.Controls.Add(this.CHK_PaymentByCard);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Sell);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.L_Desctription);
            this.Controls.Add(this.DAT_Date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_SelectDateOfSale";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Продажа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DAT_Date;
        private System.Windows.Forms.Label L_Desctription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_Sell;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.CheckBox CHK_PaymentByCard;
    }
}