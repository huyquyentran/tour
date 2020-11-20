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
                        (type == "Name" && t.Name.Contains(value.Trim())) ||
                        (type == "Description" && t.Description.Contains(value.Trim())) ||
                        (type == "TourTypeName" && t.TourType.Name.Contains(value.Trim()))
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
            Exception ex = new Exception("Validate excption");
            if (String.IsNullOrWhiteSpace(tour.Name))
                ex.Data.Add("Tên", "Tên tour không được bỏ trống");
            if (String.IsNullOrWhiteSpace(tour.Description))
                ex.Data.Add("Mô tả", "Mô tả không được bỏ trống");
            if (tour.CurrentPrice < 0)
                ex.Data.Add("Giá", "Giá tiền phải lớn 0");
            if (TourTypeDAL.GetById(tour.TourTypeId) == null)
                ex.Data.Add("Thể Loại Tour", "Thể loại tour không tồn tại");
            if (TourDAL.Find(t => t.Name == tour.Name && (!Id.HasValue || t.Id != Id)).FirstOrDefault() != null)
                ex.Data.Add("Tên", "Tên tour đã tồn tại");
            if (ex.Data.Count > 0)
            {
                throw ex;
            }
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
        public static void Update(Tour tour)
        {
            try
            {
                IsValid(tour, tour.Id);
                TourDAL.Update(tour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int id)
        {
            var sourceTour = TourDAL.GetById(id);
            if (sourceTour == null) throw new Exception("Mã tour không tồn tại");
            TourDAL.Remove(sourceTour);
        }
    }
}
