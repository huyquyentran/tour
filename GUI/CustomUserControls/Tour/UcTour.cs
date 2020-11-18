using BLL;
using Core.Common;
using Core.Models;
using System;
using System.Collections;
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
        public int? TourId;
        public string SearchBy;
        public int? TourPriceId;
        public IList<Location> locations;
        public IList<TourLocations> tourLocations;
        public Dictionary<string, string> dictionarySearchBy = new Dictionary<string, string>()
        {
            {"Id", "Mã"},
            {"Name", "Tên"},
            {"Description", "Mô tả"},
        };
        public UcTour()
        {
            //UseEffect no dependency
            InitializeComponent();
            LoadTourDataGridView();
            LoadAllLocationDataGridView();
            LoadComboBoxTourType();
            LoadComboBoxSearchBy();
            tcTourPriceLocation.Enabled = false;
            tbTourID.Enabled = false;
            tbTourPriceID.Enabled = false;
        }
        // Handle Event Tour
        public void LoadTourDataGridView(string type = null, string value = null)
        {
            var tourData = TourBLL.ListTours(type, value);
            var dataSource = tourData.Select(t => new TourDataSource(
                                        t.Id,
                                        t.CurrentPrice,
                                        t.Name,
                                        t.Description,
                                        t.TourType.Name,
                                        t.TourTypeId)).ToList();
            dgvTourList.DataSource = dataSource;

            //ConfigTourDataGridView
            dgvTourList.Columns["Id"].Visible = false;
            dgvTourList.Columns["TourTypeId"].Visible = false;
            dgvTourList.Columns["Name"].HeaderText = "Tên";
            dgvTourList.Columns["CurrentPrice"].HeaderText = "Giá gốc";
            dgvTourList.Columns["TourTypeName"].HeaderText = "Loại";
            dgvTourList.Columns["Description"].HeaderText = "Mô tả";
        }
        public void LoadTourForm(TourDataSource data)
        {
            if (dgvTourList.SelectedRows.Count > 0)
            {
                tbTourID.Text = TourId.ToString();
                tbTourName.Text = data.Name;
                tbTourPrice.Text = data.CurrentPrice.ToString();
                tbTourDescription.Text = data.Description;
                cbTourType.SelectedValue = data.TourTypeId;
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
        public void LoadComboBoxTourType()
        {
            cbTourType.DataSource = TourTypeBLL.ListTourTypes();
            cbTourType.DisplayMember = "Name";
            cbTourType.ValueMember = "Id";
        }
        public void LoadComboBoxSearchBy()
        {
            cbTourSearchBy.DataSource = new BindingSource(dictionarySearchBy, null);
            cbTourSearchBy.DisplayMember = "Value";
            cbTourSearchBy.ValueMember = "Key";
        }
        private void btnTourSearch_Click(object sender, EventArgs e)
        {
            var type = ((KeyValuePair<string, string>) cbTourSearchBy.SelectedItem).Key;
            string value = tbTourSearchInput.Text;
            LoadTourDataGridView(type, value);
        }
        //private void cbTourSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(((KeyValuePair<string, string>)cbTourSearchBy.SelectedItem).Key);
        //}
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
                int id = Int32.Parse(tbTourID.Text);
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
        // Handle Event Price
        public void LoadTourPriceDataGridView()
        {
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                var tourPriceData = TourPriceBLL.ListTourPrices(TourId);
                dgvTourPriceList.DataSource = tourPriceData.ToList();

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
        private void dgvTourPriceList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTourPriceList.SelectedRows.Count > 0)
            {
                var row = dgvTourPriceList.SelectedRows[0];
                var data = (TourPrice)row.DataBoundItem;

                TourPriceId = data.Id;
                LoadTourPriceForm(data);
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
        public void LoadTourPriceForm(TourPrice data)
        {
            TourPriceId = data.Id;
            tbTourPriceID.Text = data.Id.ToString();
            tbTourPriceValue.Text = data.Price.ToString();
            tbTourPriceNote.Text = data.Note.ToString();

            dtpTourPriceStartDate.Value = data.StartDate;
            dtpTourPriceEndDate.Value = data.EndDate;
        }
        private void btnTourPriceAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var tourPrice = new TourPrice(
                        TourId.Value,
                        dtpTourPriceStartDate.Value,
                        dtpTourPriceEndDate.Value,
                        Int32.Parse(tbTourPriceValue.Text),
                        tbTourPriceNote.Text
                    );
                TourPriceBLL.Add(tourPrice);
                LoadTourPriceDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTourPriceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var tourPrice = new TourPrice(
                        TourId.Value,
                        dtpTourPriceStartDate.Value,
                        dtpTourPriceEndDate.Value,
                        Int32.Parse(tbTourPriceValue.Text),
                        tbTourPriceNote.Text
                    );
                TourPriceBLL.Update(TourPriceId, tourPrice);
                LoadTourPriceDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Handle Event Tour Location
        public void LoadAllLocationDataGridView()
        {
            var locationsData = LocationBLL.ListLocations();
            locations = locationsData.ToList();
            dgvTourLocationListAll.DataSource = locations;

            dgvTourLocationListAll.Columns["Id"].Visible = false;
            dgvTourLocationListAll.Columns["TourLocations"].Visible = false;

            dgvTourLocationListAll.Columns["Name"].HeaderText = "Tên";
        }
        public void LoadLocationDataGridView()
        {
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                var tourLocationsData = TourLocationBLL.ListTourLocationsByTourId(TourId);
                tourLocations = tourLocationsData.ToList();
                var dataSource = tourLocationsData.Select(t => new TourLocationDataSource(
                                        t.TourId,
                                        t.LocationId,
                                        t.Location.Name,
                                        t.Order)).OrderBy(t => t.Order).ToList();
                dgvTourLocationList.DataSource = dataSource;

                dgvTourLocationList.Columns["LocationId"].Visible = false;
                dgvTourLocationList.Columns["TourId"].Visible = false;
                dgvTourLocationList.Columns["Order"].HeaderText = "Thứ tự";
                dgvTourLocationList.Columns["Name"].HeaderText = "Tên";
            }
            else
            {
                //TODO
            }
        }
        private void btnTourLocationCancel_Click(object sender, EventArgs e)
        {
            LoadLocationDataGridView();
        }
        private void btnTourLocationAdd_Click(object sender, EventArgs e)
        {
            if (dgvTourLocationListAll.SelectedRows.Count > 0)
            {
                var locationItem = (Location)dgvTourLocationListAll.SelectedRows[0].DataBoundItem;
                var tourLocationItem = tourLocations.SingleOrDefault(tl => tl.LocationId == locationItem.Id);
                if (tourLocationItem == null)
                {
                    var newTourLocation = new TourLocations
                    {
                        TourId = TourId.Value,
                        LocationId = locationItem.Id,
                        Location = LocationBLL.LocationById(locationItem.Id),
                    };
                    tourLocations.Add(newTourLocation);
                    var dataSource = tourLocations.Select((t, index) => new TourLocationDataSource(
                                        t.TourId,
                                        t.LocationId,
                                        t.Location.Name,
                                        index + 1)).ToList();
                    dgvTourLocationList.DataSource = dataSource;
                    dgvTourLocationListAll.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Đã có địa điểm này");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn địa điểm từ danh sách tất cả địa điểm");
            }
        }
        private void btnTourLocationRemove_Click(object sender, EventArgs e)
        {
            if (dgvTourLocationList.SelectedRows.Count > 0)
            {
                var tourLocationItem = (TourLocationDataSource)dgvTourLocationList.SelectedRows[0].DataBoundItem;
                var tourLocationRemove = tourLocations.SingleOrDefault(tl => tl.LocationId == tourLocationItem.LocationId);
                tourLocations.Remove(tourLocationRemove);
                var dataSource = tourLocations.Select((t, index) => new TourLocationDataSource(
                                            t.TourId,
                                            t.LocationId,
                                            t.Location.Name,
                                            index + 1)).ToList();
                dgvTourLocationList.DataSource = dataSource;
                dgvTourLocationList.ClearSelection();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn địa điểm từ danh sách địa điểm của Tour");
            }
        }
        private void btnTourLocationMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvTourLocationList.SelectedRows.Count > 0)
            {
                var tourLocationItem = (TourLocationDataSource)dgvTourLocationList.SelectedRows[0].DataBoundItem;
                int index = tourLocations.ToList().FindIndex(t => t.LocationId == tourLocationItem.LocationId);
                if (index == 0)
                {
                    MessageBox.Show("Không thể up");
                    return;
                }

                if (index != -1)
                {
                    tourLocations = tourLocations.Swap(index, index - 1);
                }
                var dataSource = tourLocations.Select((t, i) => new TourLocationDataSource(
                                        t.TourId,
                                        t.LocationId,
                                        t.Location.Name,
                                        i + 1)).ToList();
                dgvTourLocationList.DataSource = dataSource;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn địa điểm từ danh sách địa điểm của Tour");
            }
        }
        private void btnTourLocationMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvTourLocationList.SelectedRows.Count > 0)
            {
                var tourLocationItem = (TourLocationDataSource)dgvTourLocationList.SelectedRows[0].DataBoundItem;
                int index = tourLocations.ToList().FindIndex(t => t.LocationId == tourLocationItem.LocationId);
                if (index == tourLocations.Count - 1)
                {
                    MessageBox.Show("Không thể down");
                    return;
                }
                if (index != -1)
                {
                    tourLocations = tourLocations.Swap(index, index + 1);
                }
                var dataSource = tourLocations.Select((t, i) => new TourLocationDataSource(
                                        t.TourId,
                                        t.LocationId,
                                        t.Location.Name,
                                        i + 1)).ToList();
                dgvTourLocationList.DataSource = dataSource;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn địa điểm từ danh sách địa điểm của Tour");
            }
        }
        private void btnTourLocationSave_Click(object sender, EventArgs e)
        {
            var dataUpdate = tourLocations.Select((t, i) => new TourLocations
            {
                LocationId = t.LocationId,
                TourId = t.TourId,
                Order = i + 1,
            }).ToList();
            TourLocationBLL.UpdateRange(TourId, dataUpdate);
            LoadLocationDataGridView();
            MessageBox.Show("Lưu thành công");
        }


    }

    public class TourDataSource
    {
        public TourDataSource(int id, int currentPrice, string name, string description, string tourTypeName, int tourTypeId)
        {
            Id = id;
            CurrentPrice = currentPrice;
            Name = name;
            Description = description;
            TourTypeName = tourTypeName;
            TourTypeId = tourTypeId;
        }

        public int Id { get; set; }
        public int TourTypeId { get; set; }
        public int CurrentPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TourTypeName { get; set; }
    }
    public class TourLocationDataSource
    {
        public TourLocationDataSource(int tourId, int locationId, string name, int order)
        {
            TourId = tourId;
            LocationId = locationId;
            Name = name;
            Order = order;
        }

        public int TourId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
