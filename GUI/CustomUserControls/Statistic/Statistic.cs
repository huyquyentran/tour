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
    }
}
