using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Tour
{
    public partial class UcTour : UserControl
    {
        public UcTour()
        {
            InitializeComponent();
            LoadDataGridView();
            ConfigDataGridView();
            LoadComboBoxTourType();
        }
        public void LoadDataGridView(string name = null)
        {
            var tourData = TourBLL.ListTours(name);
            var dataSource = tourData.Select(t => new TourDataSource(
                                        t.Id,
                                        t.CurrentPrice,
                                        t.Name,
                                        t.Description,
                                        t.TourType.Name)).ToList();
            dgvTourList.DataSource = dataSource;
        }
        public void ConfigDataGridView()
        {
            dgvTourList.Columns["Id"].HeaderText = "Mã";
            dgvTourList.Columns["Id"].Width = 30;
            dgvTourList.Columns["Name"].HeaderText = "Tên";
            dgvTourList.Columns["CurrentPrice"].HeaderText = "Giá gốc";
            dgvTourList.Columns["TourTypeName"].HeaderText = "Loại";
            dgvTourList.Columns["Description"].HeaderText = "Mô tả";
        }
        public void LoadComboBoxTourType()
        {
            cbTourType.DataSource = TourTypeBLL.ListTourTypes();
            cbTourType.DisplayMember = "Name";
            cbTourType.ValueMember = "Id";
        }
        public void LoadForm ()
        {
            if(dgvTourList.SelectedRows.Count > 0)
            {
                var row = dgvTourList.SelectedRows[0];
                var data = (TourDataSource) row.DataBoundItem;

                tbTourID.Text = data.Id.ToString();
                tbTourName.Text = data.Name;
                tbTourPrice.Text = data.CurrentPrice.ToString();
                tbTourDescription.Text = data.Description;
                cbTourType.Text = data.TourTypeName;
            }                
        }

        private void tourDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            LoadForm();
        }
    }

    public class TourDataSource
    {
        public TourDataSource(int id, int currentPrice, string name, string description, string tourTypeName)
        {
            Id = id;
            CurrentPrice = currentPrice;
            Name = name;
            Description = description;
            TourTypeName = tourTypeName;
        }

        public int Id { get; set; }
        public int CurrentPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TourTypeName { get; set; }
    }
}
