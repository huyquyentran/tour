using Core.Models;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.BindingClasses
{
    public class TourStatisticBinding
    {
        public TourStatisticBinding(int tourId, string tourName, int totalGroup, int totalRevenue, int totalCost)
        {
            TourId = tourId;
            TourName = tourName;
            TotalGroup = totalGroup;
            TotalRevenue = totalRevenue;
            TotalCost = totalCost;
        }

        public int TourId { get; set; }
        public string TourName { get; set; }
        public int TotalGroup
        {
            get; set;
        }
        public int TotalRevenue
        {
            get; set;
        }
        public int TotalCost { get; set; }
        public int Profit
        {
            get
            {
                return TotalRevenue - TotalCost;
            }
        }
    }
}
