using System;
using System.Data.SQLite;
using System.Windows.Forms;

using ApplicationCore.Entities;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_Card_PointOfSale : Form
    {
        const string c_errConstraint    = "Невозможно удалить объект, у него имеются подчиненные объекты.";
        const string c_errGeneric       = "Не удалось удалить объект. Технический код отказа: {0}.";

        void Clear()
        {
            T_Name.Text = string.Empty;
        }
        PointOfSale GetObject()
        {
            PointOfSale result = null;
            int id;
            if (!int.TryParse(T_ReadId.Text, out id))
            {
                MessageBox.Show("Неверный ID.", "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return result;
            }

            result = PointOfSale.Restore(id);
            if (result == null)
            {
                MessageBox.Show("Объект не найден.", "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return result;
            }

            return result;
        }

        void B_Read_Click(object sender, EventArgs e)
        {
            PointOfSale o = GetObject();
            if (o == null)
            {
                Clear();
                return;
            }

            T_Name.Text = o.Name;
        }
        void B_Save_Click(object sender, EventArgs e)
        {
            PointOfSale o;
            if (T_ReadId.Text == string.Empty)
                o = new PointOfSale();
            else
                o = GetObject();

            o.Name = T_Name.Text;
            o.Flush();

            T_ReadId.Text = o.Id.ToString();
        }
        void B_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить объект?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            PointOfSale o = GetObject();
            if (o != null)
            {
                try
                {
                    PointOfSale.Delete(o);
                }
                catch (SQLiteException ex)
                {
                    //if (ex.ErrorCode == SQLiteErrorCode.Constraint)
                    if (ex.ResultCode == SQLiteErrorCode.Constraint)
                        MessageBox.Show(this, c_errConstraint, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show(this, string.Format(c_errGeneric, ex.ErrorCode), "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            Clear();
            T_ReadId.Text = string.Empty;
        }

        public FRM_Card_PointOfSale()
        {
            InitializeComponent();
        }
    }
}
