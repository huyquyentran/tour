using System;

namespace Core.Models
{
    public class TourPrice
    {
        public TourPrice () { }
        public TourPrice(int tourId, DateTime startDate, DateTime endDate, int price, string note)
        {
            TourId = tourId;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Note = note;
        }
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }
    }
}
