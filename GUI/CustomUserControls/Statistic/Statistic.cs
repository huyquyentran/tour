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
        }

        private void LoadTourStatistic()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvStatisticDetailTourList.ShowLoading(true);
                }));
            }
            var tourWithGroups = TourStatistic.ListTourWithGroups();
            var tourStatistics = tourWithGroups.Select(ele => new TourStatisticBinding (
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
                }));
            }
            
        }
    }
}
