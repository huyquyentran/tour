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
        public static IList<Cost> ListCosts(int groupId)
        {
            return CostDAL.Get(
                c => c.GroupId == groupId,
                null,
                includeProperties: new List<Expression<Func<Cost, object>>>
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
                ex.Data.Add("Price", "Chi phí không hợp lệ");
            }
            else if (priceValue < 0)
            {
                ex.Data.Add("Price", "Chi phí không được nhỏ hơn 0");
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

        public static void RemoveCost(int id)
        {
            var cost = CostDAL.GetById(id);
            CostDAL.Remove(cost);
        }
    }
}
