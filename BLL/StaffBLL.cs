using Core;
using Core.Dtos;
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
    public static class StaffBLL
    {
        public static IList<Staff> ListStaffsNotInGroup(int groupId)
        {
            return StaffDAL.Get(
                s => s.Assignments.All(a => a.GroupId != groupId)
                ).ToList();
        }

        public static IList<Staff> ListStaffsInGroup(int groupId)
        {
            return StaffDAL.Get(
                s => s.Assignments.Any(a => a.GroupId == groupId),
                null,
                new List<Expression<Func<Staff, object>>>
                {
                    s=> s.Assignments,
                }
                ).ToList();
        }

        public static List<TourCountOfStaff> GetTourCountOfStaffs(DateTime? startDate, DateTime? endDate)
        {
            return StaffDAL.GetTourCountOfStaffs(startDate, endDate);
        }
    }
}
