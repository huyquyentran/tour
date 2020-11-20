using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Statistic
{
    public class OverviewInformation
    {
        public OverviewInformation () { }
        public int CountTours
        {
            get
            {
                return TourDAL.Get().Count();
            }
        }
        public int CountGroups
        {
            get
            {
                return GroupDAL.Get().Count();
            }
        }
        public int CountStaffs
        {
            get
            {
                return StaffDAL.Get().Count();
            }
        }
        public int CountCustomers
        {
            get
            {
                return CustomerDAL.Get().Count();
            }
        }

        public int TotalRevenue
        {
            get
            {
                int total = 0;
                var groups = GroupBLL.ListGroups();
                foreach (var group in groups)
                {
                    total += GroupBLL.GetRevenueOfGroup(group);
                }
                return total;
            }
        }
        public int TotalCosts
        {
            get
            {
                int total = 0;
                var groups = GroupBLL.ListGroups();
                foreach (var group in groups)
                {
                    total += GroupBLL.GetTotalCostOfGroup(group);
                }
                return total;
            }
        }

        public int TotalProfix
        {
            get
            {
                return TotalRevenue - TotalCosts;
            }
        }

        public int PercentDevelop
        {
            get
            {
                return 696969;
            }
        }
    }
}
