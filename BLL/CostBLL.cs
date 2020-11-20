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
    public static class CostBLL
    {
        public static IList<Cost> ListCosts(int groupId, string type = null, string value = null)
        {
            bool isNumeric = int.TryParse(value, out int valueInt);
            bool isDateTime = DateTime.TryParse(value, out DateTime valueDateTime);

            return CostDAL.Get(
                c => c.GroupId == groupId &&
                (
                    type == null || (
                        (type == "Id" && isNumeric && c.Id == valueInt) ||
                        (type == "CostType" && c.CostType.Name.Contains(value.Trim())) ||
                        (type == "Price" && isNumeric && c.Price == valueInt) ||
                        (type == "Note" && c.Note.Contains(value.Trim()))
                        )
                ),
                null,
                new List<Expression<Func<Cost, object>>>
                {
                        c => c.CostType,
                        c => c.Group
                }
                ).ToList();
        }

        public static IList<CostType> ListCostTypes()
        {
            return CostTypeDAL.Get().ToList();
        }

        private static void ValidateCost(string price, string note)
        {
            Exception ex = new Exception("Validate excption");
            int priceValue;
            if (!int.TryParse(price, out priceValue))
            {
                ex.Data.Add("Price", "Giá không hợp lệ");
            }
            else if (priceValue < 0)
            {
                ex.Data.Add("Price", "Giá không được nhỏ hơn 0");
            }

            if (note.Trim().Length > 255)
            {
                ex.Data.Add("Note", "Ghi chú không được quá 255 ký tự");
            }

            if (ex.Data.Count > 0)
            {
                throw ex;
            }
        }

        public static Cost CreateCost(int groupId, int costTypeId, string price, string note)
        {
            ValidateCost(price, note);
            var priceValue = int.Parse(price);
            var cost = new Cost
            {
                GroupId = groupId,
                CostTypeId = costTypeId,
                Price = priceValue,
                Note = note,
            };
            CostDAL.Add(cost);
            return cost;
        }

        public static Cost EditCost(int id, int groupId, int costTypeId, string price, string note)
        {
            ValidateCost(price, note);
            var priceValue = int.Parse(price);
            var cost = new Cost
            {
                Id = id,
                GroupId = groupId,
                CostTypeId = costTypeId,
                Price = priceValue,
                Note = note,
            };

            CostDAL.Update(cost);
            return cost;
        }

        public static void RemoveCost(int id)
        {
            var cost = CostDAL.GetById(id);
            CostDAL.Remove(cost);
        }

        public static List<CostStatistic> GetCostStatisticsByTourId(int tourId, DateTime? startDate, DateTime? endDate)
        {
            return CostDAL.GetCostStatisticsByTourId(tourId, startDate, endDate);
        }
    }
}
