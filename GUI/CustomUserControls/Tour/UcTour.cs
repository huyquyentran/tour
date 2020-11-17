using BLL;
using Core.Models;
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
        public int? TourId = 1;
        public bool FlagEnabled = false;
        public UcTour()
        {
            //UseEffect no dependency
            InitializeComponent();
            LoadTourDataGridView();
            LoadAllLocationDataGridView();
            LoadComboBoxTourType();
            tcTourPriceLocation.Enabled = false;
            tbTourID.Enabled = false;
            tbTourPriceID.Enabled = false;
        }
        public void LoadTourDataGridView(string name = null)
        {
            var tourData = TourBLL.ListTours(name);
            var dataSource = tourData.Select(t => new TourDataSource(
                                        t.Id,
                                        t.CurrentPrice,
                                        t.Name,
                                        t.Description,
                                        t.TourType.Name)).ToList();
            dgvTourList.DataSource = dataSource;

            //ConfigTourDataGridView
            dgvTourList.Columns["Id"].Visible = false;
            dgvTourList.Columns["Name"].HeaderText = "Tên";
            dgvTourList.Columns["CurrentPrice"].HeaderText = "Giá gốc";
            dgvTourList.Columns["TourTypeName"].HeaderText = "Loại";
            dgvTourList.Columns["Description"].HeaderText = "Mô tả";
        }
        public void LoadTourPriceDataGridView()
        {   
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                var tourPriceData = TourPriceBLL.ListTourPrices(TourId);
                dgvTourPriceList.DataSource = tourPriceData;

                dgvTourPriceList.Columns["Id"].Visible = false;
                dgvTourPriceList.Columns["TourId"].Visible = false;
                dgvTourPriceList.Columns["Tour"].Visible = false;

                dgvTourPriceList.Columns["StartDate"].HeaderText = "Bắt đầu";
                dgvTourPriceList.Columns["EndDate"].HeaderText = "Kết thúc";
                dgvTourPriceList.Columns["Price"].HeaderText = "Giá";
                dgvTourPriceList.Columns["Note"].HeaderText = "Ghi chú";
            }
            else
            {
                //TODO
            }    
        }
        public void LoadAllLocationDataGridView()
        {
            var locationsData = LocationBLL.ListLocations(null);
            dgvTourLocationListAll.DataSource = locationsData;

            dgvTourLocationListAll.Columns["Id"].Visible = false;
            dgvTourLocationListAll.Columns["TourLocations"].Visible = false;

            dgvTourLocationList.Columns["Name"].HeaderText = "Tên";
        }
        public void LoadLocationDataGridView()
        {
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                var locationsData = LocationBLL.ListLocations(TourId);
                var dataSource = locationsData.Select(t => new TourLocationDataSource(
                                        t.Id,
                                        t.Name,
                                        t.TourLocations.FirstOrDefault().Order)).ToList();
                dgvTourLocationList.DataSource = dataSource;

                dgvTourLocationList.Columns["Id"].Visible = false;
                dgvTourLocationList.Columns["Order"].HeaderText = "Thứ tự";
                dgvTourLocationList.Columns["Name"].HeaderText = "Tên";
            }
            else
            {
                //TODO
            }
        }
        public void LoadTourForm(TourDataSource data)
        {
            if(dgvTourList.SelectedRows.Count > 0)
            {
                tbTourID.Text = TourId.ToString();
                tbTourName.Text = data.Name;
                tbTourPrice.Text = data.CurrentPrice.ToString();
                tbTourDescription.Text = data.Description;
                cbTourType.Text = data.TourTypeName;
            }
            else
            {
                tbTourID.Text = "";
                tbTourName.Text = "";
                tbTourPrice.Text = "";
                tbTourDescription.Text = "";

                dgvTourPriceList.DataSource = null;
                dgvTourLocationList.DataSource = null;
                dgvTourLocationListAll.DataSource = null;
            }    
        }
        public void LoadTourPriceForm()
        {
            if (dgvTourPriceList.SelectedRows.Count > 0)
            {
                var row = dgvTourPriceList.SelectedRows[0];
                var data = (TourPrice) row.DataBoundItem;

                tbTourPriceID.Text = data.Id.ToString();
                tbTourPriceValue.Text = data.Price.ToString();
                tbTourPriceNote.Text = data.Note.ToString();

                dtpTourPriceStartDate.Value = data.StartDate;
                dtpTourPriceEndDate.Value = data.EndDate;
            }
            else
            {
                tbTourPriceID.Text = "";
                tbTourPriceValue.Text = "";
                tbTourPriceNote.Text = "";

                dtpTourPriceStartDate.Value = DateTime.UtcNow;
                dtpTourPriceEndDate.Value = DateTime.UtcNow;
            }    
        }
        public void LoadComboBoxTourType()
        {
            cbTourType.DataSource = TourTypeBLL.ListTourTypes();
            cbTourType.DisplayMember = "Name";
            cbTourType.ValueMember = "Id";
        }
        private void dgvTourList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTourList.SelectedRows.Count > 0)
            {
                var row = dgvTourList.SelectedRows[0];
                var data = (TourDataSource)row.DataBoundItem;
                TourId = data.Id;
                LoadTourForm(data);
                LoadTourPriceDataGridView();
                LoadLocationDataGridView();
                tcTourPriceLocation.Enabled = true;
            }
        }
        private void dgvTourPriceList_SelectionChanged(object sender, EventArgs e)
        {
            LoadTourPriceForm();
        }
        private void btnTourAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var tour = new Core.Models.Tour(tbTourName.Text,
                                                    Int32.Parse(tbTourPrice.Text),
                                                    tbTourDescription.Text,
                                                    Int32.Parse(cbTourType.SelectedValue.ToString()));
                TourBLL.Add(tour);
                LoadTourDataGridView();
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá tour không được chứa chữ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTourEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var tour = new Core.Models.Tour(tbTourName.Text,
                                                    Int32.Parse(tbTourPrice.Text),
                                                    tbTourDescription.Text,
                                                    Int32.Parse(cbTourType.SelectedValue.ToString()));
                int id  = Int32.Parse(tbTourID.Text);
                TourBLL.Update(id, tour);
                LoadTourDataGridView();
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá tour không được chứa chữ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTourPriceAdd_Click(object sender, EventArgs e)
        {
            //TODO
        }
        private void btnTourPriceEdit_Click(object sender, EventArgs e)
        {
            //TODO
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
    public class TourLocationDataSource
    {
        public TourLocationDataSource(int id, string name, int order)
        {
            Id = id;
            Name = name;
            Order = order;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
