using Core.Common;
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
    public class TourBLL
    {
        public static IEnumerable<Tour> ListTours(string type = null, string value = null)
        {
            int valueInt;
            bool isNumeric = int.TryParse(value, out valueInt);
            return TourDAL.Get(
                t => type == null || (
                        (type == "Id" && isNumeric && t.Id == valueInt) ||
                        (type == "Name" && t.Name.Contains(value)) ||
                        (type == "Description" && t.Description.Contains(value))
                    ),
                null,
                new List<Expression<Func<Tour, object>>>
                {
                        t=> t.TourType
                }
            );
        }
        public static void IsValid(Tour tour, int? Id = null)
        {
            if (String.IsNullOrWhiteSpace(tour.Name))
                throw new Exception("Tên không được bỏ trống");
            if (String.IsNullOrWhiteSpace(tour.Description))
                throw new Exception("Mô tả không được bỏ trống");
            if (tour.CurrentPrice < 0)
                throw new Exception("Giá phải lớn hơn 0");
            if (TourTypeDAL.GetById(tour.TourTypeId) == null)
                throw new Exception("Không tồn tại thể loại tour này");
            if (TourDAL.Find(t => t.Name == tour.Name && (!Id.HasValue || t.Id != Id)).FirstOrDefault() != null)
                throw new Exception("Tên tour đã tồn tại");
        }
        public static void Add(Tour tour)
        {
            try
            {
                IsValid(tour);
                TourDAL.Add(tour);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void Update(int id, Tour tour)
        {
            try
            {
                IsValid(tour, id);

                var sourceTour = TourDAL.GetById(id);
                if (sourceTour == null) throw new Exception("Mã tour không tồn tại");

                sourceTour.Name = tour.Name;
                sourceTour.Description = tour.Description;
                sourceTour.CurrentPrice = tour.CurrentPrice;
                sourceTour.TourTypeId = tour.TourTypeId;
                TourDAL.Update(sourceTour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
