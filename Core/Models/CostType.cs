using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class CostType
    {
        public CostType(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Cost> Costs { get; set; }
    }
}
