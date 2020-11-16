using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TourTypeBLL
    {
        public static IEnumerable<TourType> ListTourTypes()
        {
            return TourTypeDAL.Get();
        }
    }
}
