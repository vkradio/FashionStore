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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_SelectDateOfSale));
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
            resources.ApplyResources(this.DAT_Date, "DAT_Date");
            this.DAT_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DAT_Date.Name = "DAT_Date";
            // 
            // L_Desctription
            // 
            resources.ApplyResources(this.L_Desctription, "L_Desctription");
            this.L_Desctription.Name = "L_Desctription";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // B_Sell
            // 
            resources.ApplyResources(this.B_Sell, "B_Sell");
            this.B_Sell.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Sell.Name = "B_Sell";
            this.B_Sell.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            resources.ApplyResources(this.B_Cancel, "B_Cancel");
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // CHK_PaymentByCard
            // 
            resources.ApplyResources(this.CHK_PaymentByCard, "CHK_PaymentByCard");
            this.CHK_PaymentByCard.Name = "CHK_PaymentByCard";
            this.CHK_PaymentByCard.UseVisualStyleBackColor = true;
            // 
            // FRM_SelectDateOfSale
            // 
            this.AcceptButton = this.B_Sell;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
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