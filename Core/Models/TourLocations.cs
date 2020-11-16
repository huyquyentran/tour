namespace Core.Models
{
    public class TourLocations
    {
        public TourLocations () { }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int Order { get; set; }
    }
}
