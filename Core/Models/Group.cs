using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Group
    {
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
        public ICollection<CustomerGroups> CustomerGroups { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Cost> Costs { get; set; }
    }
}
