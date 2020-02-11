namespace FashionStoreWinForms.Forms
{
    partial class FRM_UserSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_UserSettings));
            this.B_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.T_DbPath = new System.Windows.Forms.TextBox();
            this.T_BackupFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.B_SelectBackupFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B_Save
            // 
            resources.ApplyResources(this.B_Save, "B_Save");
            this.B_Save.Name = "B_Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // T_DbPath
            // 
            resources.ApplyResources(this.T_DbPath, "T_DbPath");
            this.T_DbPath.Name = "T_DbPath";
            // 
            // T_BackupFolder
            // 
            resources.ApplyResources(this.T_BackupFolder, "T_BackupFolder");
            this.T_BackupFolder.Name = "T_BackupFolder";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // B_SelectBackupFolder
            // 
            resources.ApplyResources(this.B_SelectBackupFolder, "B_SelectBackupFolder");
            this.B_SelectBackupFolder.Name = "B_SelectBackupFolder";
            this.B_SelectBackupFolder.UseVisualStyleBackColor = true;
            this.B_SelectBackupFolder.Click += new System.EventHandler(this.B_SelectBackupFolder_Click);
            // 
            // FRM_UserSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.B_SelectBackupFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.T_BackupFolder);
            this.Controls.Add(this.T_DbPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FRM_UserSettings";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FRM_UserSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox T_DbPath;
        private System.Windows.Forms.TextBox T_BackupFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_SelectBackupFolder;
    }
}