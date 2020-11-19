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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Common;

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
            {"TourTypeName", "Thể loại Tour" }
        };
        public UcTour()
        {
            InitializeComponent();
        }
        private void UcTour_Load(object sender, EventArgs e)
        {
            Thread threadLoadTourDataGridView = new Thread(new ThreadStart(()=>LoadTourDataGridView()));
            threadLoadTourDataGridView.Start();
            Thread threadLoadComboBoxTourType = new Thread(new ThreadStart(() => LoadComboBoxTourType()));
            threadLoadComboBoxTourType.Start();
            Thread threadLoadAllLocationDataGridView = new Thread(new ThreadStart(() => LoadAllLocationDataGridView()));
            threadLoadAllLocationDataGridView.Start();
            LoadComboBoxSearchBy();
            tcTourPriceLocation.Enabled = false;
            tbTourID.Enabled = false;
            tbTourPriceID.Enabled = false;
        }
        // Handle Event Tour
        public void LoadTourDataGridView(string type = null, string value = null)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvTourList.ShowLoading(true);
                }));
            }
            var tourData = TourBLL.ListTours(type, value);
            var dataSource = tourData.Select(t => new TourDataSource(
                                        t.Id,
                                        t.CurrentPrice,
                                        t.Name,
                                        t.Description,
                                        t.TourType.Name,
                                        t.TourTypeId)).ToList();
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvTourList.ShowLoading(false);
                    dgvTourList.DataSource = dataSource;
                    //ConfigTourDataGridView
                    dgvTourList.Columns["Id"].Visible = false;
                    dgvTourList.Columns["TourTypeId"].Visible = false;
                    dgvTourList.Columns["Name"].HeaderText = "Tên";
                    dgvTourList.Columns["CurrentPrice"].HeaderText = "Giá gốc";
                    dgvTourList.Columns["TourTypeName"].HeaderText = "Loại";
                    dgvTourList.Columns["Description"].HeaderText = "Mô tả";
                }));
            }

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
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => {
                    cbTourType.Text = "Loading ...";
                }));
            }
            var tourTypes = TourTypeBLL.ListTourTypes();
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>{
            
                    cbTourType.DataSource = tourTypes;
                    cbTourType.DisplayMember = "Name";
                    cbTourType.ValueMember = "Id";
                }));
            }
        }
        public void LoadComboBoxSearchBy()
        {
            cbTourSearchBy.DataSource = new BindingSource(dictionarySearchBy, null);
            cbTourSearchBy.DisplayMember = "Value";
            cbTourSearchBy.ValueMember = "Key";
        }
        private void btnTourSearch_Click(object sender, EventArgs e)
        {
            var type = ((KeyValuePair<string, string>)cbTourSearchBy.SelectedItem).Key;
            string value = tbTourSearchInput.Text;
            Thread threadLoadTourDataGridView = new Thread(new ThreadStart(() => LoadTourDataGridView(type, value)));
            threadLoadTourDataGridView.Start();
        }
        private void btnTourListRefresh_Click(object sender, EventArgs e)
        {
            Thread threadLoadTourDataGridView = new Thread(new ThreadStart(() => LoadTourDataGridView()));
            threadLoadTourDataGridView.Start();
        }
        private void dgvTourList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTourList.SelectedRows.Count > 0)
            {
                var row = dgvTourList.SelectedRows[0];
                var data = (TourDataSource)row.DataBoundItem;
                TourId = data.Id;
                Thread threadLoadLocationDataGridView = new Thread(new ThreadStart(() => LoadLocationDataGridView()));
                threadLoadLocationDataGridView.Start();
                Thread threadLoadTourPriceDataGridView = new Thread(new ThreadStart(() => LoadTourPriceDataGridView()));
                threadLoadTourPriceDataGridView.Start();
                LoadTourForm(data);
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
                Thread threadLoadTourDataGridView = new Thread(new ThreadStart(() => LoadTourDataGridView()));
                threadLoadTourDataGridView.Start();
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
                                                    Int32.Parse(tbTourPrice.Text == "" ? "-1" : tbTourPrice.Text),
                                                    tbTourDescription.Text,
                                                    Int32.Parse(cbTourType.SelectedValue.ToString()));
                int id = Int32.Parse(tbTourID.Text);
                TourBLL.Update(id, tour);

                Thread threadLoadTourDataGridView = new Thread(new ThreadStart(() => LoadTourDataGridView()));
                threadLoadTourDataGridView.Start();
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
        public void LoadTourPriceDataGridView(DateTime? StartDate = null)
        {
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        dgvTourPriceList.ShowLoading(true);
                    }));
                }
                var tourPriceData = TourPriceBLL.ListTourPrices(TourId, StartDate).ToList();
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        dgvTourPriceList.ShowLoading(false);
                        dgvTourPriceList.DataSource = tourPriceData;

                        dgvTourPriceList.Columns["Id"].Visible = false;
                        dgvTourPriceList.Columns["TourId"].Visible = false;
                        dgvTourPriceList.Columns["Tour"].Visible = false;

                        dgvTourPriceList.Columns["StartDate"].HeaderText = "Bắt đầu";
                        dgvTourPriceList.Columns["EndDate"].HeaderText = "Kết thúc";
                        dgvTourPriceList.Columns["Price"].HeaderText = "Giá";
                        dgvTourPriceList.Columns["Note"].HeaderText = "Ghi chú";
                    }));
                }
               
                
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
        private void btnTourPriceRefresh_Click(object sender, EventArgs e)
        {
            Thread threadLoadTourPriceDataGridView = new Thread(new ThreadStart(() => LoadTourPriceDataGridView()));
            threadLoadTourPriceDataGridView.Start();
        }
        public void LoadTourPriceForm(TourPrice data)
        {
            TourPriceId = data.Id;
            tbTourPriceID.Text = data.Id.ToString();
            tbTourPriceValue.Text = data.Price.ToString();
            tbTourPriceNote.Text = data.Note?.ToString();

            dtpTourPriceStartDate.Value = data.StartDate;
            dtpTourPriceEndDate.Value = data.EndDate;
        }
        private void btnTourPriceSearch_Click(object sender, EventArgs e)
        {
            Thread threadLoadTourPriceDataGridView = new Thread(new ThreadStart(() => LoadTourPriceDataGridView(dtpTourPriceSearch.Value)));
            threadLoadTourPriceDataGridView.Start();
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
                Thread threadLoadTourPriceDataGridView = new Thread(new ThreadStart(() => LoadTourPriceDataGridView()));
                threadLoadTourPriceDataGridView.Start();
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
                Thread threadLoadTourPriceDataGridView = new Thread(new ThreadStart(() => LoadTourPriceDataGridView()));
                threadLoadTourPriceDataGridView.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Handle Event Tour Location
        public void LoadAllLocationDataGridView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    dgvTourLocationListAll.ShowLoading(true);
                }));
            }
            var locationsData = LocationBLL.ListLocations();
            locations = locationsData.ToList();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    dgvTourLocationListAll.ShowLoading(false);
                    dgvTourLocationListAll.DataSource = locations;

                    dgvTourLocationListAll.Columns["Id"].Visible = false;
                    dgvTourLocationListAll.Columns["TourLocations"].Visible = false;

                    dgvTourLocationListAll.Columns["Name"].HeaderText = "Tên";
                }));
            }
        }
        public void LoadLocationDataGridView()
        {
            if (dgvTourList.SelectedRows.Count > 0 && TourId > 0)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        dgvTourLocationList.ShowLoading(true);
                    }));
                }
                var tourLocationsData = TourLocationBLL.ListTourLocationsByTourId(TourId);
                tourLocations = tourLocationsData.ToList();
                var dataSource = tourLocationsData.Select(t => new TourLocationDataSource(
                                        t.TourId,
                                        t.LocationId,
                                        t.Location.Name,
                                        t.Order)).ToList();
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        dgvTourLocationList.ShowLoading(false);
                        dgvTourLocationList.DataSource = dataSource;

                        dgvTourLocationList.Columns["LocationId"].Visible = false;
                        dgvTourLocationList.Columns["TourId"].Visible = false;
                        dgvTourLocationList.Columns["Order"].HeaderText = "Thứ tự";
                        dgvTourLocationList.Columns["Name"].HeaderText = "Tên";
                    }));
                }


            }
            else
            {
                //TODO
            }
        }
        private void btnTourLocationCancel_Click(object sender, EventArgs e)
        {
            Thread threadLoadLocationDataGridView = new Thread(new ThreadStart(() => LoadLocationDataGridView()));
            threadLoadLocationDataGridView.Start();
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
            Thread threadLoadLocationDataGridView = new Thread(new ThreadStart(() => LoadLocationDataGridView()));
            threadLoadLocationDataGridView.Start();
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
