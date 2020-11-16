using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace GUI.Staff
{
    public partial class UcStaff : UserControl
    {
        public UcStaff()
        {
            InitializeComponent();
        }

        private void loadListStaff()
        {
            dataGridViewStaff.DataSource = StaffBLL.ListStaff();
        }

        private void UcStaff_Load(object sender, EventArgs e)
        {
            loadListStaff();
        }
    }
}
