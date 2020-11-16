using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Tour
    {
        public Tour() { }
        public Tour(string name, int currentPrice, string description, int tourTypeId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            CurrentPrice = currentPrice;
            TourTypeId = tourTypeId;
        }
        public int CurrentPrice { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TourTypeId { get; set; }
        public TourType TourType { get; set; }
        public ICollection<TourPrice> TourPrices { get; set; } = new List<TourPrice>();
        public ICollection<TourLocations> TourLocations { get; set; } = new List<TourLocations>();
    }
}
