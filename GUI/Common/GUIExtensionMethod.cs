using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Common
{
    public static class GUIExtensionMethod
    {
        public static DataGridView ShowLoading(this DataGridView dgv, bool IsShowlLoading)
        {
            dgv.Columns.Clear();
            if (IsShowlLoading)
            {
                dgv.DataSource = null;

                dgv.Columns.Add("Loading", "Loading...");
                dgv.Columns["Loading"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }    
            else
            {
                //TODO
            }    

            return dgv;
        }
        public static void HandleError(Exception ex)
        {
            string message = "";
            foreach (DictionaryEntry item in ex.Data)
            {
                message += item.Value?.ToString();
                message += Environment.NewLine;
            }
            MessageBox.Show(message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
