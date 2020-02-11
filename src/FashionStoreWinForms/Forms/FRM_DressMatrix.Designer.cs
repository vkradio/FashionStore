namespace FashionStoreWinForms.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_DressMatrix));
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
            resources.ApplyResources(this.T_Name, "T_Name");
            this.T_Name.Name = "T_Name";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // B_Delete
            // 
            resources.ApplyResources(this.B_Delete, "B_Delete");
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            resources.ApplyResources(this.B_Save, "B_Save");
            this.B_Save.Name = "B_Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Read
            // 
            resources.ApplyResources(this.B_Read, "B_Read");
            this.B_Read.Name = "B_Read";
            this.B_Read.UseVisualStyleBackColor = true;
            this.B_Read.Click += new System.EventHandler(this.B_Read_Click);
            // 
            // T_ReadId
            // 
            resources.ApplyResources(this.T_ReadId, "T_ReadId");
            this.T_ReadId.Name = "T_ReadId";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // T_CellsX
            // 
            resources.ApplyResources(this.T_CellsX, "T_CellsX");
            this.T_CellsX.Name = "T_CellsX";
            // 
            // T_CellsY
            // 
            resources.ApplyResources(this.T_CellsY, "T_CellsY");
            this.T_CellsY.Name = "T_CellsY";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FRM_DressMatrix
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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