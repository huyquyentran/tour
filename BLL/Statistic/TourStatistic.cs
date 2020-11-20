using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Statistic
{
    public class TourStatistic
    {
        public static IEnumerable<TourWithGroups> ListTourWithGroups()
        {
            var listTours = TourDAL.Get();
            var listTourWithGroups = listTours.Select(t => new TourWithGroups { Tour = t }).ToList();
            //Check filter date in this listGroups;
            var listGroups = GroupBLL.ListGroups();
            foreach (var group in listGroups)
            {
                int foundIndex = listTourWithGroups.FindIndex(ele => ele.Tour.Id == group.Tour.Id);
                if (foundIndex != -1)
                {
                    listTourWithGroups[foundIndex].Groups.Add(group);
                }
            }
            
            return listTourWithGroups;
        }

        public static int GetRevenueOfTourWithGroups(TourWithGroups tourWithGroups)
        {
            int total = 0;
            if (tourWithGroups.Groups == null) return 0;
            foreach (var group in tourWithGroups.Groups)
            {
                total += GroupBLL.GetRevenueOfGroup(group);
            }
            return total;
        }

        public static int GetCostOfTourWithGroups(TourWithGroups tourWithGroups)
        {
            int total = 0;
            if (tourWithGroups.Groups == null) return 0;
            foreach (var group in tourWithGroups.Groups)
            {
                total += GroupBLL.GetTotalCostOfGroup(group);
            }
            return total;
        }
    }
}
