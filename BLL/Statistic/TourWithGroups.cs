using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Statistic
{
    public class TourWithGroups
    {
        public Tour Tour { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
