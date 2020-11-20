using BLL;
using Core.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using GUI.Common;
using Core.Models;
using GUI.BindingClasses;

namespace GUI.CustomUserControls.TourGroup
{
    public partial class UcTourGroup : UserControl
    {
        private OperationType groupMode = OperationType.View;
        private OperationType costMode = OperationType.View;

        public UcTourGroup()
        {
            InitializeComponent();
        }

        private void UcTourGroup_Load(object sender, EventArgs e)
        {
            dgvGroupList.AutoGenerateColumns = false;
            dgvGroupCustomerList.AutoGenerateColumns = false;
            dgvGroupCustomerListAll.AutoGenerateColumns = false;
            dgvGroupStaffList.AutoGenerateColumns = false;
            dgvGroupStaffListAll.AutoGenerateColumns = false;
            Thread loadGroupsThread = new Thread(new ThreadStart(LoadGroups));
            Thread loadToursThread = new Thread(new ThreadStart(LoadTours));
            Thread loadCostTypesThread = new Thread(new ThreadStart(LoadCostTypes));
            loadGroupsThread.Start();
            loadToursThread.Start();
            loadCostTypesThread.Start();
        }

        private void LoadGroups()
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupList.ShowLoading(true);
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

        private void LoadCostTypes()
        {
            var costTypes = CostBLL.ListCostTypes();
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    cbGroupCostType.DataSource = costTypes;
                    cbGroupCostType.DisplayMember = "Name";
                    cbGroupCostType.ValueMember = "Id";
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
                    dgvGroupCustomerListAll.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var customers = CustomerBLL.ListCustomersNotInGroup(groupId);
            var data = new BindingList<Customer>();
            foreach (var customer in customers)
            {
                data.Add(new Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    IdentificationNumber = customer.IdentificationNumber,
                    Gender = customer.Gender,
                    Address = customer.Address
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
                    dgvGroupCustomerList.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var customers = CustomerBLL.ListCustomersInGroup(groupId);
            var data = new BindingList<Customer>();

            foreach (var customer in customers)
            {
                data.Add(new Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    IdentificationNumber = customer.IdentificationNumber,
                    Gender = customer.Gender,
                    Address = customer.Address
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

                    dgvGroupCustomerList.DataSource = data;
                }));
            }
        }

        private void LoadStaffsInGroup(int groupId)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupStaffList.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var staffs = StaffBLL.ListStaffsInGroup(groupId);
            var data = new BindingList<StaffBinding>();


            foreach (var staff in staffs)
            {
                data.Add(new StaffBinding
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    DoB = staff.DoB,
                    PhoneNumber = staff.PhoneNumber,
                    IdentificationNumber = staff.IdentificationNumber,
                    Gender = staff.Gender,
                    Address = staff.Address,
                    Position = staff.Assignments.FirstOrDefault(a => a.GroupId == groupId).Position,
                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupStaffList.Columns.Clear();

                    dgvGroupStaffList.Columns.Add("Id", "Mã");
                    dgvGroupStaffList.Columns.Add("Name", "Tên");
                    dgvGroupStaffList.Columns.Add("DoB", "Ngày sinh");
                    dgvGroupStaffList.Columns.Add("PhoneNumber", "SĐT");
                    dgvGroupStaffList.Columns.Add("IdentificationNumber", "CMND");
                    dgvGroupStaffList.Columns.Add("Gender", "Giới tính");
                    dgvGroupStaffList.Columns.Add("Address", "Địa chỉ");

                    dgvGroupStaffList.Columns["DoB"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    foreach (DataGridViewColumn column in dgvGroupStaffList.Columns)
                    {
                        column.DataPropertyName = column.Name;
                        column.ReadOnly = true;
                    }

                    DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.HeaderText = "Nhiệm vụ";
                    dgvCmb.Name = "Position";
                    dgvCmb.DataPropertyName = "Position";
                    dgvCmb.DisplayMember = "Description";
                    dgvCmb.ValueMember = "Value";
                    dgvCmb.BindEnumToDataGridViewCombobox<Position>();
                    dgvGroupStaffList.Columns.Add(dgvCmb);
                    dgvGroupStaffList.Columns["Position"].DisplayIndex = 2;

                    dgvGroupStaffList.DataSource = data;
                }));
            }
        }

        private void LoadStaffsNotInGroup(int groupId)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupStaffListAll.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var staffs = StaffBLL.ListStaffsNotInGroup(groupId);
            var data = new BindingList<StaffBinding>();


            foreach (var staff in staffs)
            {
                data.Add(new StaffBinding
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    DoB = staff.DoB,
                    PhoneNumber = staff.PhoneNumber,
                    IdentificationNumber = staff.IdentificationNumber,
                    Gender = staff.Gender,
                    Address = staff.Address,
                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupStaffListAll.Columns.Clear();

                    dgvGroupStaffListAll.Columns.Add("Id", "Mã");
                    dgvGroupStaffListAll.Columns.Add("Name", "Tên");
                    dgvGroupStaffListAll.Columns.Add("DoB", "Ngày sinh");
                    dgvGroupStaffListAll.Columns.Add("PhoneNumber", "SĐT");
                    dgvGroupStaffListAll.Columns.Add("IdentificationNumber", "CMND");
                    dgvGroupStaffListAll.Columns.Add("Gender", "Giới tính");
                    dgvGroupStaffListAll.Columns.Add("Address", "Địa chỉ");

                    dgvGroupStaffListAll.Columns["DoB"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    foreach (DataGridViewColumn column in dgvGroupStaffListAll.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvGroupStaffListAll.DataSource = data;
                }));
            }
        }

        private void LoadCosts(int groupId)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvGroupCostList.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var costs = CostBLL.ListCosts(groupId);
            var data = new BindingList<object>();
            foreach (var cost in costs)
            {
                data.Add(new
                {
                    cost.Id,
                    Type = cost.CostType.Name,
                    cost.Price,
                    cost.Note,
                });
            }

            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //Clear loading column
                    dgvGroupCostList.Columns.Clear();

                    dgvGroupCostList.Columns.Add("Id", "Mã");
                    dgvGroupCostList.Columns.Add("Type", "Loại");
                    dgvGroupCostList.Columns.Add("Price", "Giá");
                    dgvGroupCostList.Columns.Add("Note", "Ghi chú");

                    foreach (DataGridViewColumn column in dgvGroupCostList.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvGroupCostList.DataSource = data;
                }));
            }
        }

        private void dgvGroupList_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
            {
                //No rows are selected
                btnGroupDelete.Enabled = false;
                btnGroupEdit.Enabled = false;

                dgvGroupCustomerList.Rows.Clear();
                dgvGroupCustomerListAll.Rows.Clear();
                dgvGroupStaffList.Rows.Clear();
                dgvGroupStaffListAll.Rows.Clear();
                dgvGroupCostList.Rows.Clear();

                dgvGroupCustomerList.Refresh();
                dgvGroupCustomerListAll.Refresh();
                dgvGroupStaffList.Refresh();
                dgvGroupStaffListAll.Refresh();
                dgvGroupCostList.Refresh();

                tbGroupID.Text = null;
                tbGroupName.Text = null;
                dtpGroupStartDate.Value = DateTime.Now;
                dtpGroupEndDate.Value = DateTime.Now;
                tbGroupPrice.Text = null;
                tbGroupRevenue.Text = null;
                tbGroupTotalCost.Text = null;

                btnGroupCostAdd.Enabled = false;
                return;
            }
            btnGroupDelete.Enabled = true;
            btnGroupEdit.Enabled = true;

            btnGroupCostAdd.Enabled = true;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];

            tbGroupID.Text = row.Cells["Id"].Value?.ToString();
            tbGroupName.Text = row.Cells["Name"].Value?.ToString();
            cbGroupTour.SelectedIndex = cbGroupTour.FindString(row.Cells["Tour"].Value?.ToString());
            dtpGroupStartDate.Value = (DateTime)row.Cells["StartDate"].Value;
            dtpGroupEndDate.Value = (DateTime)row.Cells["EndDate"].Value;
            tbGroupPrice.Text = row.Cells["PriceTour"].Value?.ToString();
            tbGroupRevenue.Text = row.Cells["Revenue"].Value?.ToString();
            tbGroupTotalCost.Text = row.Cells["Cost"].Value?.ToString();

            var groupId = int.Parse(tbGroupID.Text);
            Thread loadCustomersThread = new Thread(() =>
            {
                LoadCustomersInGroup(groupId);
                LoadCustomersNotInGroup(groupId);
            });
            Thread loadStaffsThread = new Thread(() =>
            {
                LoadStaffsInGroup(groupId);
                LoadStaffsNotInGroup(groupId);
                LoadCosts(groupId);
            });


            loadCustomersThread.Start();
            loadStaffsThread.Start();
        }

        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            if (groupMode != OperationType.Add)
            {
                groupMode = OperationType.Add;
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

                groupMode = OperationType.View;
                btnGroupAdd.Text = "+ Thêm";
                btnGroupEdit.Visible = true;
                btnGroupDelete.Visible = true;
                btnCancel.Visible = false;

                tbGroupName.Enabled = false;
                cbGroupTour.Enabled = false;
                dtpGroupStartDate.Enabled = false;
                dtpGroupEndDate.Enabled = false;
            }

            dgvGroupList.ClearSelection();
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (groupMode != OperationType.Edit)
            {
                groupMode = OperationType.Edit;
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

                groupMode = OperationType.View;
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

            groupMode = OperationType.View;

            if (cbGroupTour.Items.Count > 0)
            {
                cbGroupTour.SelectedIndex = 0;
            }

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
                MessageBox.Show("Vui lòng chọn khách hàng cần thêm vào đoàn");
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

            var list = (BindingList<Customer>)dgvGroupCustomerList.DataSource;
            list.Add(new Customer
            {
                Id = int.Parse(id),
                Name = name,
                PhoneNumber = phoneNumber,
                IdentificationNumber = identificationNumber,
                Gender = (Gender)Enum.Parse(typeof(Gender), gender, true),
                Address = address,
            });

            dgvGroupCustomerListAll.Rows.RemoveAt(index);

        }

        private void btnGroupCustomerRemove_Click(object sender, EventArgs e)
        {
            if (dgvGroupCustomerList.SelectedRows.Count == 0 || dgvGroupCustomerList.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa khỏi đoàn");
                return;
            }

            var index = dgvGroupCustomerList.SelectedRows[0].Index;
            var row = dgvGroupCustomerList.Rows[index];

            var id = row.Cells["Id"].Value?.ToString();
            var name = row.Cells["Name"].Value?.ToString();
            var phoneNumber = row.Cells["PhoneNumber"].Value?.ToString();
            var identificationNumber = row.Cells["IdentificationNumber"].Value?.ToString();
            var gender = row.Cells["Gender"].Value?.ToString();
            var address = row.Cells["Address"].Value?.ToString();

            var list = (BindingList<Customer>)dgvGroupCustomerListAll.DataSource;
            list.Add(new Customer
            {
                Id = int.Parse(id),
                Name = name,
                PhoneNumber = phoneNumber,
                IdentificationNumber = identificationNumber,
                Gender = (Gender)Enum.Parse(typeof(Gender), gender, true),
                Address = address,
            });

            dgvGroupCustomerList.Rows.RemoveAt(index);

        }

        private void btnGroupCustomerSave_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
                return;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];
            var groupId = int.Parse(row.Cells["Id"].Value?.ToString());

            var customersList = (BindingList<Customer>)dgvGroupCustomerList.DataSource;
            var customersListId = customersList.Select(c => c.Id).ToList();

            GroupBLL.SaveListCustomersOfGroup(groupId, customersListId);
            Thread loadCustomersInGroupThread = new Thread(() => LoadCustomersInGroup(groupId));
            Thread loadCustomersNotInGroupThread = new Thread(() => LoadCustomersNotInGroup(groupId));
            Thread loadGroupsThread = new Thread(() => LoadGroups());
            loadCustomersInGroupThread.Start();
            loadCustomersNotInGroupThread.Start();
            loadGroupsThread.Start();
            MessageBox.Show("Cập nhật danh sách khách hàng thành công");
        }

        private void btnGroupCustomerCancel_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
                return;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];

            var groupId = int.Parse(row.Cells["Id"].Value?.ToString());
            Thread loadCustomersThread = new Thread(() =>
            {
                LoadCustomersInGroup(groupId);
                LoadCustomersNotInGroup(groupId);
            });
            loadCustomersThread.Start();
        }

        private void dgvGroupStaffList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Open combobox at first click
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        private void btnGroupStaffAdd_Click(object sender, EventArgs e)
        {
            if (dgvGroupStaffListAll.SelectedRows.Count == 0 || dgvGroupStaffListAll.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần thêm vào đoàn");
                return;
            }

            var index = dgvGroupStaffListAll.SelectedRows[0].Index;
            var row = dgvGroupStaffListAll.Rows[index];

            var id = row.Cells["Id"].Value?.ToString();
            var name = row.Cells["Name"].Value?.ToString();
            DateTime doB = (DateTime)row.Cells["DoB"].Value;
            var phoneNumber = row.Cells["PhoneNumber"].Value?.ToString();
            var identificationNumber = row.Cells["IdentificationNumber"].Value?.ToString();
            var gender = row.Cells["Gender"].Value?.ToString();
            var address = row.Cells["Address"].Value?.ToString();

            var list = (BindingList<StaffBinding>)dgvGroupStaffList.DataSource;
            list.Add(new StaffBinding
            {
                Id = int.Parse(id),
                Name = name,
                DoB = doB,
                PhoneNumber = phoneNumber,
                IdentificationNumber = identificationNumber,
                Gender = (Gender)Enum.Parse(typeof(Gender), gender, true),
                Address = address,
                Position = Position.HuongDanVien // Default position
            });

            dgvGroupStaffListAll.Rows.RemoveAt(index);
        }

        private void btnGroupStaffSave_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
                return;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];
            var groupId = int.Parse(row.Cells["Id"].Value?.ToString());

            var staffs = (BindingList<StaffBinding>)dgvGroupStaffList.DataSource;
            var assignment = new List<Assignment>();
            foreach (var staff in staffs)
            {
                assignment.Add(
                    new Assignment
                    {
                        StaffId = staff.Id,
                        GroupId = groupId,
                        Position = staff.Position
                    });
            }

            GroupBLL.SaveListStaffsOfGroup(groupId, assignment);
            Thread loadStaffsThread = new Thread(
                () =>
                {
                    LoadStaffsInGroup(groupId);
                    LoadStaffsNotInGroup(groupId);
                });
            loadStaffsThread.Start();
            MessageBox.Show("Cập nhật danh sách nhân viên thành công");
        }

        private void btnGroupStaffRemove_Click(object sender, EventArgs e)
        {
            if (dgvGroupStaffList.SelectedRows.Count == 0 || dgvGroupStaffList.SelectedRows[0].Index < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa khỏi đoàn");
                return;
            }

            var index = dgvGroupStaffList.SelectedRows[0].Index;
            var row = dgvGroupStaffList.Rows[index];

            var id = row.Cells["Id"].Value?.ToString();
            var name = row.Cells["Name"].Value?.ToString();
            DateTime doB = (DateTime)row.Cells["DoB"].Value;
            var phoneNumber = row.Cells["PhoneNumber"].Value?.ToString();
            var identificationNumber = row.Cells["IdentificationNumber"].Value?.ToString();
            var gender = row.Cells["Gender"].Value?.ToString();
            var address = row.Cells["Address"].Value?.ToString();

            var list = (BindingList<StaffBinding>)dgvGroupStaffListAll.DataSource;
            list.Add(new StaffBinding
            {
                Id = int.Parse(id),
                Name = name,
                DoB = doB,
                PhoneNumber = phoneNumber,
                IdentificationNumber = identificationNumber,
                Gender = (Gender)Enum.Parse(typeof(Gender), gender, true),
                Address = address,
            });

            dgvGroupStaffList.Rows.RemoveAt(index);
        }

        private void btnGroupStaffCancel_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
                return;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];

            var groupId = int.Parse(row.Cells["Id"].Value?.ToString());
            Thread loadStaffsThread = new Thread(() =>
            {
                LoadStaffsInGroup(groupId);
                LoadStaffsNotInGroup(groupId);
            });
            loadStaffsThread.Start();
        }

        private void dgvGroupCostList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGroupCostList.SelectedRows.Count == 0 || dgvGroupCostList.SelectedRows[0].Index < 0)
            {
                btnGroupCostEdit.Enabled = false;
                btnGroupCostDelete.Enabled = false;

                //No rows are selected
                tbGroupCostID.Text = null;
                tbGroupCostValue.Text = null;
                tbGroupCostNote.Text = null;
                return;
            }
            btnGroupCostEdit.Enabled = true;
            btnGroupCostDelete.Enabled = true;

            var rowIndex = dgvGroupCostList.SelectedRows[0].Index;
            var row = dgvGroupCostList.Rows[rowIndex];

            tbGroupCostID.Text = row.Cells["Id"].Value?.ToString();
            cbGroupCostType.SelectedIndex = cbGroupCostType.FindString(row.Cells["Type"].Value?.ToString());
            tbGroupCostValue.Text = row.Cells["Price"].Value?.ToString();
            tbGroupCostNote.Text = row.Cells["Note"].Value?.ToString();
        }

        private void btnGroupCostAdd_Click(object sender, EventArgs e)
        {
            if (costMode != OperationType.Add)
            {
                costMode = OperationType.Add;
                btnGroupCostCancel.Visible = true;
                btnGroupCostAdd.Text = "Lưu";
                btnGroupCostEdit.Visible = false;
                btnGroupCostDelete.Visible = false;

                cbGroupCostType.Enabled = true;
                tbGroupCostValue.Enabled = true;
                tbGroupCostNote.Enabled = true;
            }
            else
            {
                var rowIndex = dgvGroupList.SelectedRows[0].Index;
                var row = dgvGroupList.Rows[rowIndex];
                var groupId = int.Parse(row.Cells["Id"].Value?.ToString());

                int costTypeId = (int)cbGroupCostType.SelectedValue;
                var price = tbGroupCostValue.Text;
                var note = tbGroupCostNote.Text;
                try
                {
                    CostBLL.CreateCost(groupId, costTypeId, price, note);
                    Thread loadCostsThread = new Thread(() => LoadCosts(groupId));
                    loadCostsThread.Start();
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

                costMode = OperationType.View;
                btnGroupCostAdd.Text = "+ Thêm";
                btnGroupCostEdit.Visible = true;
                btnGroupCostDelete.Visible = true;
                btnGroupCostCancel.Visible = false;

                cbGroupCostType.Enabled = false;
                tbGroupCostValue.Enabled = false;
                tbGroupCostNote.Enabled = false;
            }

            dgvGroupCostList.ClearSelection();
        }

        private void btnGroupCostCancel_Click(object sender, EventArgs e)
        {
            btnGroupCostCancel.Visible = false;
            btnGroupCostAdd.Visible = true;
            btnGroupCostDelete.Visible = true;
            btnGroupCostEdit.Visible = true;

            btnGroupCostAdd.Text = "+ Thêm";
            btnGroupCostDelete.Text = "Xóa 🗑";
            btnGroupCostEdit.Text = "Sửa ✎";

            costMode = OperationType.View;

            if (cbGroupCostType.Items.Count > 0)
            {
                cbGroupCostType.SelectedIndex = 0;
            }

            dgvGroupCostList.ClearSelection();

            cbGroupCostType.Enabled = false;
            tbGroupCostValue.Enabled = false;
            tbGroupCostNote.Enabled = false;
        }

        private void btnGroupCostEdit_Click(object sender, EventArgs e)
        {
            if (costMode != OperationType.Edit)
            {
                costMode = OperationType.Edit;
                btnGroupCostCancel.Visible = true;
                btnGroupCostEdit.Text = "Lưu";
                btnGroupCostAdd.Visible = false;
                btnGroupCostDelete.Visible = false;

                cbGroupCostType.Enabled = true;
                tbGroupCostValue.Enabled = true;
                tbGroupCostNote.Enabled = true;
            }
            else
            {
                var rowIndex = dgvGroupList.SelectedRows[0].Index;
                var row = dgvGroupList.Rows[rowIndex];
                var groupId = int.Parse(row.Cells["Id"].Value?.ToString());

                int id = int.Parse(tbGroupCostID.Text);
                int costTypeId = (int)cbGroupCostType.SelectedValue;
                var price = tbGroupCostValue.Text;
                var note = tbGroupCostNote.Text;
                try
                {
                    CostBLL.EditCost(id, groupId, costTypeId, price, note);
                    Thread loadCostsThread = new Thread(() => LoadCosts(groupId));
                    loadCostsThread.Start();
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

                costMode = OperationType.View;
                btnGroupCostEdit.Text = "Sửa ✎";
                btnGroupCostAdd.Visible = true;
                btnGroupCostDelete.Visible = true;
                btnGroupCostCancel.Visible = false;

                cbGroupCostType.Enabled = false;
                tbGroupCostValue.Enabled = false;
                tbGroupCostNote.Enabled = false;
            }
        }

        private void btnGroupCostDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa??", "Xác nhận xóa!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int id = int.Parse(tbGroupCostID.Text);
                CostBLL.RemoveCost(id);

                var rowIndex = dgvGroupList.SelectedRows[0].Index;
                var row = dgvGroupList.Rows[rowIndex];
                var groupId = int.Parse(row.Cells["Id"].Value?.ToString());
                Thread loadCostsThread = new Thread(() => LoadCosts(groupId));
                loadCostsThread.Start();
            }
        }

        private void btnGroupCostRefresh_Click(object sender, EventArgs e)
        {

            if (dgvGroupList.SelectedRows.Count == 0 || dgvGroupList.SelectedRows[0].Index < 0)
                return;

            var rowIndex = dgvGroupList.SelectedRows[0].Index;
            var row = dgvGroupList.Rows[rowIndex];

            var groupId = int.Parse(row.Cells["Id"].Value?.ToString());
            Thread loadCostsThread = new Thread(() => LoadCosts(groupId));
            loadCostsThread.Start();
        }
    }
}