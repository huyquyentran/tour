using Core.Enums;
using System;

namespace GUI.BindingClasses
{
    public class StaffBinding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; } = DateTime.UtcNow;
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }
    }
}
