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
    public class TourLocationBLL
    {
        public static IEnumerable<TourLocations> ListTourLocationsByTourId(int? TourId)
        {
            return TourLocationDAL.Get(
                t => !TourId.HasValue || t.TourId == TourId,
                null,
                new List<Expression<Func<TourLocations, object>>>
                {
                    t=> t.Location
                }
            );
        }

        public static void UpdateRange(int? TourId, IList<TourLocations> tourLocations)
        {
            var sourceTourLocations = TourLocationDAL.Find(tl => tl.TourId == TourId);
            TourLocationDAL.RemoveRange(sourceTourLocations);
            TourLocationDAL.AddRange(tourLocations.ToList());
        }
    }
}
