using System;
using System.Data;
using System.Windows.Forms;

using ApplicationCore.Entities;
using FashionStoreWinForms.Properties;
using FashionStoreWinForms.Sys;

namespace FashionStoreWinForms.Widgets.PageViewSku
{
    public partial class PanelViewSku : UserControl
    {
        const int c_maxPositions = 100;

        string _namePart;

        void Search(bool in_showMsgNotFound)
        {
            PAN_Lists.Controls.Clear();

            DataTable table = SkuInStock.RestoreAllByNameBeginning(Registry.CurrentPointOfSale.Id, _namePart);
            if (in_showMsgNotFound && table.Rows.Count == 0)
            {
                MessageBox.Show(this, Resources.NOTHING_FOUND, Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (table.Rows.Count != 0)
            {
                if (table.Rows.Count > c_maxPositions)
                    MessageBox.Show(this, string.Format(Resources.FOUND_TOO_MUCH, table.Rows.Count, c_maxPositions), Resources.MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.SuspendLayout();

                Cursor oldCur = this.Cursor;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    ManageSkuTable panTable = new ManageSkuTable(table, c_maxPositions);
                    panTable.Parent = PAN_Lists;
                    panTable.UpdateHeight();
                }
                finally
                {
                    this.Cursor = oldCur;
                }

                this.ResumeLayout();
            }
        }

        void B_Search_Click(object sender, EventArgs e)
        {
            if (T_NamePart.Text == string.Empty)
            {
                if (MessageBox.Show(this, Resources.ASK_SHOW_ALL_SKUS_IN_THIS_STOCK, Resources.CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            _namePart = T_NamePart.Text;
            try
            {
                B_Search.Enabled = false;
                Application.DoEvents();

                Search(true);
            }
            finally
            {
                B_Search.Enabled = true;
            }
        }
        void T_NamePart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                B_Search_Click(sender, null);
        }

        public PanelViewSku()
        {
            InitializeComponent();
        }

        public TextBox T_NamePartExt { get { return T_NamePart; } }

        public void UpdateWOldSearch() { Search(false); }
    }
}
