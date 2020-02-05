using System;
using System.Data;
using System.Windows.Forms;

using ApplicationCore.Entities;
using FashionStoreWinForms.Sys;

namespace FashionStoreWinForms.Widgets.PageViewSku
{
    public partial class PanelViewSku : UserControl
    {
        const int c_maxPositions = 100;
        const string c_captionMsg               = "Сообщение";
        const string c_msgWillShowNoMoreThan    = "Найдено {0} позиций. Из-за ограничений памяти будет выведено показано только {1} из них. Для отображения всех нужных позиций вводите более точный поисковый запрос.";

        string _namePart;

        void Search(bool in_showMsgNotFound)
        {
            PAN_Lists.Controls.Clear();

            DataTable table = SkuInStock.RestoreAllByNameBeginning(Registry.CurrentPointOfSale.Id, _namePart);
            if (in_showMsgNotFound && table.Rows.Count == 0)
            {
                MessageBox.Show(this, "Ничего не найдено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (table.Rows.Count != 0)
            {
                if (table.Rows.Count > c_maxPositions)
                    MessageBox.Show(this, string.Format(c_msgWillShowNoMoreThan, table.Rows.Count, c_maxPositions), c_captionMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                if (MessageBox.Show(this, "Будет выведен полный список товара на точке. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
