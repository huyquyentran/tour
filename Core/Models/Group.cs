using Core.Common;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Group
    {
        public Group () { }
        public Group(DateTime startDate, DateTime endDate, int tourId)
        {
            StartDate = startDate;
            EndDate = endDate;
            TourId = tourId;
        }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int PriceTour {
            get
            {
                if (Tour == null) return 0;
                var tourPrices = Tour.TourPrices;
                foreach(var tourPrice in tourPrices)
                {
                    if (new DateRange(tourPrice.StartDate, tourPrice.EndDate).Includes(StartDate))
                    {
                        return tourPrice.Price;
                    }    
                }
                return Tour.CurrentPrice;
            }
            set {;} 
        }
        public int PriceCosts
        {
            get
            {
                int temp = 0;
                if (Costs == null) return temp;
                foreach(var cost in Costs)
                {
                    temp += cost.Price;
                }
                return temp;
            }
            set {;}
        }
        public int TotalPrice
        {
            get
            {
                return PriceCosts + PriceTour;
            }
            set {; }
        }
        public ICollection<CustomerGroups> CustomerGroups { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Cost> Costs { get; set; }
    }
}
