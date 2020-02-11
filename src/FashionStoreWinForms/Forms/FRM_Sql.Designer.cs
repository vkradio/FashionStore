namespace FashionStoreWinForms.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_Sql));
            this.T_Sql = new System.Windows.Forms.TextBox();
            this.B_Run = new System.Windows.Forms.Button();
            this.T_Result = new System.Windows.Forms.TextBox();
            this.B_Select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // T_Sql
            // 
            resources.ApplyResources(this.T_Sql, "T_Sql");
            this.T_Sql.Name = "T_Sql";
            // 
            // B_Run
            // 
            resources.ApplyResources(this.B_Run, "B_Run");
            this.B_Run.Name = "B_Run";
            this.B_Run.UseVisualStyleBackColor = true;
            this.B_Run.Click += new System.EventHandler(this.B_Run_Click);
            // 
            // T_Result
            // 
            resources.ApplyResources(this.T_Result, "T_Result");
            this.T_Result.Name = "T_Result";
            // 
            // B_Select
            // 
            resources.ApplyResources(this.B_Select, "B_Select");
            this.B_Select.Name = "B_Select";
            this.B_Select.UseVisualStyleBackColor = true;
            this.B_Select.Click += new System.EventHandler(this.B_Select_Click);
            // 
            // FRM_Sql
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.B_Select);
            this.Controls.Add(this.T_Result);
            this.Controls.Add(this.B_Run);
            this.Controls.Add(this.T_Sql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FRM_Sql";
            this.ShowInTaskbar = false;
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