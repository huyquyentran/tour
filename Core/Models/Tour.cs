using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Tour
    {
        public Tour(string name, string description, int tourTypeId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            TourTypeId = tourTypeId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TourTypeId { get; set; }
        public TourType TourType { get; set; }
        public ICollection<TourPrice> TourPrices { get; set; }
        public ICollection<TourLocations> TourLocations { get; set; }
    }
}
