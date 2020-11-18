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
        public static IEnumerable<Location> ListLocations()
        {
            return LocationDAL.Get();
        }

        public static Location LocationById(int id)
        {
            return LocationDAL.GetById(id);
        }
    }
}
