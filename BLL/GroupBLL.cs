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
        public static IList<Group> ListGroups(string type = null, string value = null)
        {
            return GroupDAL.Get(type, value);
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

        public static void SaveListCustomersOfGroup(int groupId, List<int> customersId)
        {
            // List of customer in group
            var customersInGroup = CustomerBLL.ListCustomersInGroup(groupId);
            var customersInGroupId = customersInGroup.Select(c => c.Id).ToList();


            // List of new customer wants to add to the group
            var newCustomersId = customersId.Except(customersInGroupId);

            // List of customer wants to remove from the group
            var removeCustomersId = customersInGroupId.Except(customersId);

            var newCustomersList = new List<CustomerGroups>();
            foreach (var customerId in newCustomersId)
            {
                newCustomersList.Add(
                    new CustomerGroups
                    {
                        CustomerId = customerId,
                        GroupId = groupId,
                        JoinDate = DateTime.Now,
                    });
            }

            var removeCustomersList = new List<CustomerGroups>();
            foreach (var customerId in removeCustomersId)
            {
                removeCustomersList.Add(
                    new CustomerGroups
                    {
                        CustomerId = customerId,
                        GroupId = groupId
                    });
            }

            CustomerGroupDAL.AddRange(newCustomersList);
            CustomerGroupDAL.RemoveRange(removeCustomersList);
        }

        public static void SaveListStaffsOfGroup(int groupId, List<Assignment> assignments)
        {
            // List of staff in group
            var staffsInGroup = StaffBLL.ListStaffsInGroup(groupId);
            var staffsInGroupId = staffsInGroup.Select(s => s.Id).ToList();

            var removeStaffsList = new List<Assignment>();
            foreach (var staffId in staffsInGroupId)
            {
                removeStaffsList.Add(
                    new Assignment
                    {
                        StaffId = staffId,
                        GroupId = groupId
                    });
            }

            AssignmentDAL.RemoveRange(removeStaffsList);
            AssignmentDAL.AddRange(assignments);
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

            return GetPriceTourOfGroup(group) * group.CustomerGroups.Count;
        }
    }
}
