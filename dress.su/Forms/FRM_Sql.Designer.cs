namespace dress.su.Forms
{
    partial class FRM_Sql
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
            this.T_Sql = new System.Windows.Forms.TextBox();
            this.B_Run = new System.Windows.Forms.Button();
            this.T_Result = new System.Windows.Forms.TextBox();
            this.B_Select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // T_Sql
            // 
            this.T_Sql.Location = new System.Drawing.Point(13, 13);
            this.T_Sql.Multiline = true;
            this.T_Sql.Name = "T_Sql";
            this.T_Sql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_Sql.Size = new System.Drawing.Size(433, 239);
            this.T_Sql.TabIndex = 0;
            // 
            // B_Run
            // 
            this.B_Run.Location = new System.Drawing.Point(371, 258);
            this.B_Run.Name = "B_Run";
            this.B_Run.Size = new System.Drawing.Size(75, 23);
            this.B_Run.TabIndex = 1;
            this.B_Run.Text = "Исполнить";
            this.B_Run.UseVisualStyleBackColor = true;
            this.B_Run.Click += new System.EventHandler(this.B_Run_Click);
            // 
            // T_Result
            // 
            this.T_Result.Location = new System.Drawing.Point(13, 303);
            this.T_Result.Multiline = true;
            this.T_Result.Name = "T_Result";
            this.T_Result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_Result.Size = new System.Drawing.Size(433, 164);
            this.T_Result.TabIndex = 2;
            // 
            // B_Select
            // 
            this.B_Select.Location = new System.Drawing.Point(290, 258);
            this.B_Select.Name = "B_Select";
            this.B_Select.Size = new System.Drawing.Size(75, 23);
            this.B_Select.TabIndex = 3;
            this.B_Select.Text = "Выбрать";
            this.B_Select.UseVisualStyleBackColor = true;
            this.B_Select.Click += new System.EventHandler(this.B_Select_Click);
            // 
            // FRM_Sql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 479);
            this.Controls.Add(this.B_Select);
            this.Controls.Add(this.T_Result);
            this.Controls.Add(this.B_Run);
            this.Controls.Add(this.T_Sql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FRM_Sql";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox T_Sql;
        private System.Windows.Forms.Button B_Run;
        private System.Windows.Forms.TextBox T_Result;
        private System.Windows.Forms.Button B_Select;
    }
}