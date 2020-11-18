using BLL;
using Core.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace GUI.CustomUserControls.TourGroup
{
    public partial class UcTourGroup : UserControl
    {
        private OperationType mode = OperationType.View;
        private BindingList<object> customersInGroup;
        public UcTourGroup()
        {
            InitializeComponent();
        }

        private void UcTourGroup_Load(object sender, EventArgs e)
        {
            Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
            Thread loadToursThread = new Thread(new ThreadStart(LoadTours));
            loadGroupsThread.Start();
            loadToursThread.Start();
        }

        private void LoadGroups()
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupList.Columns.Add("Loading", "Loading...");
                    dgvGroupList.Columns["Loading"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }));
            }

            //Fill DataTable
            var groups = GroupBLL.ListGroups();
            List<object> data = new List<object>();
            foreach (var group in groups)
            {
                data.Add(new
                {
                    group.Id,
                    group.Name,
                    Tour = group.Tour.Name,
                    group.StartDate,
                    group.EndDate,
                    PriceTour = GroupBLL.GetPriceTourOfGroup(group),
                    Revenue = GroupBLL.GetRevenueOfGroup(group),
                    Cost = GroupBLL.GetTotalCostOfGroup(group),

                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupList.Columns.Clear();

                    dgvGroupList.Columns.Add("Id", "Mã");
                    dgvGroupList.Columns.Add("Name", "Tên đoàn");
                    dgvGroupList.Columns.Add("Tour", "Tour");
                    dgvGroupList.Columns.Add("PriceTour", "Giá vé");
                    dgvGroupList.Columns.Add("StartDate", "Ngày đi");
                    dgvGroupList.Columns.Add("EndDate", "Ngày về");
                    dgvGroupList.Columns.Add("PriceTour", "Giá vé");
                    dgvGroupList.Columns.Add("Revenue", "Doanh Thu");
                    dgvGroupList.Columns.Add("Cost", "Chi phí");

                    dgvGroupList.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvGroupList.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    foreach (DataGridViewColumn column in dgvGroupList.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvGroupList.DataSource = data;
                }));
            }
        }

        private void LoadTours()
        {
            var tours = TourBLL.ListTours();
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    cbGroupTour.DataSource = tours;
                    cbGroupTour.DisplayMember = "Name";
                    cbGroupTour.ValueMember = "Id";
                }));
            }
        }

        private void LoadCustomersNotInGroup(int groupId)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupCustomerListAll.Columns.Add("Loading", "Loading...");
                    dgvGroupCustomerListAll.Columns["Loading"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }));
            }

            //Fill DataTable
            var customers = CustomerBLL.ListCustomersNotInGroup(groupId);
            BindingList<object> data = new BindingList<object>();
            foreach (var customer in customers)
            {
                data.Add(new
                {
                    customer.Id,
                    customer.Name,
                    customer.PhoneNumber,
                    customer.IdentificationNumber,
                    customer.Gender,
                    customer.Address
                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupCustomerListAll.Columns.Clear();

                    dgvGroupCustomerListAll.Columns.Add("Id", "Mã");
                    dgvGroupCustomerListAll.Columns.Add("Name", "Tên");
                    dgvGroupCustomerListAll.Columns.Add("PhoneNumber", "SĐT");
                    dgvGroupCustomerListAll.Columns.Add("IdentificationNumber", "CMND");
                    dgvGroupCustomerListAll.Columns.Add("Gender", "Giới tính");
                    dgvGroupCustomerListAll.Columns.Add("Address", "Địa chỉ");

                    foreach (DataGridViewColumn column in dgvGroupCustomerListAll.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvGroupCustomerListAll.DataSource = data;
                }));
            }
        }

        private void LoadCustomersInGroup(int groupId)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupCustomerList.Columns.Add("Loading", "Loading...");
                    dgvGroupCustomerList.Columns["Loading"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }));
            }

            //Fill DataTable
            var customers = CustomerBLL.ListCustomersInGroup(groupId);
            customersInGroup = new BindingList<object>();
            foreach (var customer in customers)
            {
                customersInGroup.Add(new
                {
                    customer.Id,
                    customer.Name,
                    customer.PhoneNumber,
                    customer.IdentificationNumber,
                    customer.Gender,
                    customer.Address
                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupCustomerList.Columns.Clear();

                    dgvGroupCustomerList.Columns.Add("Id", "Mã");
                    dgvGroupCustomerList.Columns.Add("Name", "Tên");
                    dgvGroupCustomerList.Columns.Add("PhoneNumber", "SĐT");
                    dgvGroupCustomerList.Columns.Add("IdentificationNumber", "CMND");
                    dgvGroupCustomerList.Columns.Add("Gender", "Giới tính");
                    dgvGroupCustomerList.Columns.Add("Address", "Địa chỉ");

                    foreach (DataGridViewColumn column in dgvGroupCustomerList.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvGroupCustomerList.DataSource = customersInGroup;
                }));
            }
        }

        private void dgvGroupList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvGroupList.Rows[e.RowIndex];
            tbGroupID.Text = row.Cells["Id"].Value?.ToString();
            tbGroupName.Text = row.Cells["Name"].Value?.ToString();
            cbGroupTour.SelectedIndex = cbGroupTour.FindString(row.Cells["Tour"].Value?.ToString());
            dtpGroupStartDate.Value = (DateTime)row.Cells["StartDate"].Value;
            dtpGroupEndDate.Value = (DateTime)row.Cells["EndDate"].Value;
            tbGroupPrice.Text = row.Cells["PriceTour"].Value?.ToString();
            tbGroupRevenue.Text = row.Cells["Revenue"].Value?.ToString();
            tbGroupTotalCost.Text = row.Cells["Cost"].Value?.ToString();

            var groupId = int.Parse(tbGroupID.Text);
            Thread loadCustomersInGroupThread = new Thread(() => LoadCustomersInGroup(groupId));
            Thread loadCustomersNotInGroupThread = new Thread(() => LoadCustomersNotInGroup(groupId));
            loadCustomersInGroupThread.Start();
            loadCustomersNotInGroupThread.Start();
        }

        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            if (mode != OperationType.Add)
            {
                mode = OperationType.Add;
                btnCancel.Visible = true;
                btnGroupAdd.Text = "Lưu";
                btnGroupEdit.Visible = false;
                btnGroupDelete.Visible = false;

                tbGroupName.Enabled = true;
                cbGroupTour.Enabled = true;
                dtpGroupStartDate.Enabled = true;
                dtpGroupEndDate.Enabled = true;
            }
            else
            {
                var name = tbGroupName.Text;
                int tourId = (int)cbGroupTour.SelectedValue;
                DateTime startDate = dtpGroupStartDate.Value;
                DateTime endDate = dtpGroupEndDate.Value;
                try
                {
                    GroupBLL.CreateGroup(name, startDate, endDate, tourId);
                    Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
                    loadGroupsThread.Start();
                }
                catch (Exception ex)
                {
                    string message = "";
                    foreach (DictionaryEntry item in ex.Data)
                    {
                        message += item.Value?.ToString();
                        message += Environment.NewLine;
                    }
                    MessageBox.Show(message);
                    return;
                }

                mode = OperationType.View;
                btnGroupAdd.Text = "+ Thêm";
                btnGroupEdit.Visible = true;
                btnGroupDelete.Visible = true;
                btnCancel.Visible = false;

                tbGroupName.Enabled = false;
                cbGroupTour.Enabled = false;
                dtpGroupStartDate.Enabled = false;
                dtpGroupEndDate.Enabled = false;
            }
            tbGroupID.Text = null;
            tbGroupName.Text = null;
            if (cbGroupTour.Items.Count > 0)
            {
                cbGroupTour.SelectedIndex = 0;
            }
            tbGroupPrice.Text = null;
            tbGroupRevenue.Text = null;
            tbGroupTotalCost.Text = null;
            dgvGroupList.ClearSelection();
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn đoàn cần sửa");
                return;
            }

            if (mode != OperationType.Edit)
            {
                mode = OperationType.Edit;
                btnCancel.Visible = true;
                btnGroupEdit.Text = "Lưu";
                btnGroupAdd.Visible = false;
                btnGroupDelete.Visible = false;

                tbGroupName.Enabled = true;
                cbGroupTour.Enabled = true;
                dtpGroupStartDate.Enabled = true;
                dtpGroupEndDate.Enabled = true;
            }
            else
            {
                int id = int.Parse(tbGroupID.Text);
                var name = tbGroupName.Text;
                int tourId = (int)cbGroupTour.SelectedValue;
                DateTime startDate = dtpGroupStartDate.Value;
                DateTime endDate = dtpGroupEndDate.Value;
                try
                {
                    GroupBLL.EditGroup(id, name, startDate, endDate, tourId);
                    Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
                    loadGroupsThread.Start();
                }
                catch (Exception ex)
                {
                    string message = "";
                    foreach (DictionaryEntry item in ex.Data)
                    {
                        message += item.Value?.ToString();
                        message += Environment.NewLine;
                    }
                    MessageBox.Show(message);
                    return;
                }

                mode = OperationType.View;
                btnGroupEdit.Text = "Sửa ✎";
                btnGroupAdd.Visible = true;
                btnGroupDelete.Visible = true;
                btnCancel.Visible = false;

                tbGroupName.Enabled = false;
                cbGroupTour.Enabled = false;
                dtpGroupStartDate.Enabled = false;
                dtpGroupEndDate.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnGroupAdd.Visible = true;
            btnGroupDelete.Visible = true;
            btnGroupEdit.Visible = true;

            btnGroupAdd.Text = "+ Thêm";
            btnGroupDelete.Text = "Xóa 🗑";
            btnGroupEdit.Text = "Sửa ✎";

            mode = OperationType.View;

            tbGroupID.Text = null;
            tbGroupName.Text = null;
            if (cbGroupTour.Items.Count > 0)
            {
                cbGroupTour.SelectedIndex = 0;
            }
            tbGroupPrice.Text = null;
            tbGroupRevenue.Text = null;
            tbGroupTotalCost.Text = null;
            dgvGroupList.ClearSelection();

            tbGroupID.Enabled = false;
            tbGroupName.Enabled = false;
            cbGroupTour.Enabled = false;
            tbGroupPrice.Enabled = false;
            dtpGroupStartDate.Enabled = false;
            dtpGroupEndDate.Enabled = false;
        }

        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn đoàn cần xóa");
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa??", "Xác nhận xóa!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int id = int.Parse(tbGroupID.Text);
                GroupBLL.RemoveGroup(id);
                Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
                loadGroupsThread.Start();
            }
        }

        private void btnGroupRefresh_Click(object sender, EventArgs e)
        {
            Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
            loadGroupsThread.Start();
        }

        private void btnGroupCustomerAdd_Click(object sender, EventArgs e)
        {
            if (dgvGroupCustomerListAll.SelectedRows.Count == 0 || dgvGroupCustomerListAll.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần thềm vào đoàn");
                return;
            }

            var index = dgvGroupCustomerListAll.SelectedRows[0].Index;
            var row = dgvGroupCustomerListAll.Rows[index];

            var id = row.Cells["Id"].Value?.ToString();
            var name = row.Cells["Name"].Value?.ToString();
            var phoneNumber = row.Cells["PhoneNumber"].Value?.ToString();
            var identificationNumber = row.Cells["IdentificationNumber"].Value?.ToString();
            var gender = row.Cells["Gender"].Value?.ToString();
            var address = row.Cells["Address"].Value?.ToString();

            customersInGroup.Add(new
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                IdentificationNumber = identificationNumber,
                Gender = gender,
                Address = address,
            });



            dgvGroupCustomerListAll.Rows.RemoveAt(index);

        }
    }
}
