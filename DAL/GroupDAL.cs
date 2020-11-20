using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Core.Models;

namespace DAL
{
    public class GroupDAL : BaseDAL<Group>
    {
        public static IList<Group> Get(string type = null, string value = null)
        {
            bool isNumeric = int.TryParse(value, out int valueInt);
            bool isDateTime = DateTime.TryParse(value, out DateTime valueDateTime);

            return Get(
                g => type == null || (
                    (type == "Id" && isNumeric && g.Id == valueInt) ||
                    (type == "Name" && g.Name.Contains(value.Trim())) ||
                    (type == "TourName" && g.Tour.Name.Contains(value.Trim())) ||
                    (type == "StartDate" && isDateTime && DbFunctions.TruncateTime(g.StartDate) == valueDateTime.Date) ||
                    (type == "EndDate" && isDateTime && DbFunctions.TruncateTime(g.EndDate) == valueDateTime.Date)
                    ),
                null,
                includeProperties: new List<Expression<Func<Group, object>>>
                {
                    g=> g.Tour,
                    g => g.Costs,
                    g => g.CustomerGroups,
                }
                ).ToList();
        }
    }
}
