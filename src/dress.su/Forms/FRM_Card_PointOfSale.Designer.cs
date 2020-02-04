namespace dress.su.Forms
{
    partial class FRM_Card_PointOfSale
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
            this.T_ReadId = new System.Windows.Forms.TextBox();
            this.B_Read = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.T_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // T_ReadId
            // 
            this.T_ReadId.Location = new System.Drawing.Point(12, 16);
            this.T_ReadId.Name = "T_ReadId";
            this.T_ReadId.Size = new System.Drawing.Size(53, 20);
            this.T_ReadId.TabIndex = 0;
            // 
            // B_Read
            // 
            this.B_Read.Location = new System.Drawing.Point(72, 13);
            this.B_Read.Name = "B_Read";
            this.B_Read.Size = new System.Drawing.Size(75, 23);
            this.B_Read.TabIndex = 1;
            this.B_Read.Text = "Читать";
            this.B_Read.UseVisualStyleBackColor = true;
            this.B_Read.Click += new System.EventHandler(this.B_Read_Click);
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(153, 13);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 2;
            this.B_Save.Text = "Сохранить";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(234, 13);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(75, 23);
            this.B_Delete.TabIndex = 3;
            this.B_Delete.Text = "Удалить";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Наименование";
            // 
            // T_Name
            // 
            this.T_Name.Location = new System.Drawing.Point(101, 50);
            this.T_Name.Name = "T_Name";
            this.T_Name.Size = new System.Drawing.Size(242, 20);
            this.T_Name.TabIndex = 5;
            // 
            // FRM_Card_PointOfSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 88);
            this.Controls.Add(this.T_Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_Read);
            this.Controls.Add(this.T_ReadId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FRM_Card_PointOfSale";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Точка продаж";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_ReadId;
        private System.Windows.Forms.Button B_Read;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox T_Name;
    }
}