using System;

namespace Core.Models
{
    public class TourType
    {
        public TourType () { }
        public TourType(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
