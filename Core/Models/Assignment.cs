using Core.Enums;

namespace Core.Models
{
    public class Assignment
    {
        public Assignment ( ) { }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Position Position { get; set; }
    }
}
