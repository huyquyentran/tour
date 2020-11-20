using Core.Common;
using Core.Data;
using Core.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class StaffDAL : BaseDAL<Staff>
    {
        public static List<TourCountOfStaff> GetTourCountOfStaffs(DateTime? startDate, DateTime? endDate)
        {
            string query = @"
                SELECT Staffs.Id AS StaffId, Staffs.Name AS StaffName, COUNT(GroupId) As TourCount
                FROM Staffs LEFT JOIN Assignments ON Assignments.StaffId = Staffs.Id
	            LEFT JOIN Groups ON Groups.Id = Assignments.GroupId";

            if (startDate != null && endDate != null)
            {
                query += string.Format(@"
                    AND Groups.StartDate >= CAST('{0}' as date) 
                    AND Groups.StartDate <= CAST('{1}' as date) 
                    ", startDate.ToString(), endDate.ToString());
            }

            query += "GROUP BY Staffs.Id, Staffs.Name";


            using (var context = new TourDbContext())
            {
                return context.Database.SqlQuery<TourCountOfStaff>(query).ToList();
            }
        }
    }
}
