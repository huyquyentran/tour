using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LocationBLL
    {
        public static IEnumerable<Location> ListLocations(int? TourId)
        {
            return LocationDAL.Get(
                t => t.TourLocations.Any(tl => !TourId.HasValue || tl.TourId == TourId),
                null,
                new List<Expression<Func<Location, object>>>
                {
                    t=> t.TourLocations
                }
            );
        }
    }
}
