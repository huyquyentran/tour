using Core.Common;
using Core.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TourPriceBLL
    {
        public static IEnumerable<TourPrice> ListTourPrices (int? TourId, DateTime? StartDate = null)
        {
            return TourPriceDAL.Get(
                tp => (!TourId.HasValue || tp.TourId == TourId) &&
                      (!StartDate.HasValue || (tp.StartDate <= StartDate.Value && StartDate.Value <= tp.EndDate)),
                tp => tp.OrderBy(tpo => tpo.StartDate)
            );
        }
        public static void IsValid(TourPrice tourPrice, int? Id = null)
        {
            Exception ex = new Exception("Validate excption");
            var inputDateRange = new DateRange(tourPrice.StartDate, tourPrice.EndDate);
            var tour = TourDAL.GetById(tourPrice.TourId);
            if (tour == null) ex.Data.Add("Tour", "Không tìm thấy tour");

            var tourPrices = TourPriceDAL.Find(tp => (tp.TourId == tour.Id) && (!Id.HasValue || tp.Id != Id));
            foreach (var x in tourPrices)
            {
                var sourceDateRange = new DateRange(x.StartDate, x.EndDate);
                if (!inputDateRange.BeforeOrAfter(sourceDateRange))
                {
                    ex.Data.Add("Thời gian", "Thời gian bắt đầu và kết thúc không hợp lệ");
                    break;
                }    
            }
            if (ex.Data.Count > 0)
            {
                throw ex;
            }
        }
        public static void Add(TourPrice tourPrice)
        {
            try
            {
                IsValid(tourPrice);
                TourPriceDAL.Add(tourPrice);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(int? id, TourPrice tourPrice)
        {
            try
            {
                IsValid(tourPrice, id);
                TourPriceDAL.Update(tourPrice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
