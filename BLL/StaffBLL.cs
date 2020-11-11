using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StaffBLL
    {
        public static IList<Staff> ListStaff()
        {
            return StaffDAL.Get().ToList();
        }
    }
}
