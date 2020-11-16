using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Location
    {
        public Location () { }
        public Location(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TourLocations> TourLocations { get; set; }
    }
}
