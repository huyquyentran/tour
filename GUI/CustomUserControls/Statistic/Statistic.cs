using BLL;
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
            Thread threadLoadStaff = new Thread(new ThreadStart(() => LoadStaff()));
            threadLoadStaff.Start();

            chbStatisticDetailAllTime.Checked = true;
        }
        private void LoadOverviewInformation()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lbCountTour.Text = "Loading ...";
                    lbCountGroup.Text = "Loading ...";
                    lbCountStaff.Text = "Loading ...";
                    lbCountCustomer.Text = "Loading ...";
                    lbTotalRevenue.Text = "Loading ...";
                    lbTotalCost.Text = "Loading ...";
                    lbTotalProfit.Text = "Loading ...";
                    lbPercentDevelop.Text = "Loading ...";
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
                        TourStatistic.GetRevenueOfTourWithGroups(ele),
                        TourStatistic.GetCostOfTourWithGroups(ele)
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
        private void LoadTourCostDetailGraph(int Id, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            //reset your chart series and legends
            chartCostDetail.Series.Clear();
            chartCostDetail.Legends.Clear();

            //Add a new Legend(if needed) and do some formating
            chartCostDetail.Legends.Add("LegendCostDetail");
            chartCostDetail.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Table;
            // chartCostDetail.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            // chartCostDetail.Legends[0].Alignment = StringAlignment.Center;
            chartCostDetail.Legends[0].Title = "Mục";
            chartCostDetail.Legends[0].BorderColor = Color.Black;

            //Add a new chart-series
            string seriesname = "SeriesCostDetail";
            chartCostDetail.Series.Add(seriesname);
            //set the chart-type to "Pie"
            chartCostDetail.Series[seriesname].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;

            //Add some datapoints so the series. in this case you can pass the values to this method
            var costDetails = CostBLL.GetCostStatisticsByTourId(Id, StartDate, EndDate);

            foreach (var c in costDetails)
            {
                chartCostDetail.Series[seriesname].Points.AddXY(c.Name, c.Price);
            }
            
        }
        private void LoadStaff(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        dgvStaff.ShowLoading(true);
                    }));
                }

                var staffStatistics = StaffBLL.GetTourCountOfStaffs(StartDate, EndDate);

                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        dgvStaff.ShowLoading(false);
                        dgvStaff.DataSource = staffStatistics;

                        dgvStaff.Columns["StaffId"].HeaderText = "Mã nhân viên";
                        dgvStaff.Columns["StaffName"].HeaderText = "Tên nhân viên";
                        dgvStaff.Columns["TourCount"].HeaderText = "Số lần đi khách";
                    }));
                }
            }
            catch (Exception ex)
            {
                GUIExtensionMethod.HandleError(ex);
            }
        }
        private void btnStatisticDetail_Click(object sender, EventArgs e)
        {
            if(chbStatisticDetailAllTime.Checked)
            {
                Thread threadLoadTourStatistic = new Thread(new ThreadStart(() => LoadTourStatistic()));
                threadLoadTourStatistic.Start();
                Thread threadLoadStaff = new Thread(new ThreadStart(() => LoadStaff()));
                threadLoadStaff.Start();
            }    
            else
            {
                Thread threadLoadTourStatistic = new Thread(new ThreadStart(() => LoadTourStatistic(dtpStatisticDetailStartDate.Value, dtpStatisticDetailEndDate.Value)));
                threadLoadTourStatistic.Start();
                Thread threadLoadStaff = new Thread(new ThreadStart(() => LoadStaff(dtpStatisticDetailStartDate.Value, dtpStatisticDetailEndDate.Value)));
                threadLoadStaff.Start();
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

                if (chbStatisticDetailAllTime.Checked)
                {
                    LoadTourCostDetailGraph(data.TourId);
                }
                else
                {
                    LoadTourCostDetailGraph(data.TourId, dtpStatisticDetailStartDate.Value, dtpStatisticDetailEndDate.Value);
                }
            }    
        }
    }
}
