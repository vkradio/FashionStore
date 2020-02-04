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
            this.B_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Save.Location = new System.Drawing.Point(348, 102);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 0;
            this.B_Save.Text = "Сохранить";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к файлу БД";
            // 
            // T_DbPath
            // 
            this.T_DbPath.Location = new System.Drawing.Point(12, 25);
            this.T_DbPath.Name = "T_DbPath";
            this.T_DbPath.Size = new System.Drawing.Size(411, 20);
            this.T_DbPath.TabIndex = 2;
            // 
            // T_BackupFolder
            // 
            this.T_BackupFolder.Location = new System.Drawing.Point(12, 68);
            this.T_BackupFolder.Name = "T_BackupFolder";
            this.T_BackupFolder.Size = new System.Drawing.Size(384, 20);
            this.T_BackupFolder.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Папка для резервного копирования";
            // 
            // B_SelectBackupFolder
            // 
            this.B_SelectBackupFolder.Location = new System.Drawing.Point(396, 68);
            this.B_SelectBackupFolder.Name = "B_SelectBackupFolder";
            this.B_SelectBackupFolder.Size = new System.Drawing.Size(27, 20);
            this.B_SelectBackupFolder.TabIndex = 5;
            this.B_SelectBackupFolder.Text = "...";
            this.B_SelectBackupFolder.UseVisualStyleBackColor = true;
            this.B_SelectBackupFolder.Click += new System.EventHandler(this.B_SelectBackupFolder_Click);
            // 
            // FRM_UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 137);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пользовательские настройки";
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