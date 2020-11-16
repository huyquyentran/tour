using System;

namespace Core.Models
{
    public class CustomerGroups
    {
        public CustomerGroups () { }
        public CustomerGroups(int groupId, int customerId, DateTime joinDate)
        {
            GroupId = groupId;
            CustomerId = customerId;
            JoinDate = joinDate;
        }

        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
