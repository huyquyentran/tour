using Core.Common;
using Core.Data;
using Core.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CostDAL : BaseDAL<Cost>
    {
        public static List<CostStatistic> GetCostStatisticsByTourId(int tourId, DateTime? startDate, DateTime? endDate)
        {
            string query = string.Format(@"
                    SELECT CostTypes.Id, CostTypes.Name, SUM(Costs.Price) as Price
                    FROM Groups, Costs, CostTypes
                    WHERE Groups.Id = Costs.GroupId
                    AND Costs.CostTypeId = CostTypes.Id
                    AND Groups.TourId = {0}
                   ", tourId);

            if (startDate != null && endDate != null)
            {
                query += string.Format(@"
                    AND Groups.StartDate >= CAST('{0}' as date) 
                    AND Groups.EndDate <= CAST('{1}' as date) 
                    ", startDate.ToString(), endDate.ToString());
            }

            query += "Group BY CostTypes.Id, CostTypes.Name";


            using (var context = new TourDbContext())
            {

                return context.Database.SqlQuery<CostStatistic>(query).ToList();
            }
        }
    }
}
