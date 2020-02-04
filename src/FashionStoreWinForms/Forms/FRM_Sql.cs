using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

using MainLibrary.misc;

namespace FashionStoreWinForms.Forms
{
    public partial class FRM_Sql : Form
    {
        void B_Run_Click(object sender, EventArgs e)
        {
            if (T_Sql.Text.Trim() == string.Empty)
            {
                T_Result.Text = "Error: no SQL text.";
                return;
            }

            using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = T_Sql.Text;
                    int affected = 0;
                    try
                    {
                        affected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        T_Result.Text = "Exception: " + ex.ToString();
                        return;
                    }

                    T_Result.Text = "Affected rows: " + affected;
                }
            }
        }
        void B_Select_Click(object sender, EventArgs e)
        {
            if (T_Sql.Text.Trim() == string.Empty)
            {
                T_Result.Text = "Error: no SQL text.";
                return;
            }

            using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = T_Sql.Text;
                    DataSet ds = new DataSet();
                    try
                    {
                        using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                            a.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        T_Result.Text = "Exception: " + ex.ToString();
                        return;
                    }

                    string result = string.Empty;
                    foreach (DataTable table in ds.Tables)
                    {
                        if (result != string.Empty)
                            result += Environment.NewLine + Environment.NewLine;

                        result += "Table: " + table.TableName + Environment.NewLine;
                        foreach (DataColumn col in table.Columns)
                            result += col.ColumnName + "\t";
                        result += Environment.NewLine + "-------------------------------------------" + Environment.NewLine;

                        foreach (DataRow row in table.Rows)
                        {
                            foreach (DataColumn col in table.Columns)
                                result += row[col].ToString() + "\t";
                            result += Environment.NewLine;
                        }
                        result += "Rows: " + table.Rows.Count;
                    }

                    T_Result.Text = result != string.Empty ? result : "No result.";
                }
            }
        }

        public FRM_Sql()
        {
            InitializeComponent();
        }
    }
}
