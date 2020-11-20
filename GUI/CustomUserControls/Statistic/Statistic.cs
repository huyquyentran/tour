﻿using BLL;
using BLL.Statistic;
using GUI.BindingClasses;
using GUI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.CustomUserControls.Statistic
{
    public partial class Statistic : UserControl
    {
        public IEnumerable<TourWithGroups> _tourWithGroups;
        public Statistic()
        {
            InitializeComponent();
        }
        private void Statistic_Load(object sender, EventArgs e)
        {
            Thread threadLoadTourStatistic = new Thread(new ThreadStart(() => LoadTourStatistic()));
            threadLoadTourStatistic.Start();
            Thread threadLoadOverviewInformation = new Thread(new ThreadStart(() => LoadOverviewInformation()));
            threadLoadOverviewInformation.Start();

            chbStatisticDetailAllTime.Checked = true;
        }
        private void LoadOverviewInformation()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lbCountTour.Text = "Loading ...";
                }));
            }
            var overviewInformation = new OverviewInformation();

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lbCountTour.Text = overviewInformation.CountTours.ToString();
                    lbCountGroup.Text = overviewInformation.CountGroups.ToString();
                    lbCountStaff.Text = overviewInformation.CountStaffs.ToString();
                    lbCountCustomer.Text = overviewInformation.CountCustomers.ToString();
                    lbTotalRevenue.Text = overviewInformation.TotalRevenue.ToString();
                    lbTotalCost.Text = overviewInformation.TotalCosts.ToString();
                    lbTotalProfit.Text = overviewInformation.TotalProfix.ToString();
                    lbPercentDevelop.Text = $"{overviewInformation.PercentDevelop} %";
                }));
            }
        }
        private void LoadTourStatistic(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        dgvStatisticDetailTourList.ShowLoading(true);
                    }));
                }
                var tourWithGroups = TourStatistic.ListTourWithGroups(StartDate, EndDate);
                _tourWithGroups = tourWithGroups;
                var tourStatistics = tourWithGroups.Select(ele => new TourStatisticBinding(
                        ele.Tour.Id,
                        ele.Tour.Name,
                        ele.Groups.Count(),
                        TourStatistic.GetCostOfTourWithGroups(ele),
                        TourStatistic.GetRevenueOfTourWithGroups(ele)
                    )).ToList();
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        dgvStatisticDetailTourList.ShowLoading(false);
                        dgvStatisticDetailTourList.DataSource = tourStatistics;

                        dgvStatisticDetailTourList.Columns["TourId"].Visible = false;
                        dgvStatisticDetailTourList.Columns["TourName"].HeaderText = "Tên Tour";
                        dgvStatisticDetailTourList.Columns["TotalGroup"].HeaderText = "Số Đoàn";
                        dgvStatisticDetailTourList.Columns["TotalRevenue"].HeaderText = "Tổng Doanh Thu";
                        dgvStatisticDetailTourList.Columns["TotalCost"].HeaderText = "Tổng Chi Phí";
                        dgvStatisticDetailTourList.Columns["Profit"].HeaderText = "Lợi nhuận";
                    }));
                }
            }
            catch(Exception ex)
            {
                GUIExtensionMethod.HandleError(ex);
            }
        }
        private void LoadGroupsJoin(TourStatisticBinding tourStatisticBinding)
        {
            //Back to main thread update UI
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvStatisticGroupDetail.ShowLoading(true);
                }));
            }

            //Fill DataTable
            var groups = _tourWithGroups.FirstOrDefault(t => t.Tour.Id == tourStatisticBinding.TourId).Groups;
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
                    dgvStatisticGroupDetail.Columns.Clear();

                    dgvStatisticGroupDetail.Columns.Add("Id", "Mã");
                    dgvStatisticGroupDetail.Columns.Add("Name", "Tên đoàn");
                    dgvStatisticGroupDetail.Columns.Add("Tour", "Tour");
                    dgvStatisticGroupDetail.Columns.Add("StartDate", "Ngày đi");
                    dgvStatisticGroupDetail.Columns.Add("EndDate", "Ngày về");
                    dgvStatisticGroupDetail.Columns.Add("PriceTour", "Giá vé");
                    dgvStatisticGroupDetail.Columns.Add("Revenue", "Doanh Thu");
                    dgvStatisticGroupDetail.Columns.Add("Cost", "Chi phí");

                    dgvStatisticGroupDetail.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvStatisticGroupDetail.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    foreach (DataGridViewColumn column in dgvStatisticGroupDetail.Columns)
                    {
                        column.DataPropertyName = column.Name;
                    }

                    dgvStatisticGroupDetail.DataSource = data;
                }));
            }
        }
        private void btnStatisticDetail_Click(object sender, EventArgs e)
        {
            if(chbStatisticDetailAllTime.Checked)
            {
                Thread threadLoadTourStatistic = new Thread(new ThreadStart(() => LoadTourStatistic()));
                threadLoadTourStatistic.Start();
            }    
            else
            {
                Thread threadLoadTourStatistic = new Thread(new ThreadStart(() => LoadTourStatistic(dtpStatisticDetailStartDate.Value, dtpStatisticDetailEndDate.Value)));
                threadLoadTourStatistic.Start();
            }    
        }
        private void chbStatisticDetailAllTime_CheckedChanged(object sender, EventArgs e)
        {
            if(chbStatisticDetailAllTime.Checked == true)
            {
                dtpStatisticDetailStartDate.Enabled = false;
                dtpStatisticDetailEndDate.Enabled = false;
            }    
            else
            {
                dtpStatisticDetailStartDate.Enabled = true;
                dtpStatisticDetailEndDate.Enabled = true;
            }    
        }
        private void dgvStatisticDetailTourList_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvStatisticDetailTourList.SelectedRows.Count > 0)
            {
                var row = dgvStatisticDetailTourList.SelectedRows[0];
                var data = (TourStatisticBinding )row.DataBoundItem;
                Thread threadLoadGroupsJoin = new Thread(new ThreadStart(() => LoadGroupsJoin(data)));
                threadLoadGroupsJoin.Start();
            }    
        }
    }
}
