using System;
using System.Windows.Forms;

using dress.su.Properties;

namespace dress.su.Forms
{
    public partial class FRM_UserSettings : Form
    {
        void FRM_UserSettings_Load(object sender, EventArgs e)
        {
            T_DbPath.Text = Settings.Default.DbPath;
            T_BackupFolder.Text = Settings.Default.BackupFolder;
        }
        void B_Save_Click(object sender, EventArgs e)
        {
            Settings.Default.DbPath = T_DbPath.Text;
            Settings.Default.BackupFolder = T_BackupFolder.Text;
            Settings.Default.Save();
            MessageBox.Show("Некоторые настройки требуют перезапуска программы для вступления в силу.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void B_SelectBackupFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку резервного хранения";
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    T_BackupFolder.Text = dlg.SelectedPath;
            }
        }

        public FRM_UserSettings()
        {
            InitializeComponent();
        }
    }
}
