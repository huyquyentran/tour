using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Staff;
using MaterialSkin;
using MaterialSkin.Controls;

namespace GUI
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }


        private void ShowInMainContent(UserControl uc)
        {
            this.pnlMainContent.Controls.Clear();
            this.pnlMainContent.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            ShowInMainContent(new UcStaff());
        }
    }
}
