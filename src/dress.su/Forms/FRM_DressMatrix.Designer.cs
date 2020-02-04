namespace dress.su.Forms
{
    partial class FRM_DressMatrix
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
            this.T_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.B_Delete = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Read = new System.Windows.Forms.Button();
            this.T_ReadId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.T_CellsX = new System.Windows.Forms.TextBox();
            this.T_CellsY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // T_Name
            // 
            this.T_Name.Location = new System.Drawing.Point(99, 48);
            this.T_Name.Name = "T_Name";
            this.T_Name.Size = new System.Drawing.Size(208, 20);
            this.T_Name.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Наименование";
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(232, 11);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(75, 23);
            this.B_Delete.TabIndex = 9;
            this.B_Delete.Text = "Удалить";
            this.B_Delete.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(151, 11);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 8;
            this.B_Save.Text = "Сохранить";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Read
            // 
            this.B_Read.Location = new System.Drawing.Point(70, 11);
            this.B_Read.Name = "B_Read";
            this.B_Read.Size = new System.Drawing.Size(75, 23);
            this.B_Read.TabIndex = 7;
            this.B_Read.Text = "Читать";
            this.B_Read.UseVisualStyleBackColor = true;
            this.B_Read.Click += new System.EventHandler(this.B_Read_Click);
            // 
            // T_ReadId
            // 
            this.T_ReadId.Location = new System.Drawing.Point(10, 14);
            this.T_ReadId.Name = "T_ReadId";
            this.T_ReadId.Size = new System.Drawing.Size(53, 20);
            this.T_ReadId.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "X";
            // 
            // T_CellsX
            // 
            this.T_CellsX.Location = new System.Drawing.Point(99, 69);
            this.T_CellsX.Multiline = true;
            this.T_CellsX.Name = "T_CellsX";
            this.T_CellsX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_CellsX.Size = new System.Drawing.Size(208, 59);
            this.T_CellsX.TabIndex = 13;
            // 
            // T_CellsY
            // 
            this.T_CellsY.Location = new System.Drawing.Point(99, 134);
            this.T_CellsY.Multiline = true;
            this.T_CellsY.Name = "T_CellsY";
            this.T_CellsY.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_CellsY.Size = new System.Drawing.Size(208, 59);
            this.T_CellsY.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Y";
            // 
            // FRM_DressMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 205);
            this.Controls.Add(this.T_CellsY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.T_CellsX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.T_Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_Read);
            this.Controls.Add(this.T_ReadId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FRM_DressMatrix";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тип сетки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_Delete;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Read;
        private System.Windows.Forms.TextBox T_ReadId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox T_CellsX;
        private System.Windows.Forms.TextBox T_CellsY;
        private System.Windows.Forms.Label label3;
    }
}