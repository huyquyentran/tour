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
    public class TourBLL
    {
        public static IEnumerable<Tour> ListTours(string Name = null)
        {

            return TourDAL.Get(
                t => Name == null || t.Name == Name,
                null,
                new List<Expression<Func<Tour, object>>>
                {
                    t=> t.TourPrices,
                    t=> t.TourLocations,
                    t=> t.TourType
                }
            );
        }
        public static void AddTour(Tour tour)
        {
            TourDAL.Add(tour);
        }
        public static void AddPriceToTour(TourPrice tourPrice)
        {

        }
    }
}
