using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            if(ex.Data.Count == 0 )
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }                
            foreach (DictionaryEntry item in ex.Data)
            {
                message += item.Value?.ToString();
                message += Environment.NewLine;
            }
            MessageBox.Show(message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void BindEnumToDataGridViewCombobox<T>(this DataGridViewComboBoxColumn comboBox)
        {
            var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(value => new
                {
                    Description = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? value.ToString(),
                    Value = value
                })
                .OrderBy(item => item.Value.ToString())
                .ToList();

            comboBox.DataSource = list;
            comboBox.DisplayMember = "Description";
            comboBox.ValueMember = "Value";
        }
    }
}
