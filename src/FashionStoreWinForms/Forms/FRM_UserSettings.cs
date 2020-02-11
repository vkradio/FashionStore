using System;
using System.Windows.Forms;

using FashionStoreWinForms.Properties;

namespace FashionStoreWinForms.Forms
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
            MessageBox.Show(Resources.SOME_SETTINGS_REQUIRE_TO_RELOAD_PROGRAM, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        void B_SelectBackupFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = Resources.CHOOSE_BACKUP_FOLDER;
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
