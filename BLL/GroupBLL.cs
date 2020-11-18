using Core.Common;
using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public static class GroupBLL
    {
        public static IList<Group> ListGroups()
        {
            return GroupDAL.Get(
                includeProperties: new List<Expression<Func<Group, object>>>
                {
                    g=> g.Tour,
                    g => g.Costs,
                    g => g.CustomerGroups,
                }
                ).ToList();
        }

        private static void ValidateGroup(string name, DateTime startDate, DateTime endDate)
        {
            Exception ex = new Exception("Validate excption");
            if (name.Trim().Length == 0)
            {
                ex.Data.Add("Name", "Tên đoàn không được bỏ trống");
            }
            else if (name.Trim().Length > 255)
            {
                ex.Data.Add("Name", "Tên đoàn không được quá 255 ký tự");
            }

            if (DateTime.Compare(startDate, endDate) > 0)
            {
                ex.Data.Add("StartDate", "Thời gian bắt đầu phải trước thời gian kết thúc");
            }
            if (ex.Data.Count > 0)
            {
                throw ex;
            }
        }

        public static Group CreateGroup(string name, DateTime startDate, DateTime endDate, int tourId)
        {
            ValidateGroup(name, startDate, endDate);
            var group = new Group(name, startDate, endDate, tourId);
            GroupDAL.Add(group);
            return group;
        }

        public static Group EditGroup(int id, string name, DateTime startDate, DateTime endDate, int tourId)
        {
            ValidateGroup(name, startDate, endDate);
            var group = new Group(name, startDate, endDate, tourId)
            {
                Id = id
            };

            GroupDAL.Update(group);
            return group;
        }

        public static void RemoveGroup(int id)
        {
            var group = GroupDAL.GetById(id);
            GroupDAL.Remove(group);
        }

        public static int GetTotalCostOfGroup(Group group)
        {
            int total = 0;
            if (group.Costs == null) return total;
            foreach (var cost in group.Costs)
            {
                total += cost.Price;
            }
            return total;
        }

        public static int GetPriceTourOfGroup(Group group)
        {
            if (group.Tour == null) return 0;
            foreach (var tourPrice in group.Tour.TourPrices)
            {
                if (new DateRange(tourPrice.StartDate, tourPrice.EndDate).Includes(group.StartDate))
                {
                    return tourPrice.Price;
                }
            }
            return group.Tour.CurrentPrice;
        }

        public static int GetRevenueOfGroup(Group group)
        {
            if (group.CustomerGroups == null || group.CustomerGroups.Count == 0)
            {
                return 0;
            }

            return (GetPriceTourOfGroup(group) * group.CustomerGroups.Count) - GetTotalCostOfGroup(group);
        }
    }
}
