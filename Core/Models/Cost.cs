using System;

namespace Core.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public int CostTypeId { get; set; }
        public CostType CostType { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }
    }
}
