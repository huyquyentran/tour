using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public static class CustomerBLL
    {
        public static IList<Customer> ListCustomersNotInGroup(int groupId)
        {
            return CustomerDAL.Get(
                c => c.CustomerGroups.All(cg => cg.GroupId != groupId),
                null,
                new List<Expression<Func<Customer, object>>>
                {
                    c=> c.CustomerGroups,
                }
                ).ToList();
        }

        public static IList<Customer> ListCustomersInGroup(int groupId)
        {
            return CustomerDAL.Get(
                c => c.CustomerGroups.Any(cg => cg.GroupId == groupId),
                null,
                new List<Expression<Func<Customer, object>>>
                {
                    c=> c.CustomerGroups,
                }
                ).ToList();
        }
    }
}
