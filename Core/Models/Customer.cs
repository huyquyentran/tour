using Core.Enums;
using System.Collections.Generic;

namespace Core.Models
{
    public class Customer
    {
        public Customer ( ) { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public ICollection<CustomerGroups> CustomerGroups { get; set; }
    }
}
