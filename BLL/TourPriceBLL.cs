﻿using Core.Common;
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
        public static IEnumerable<TourPrice> ListTourPrices (int? TourId)
        {
            return TourPriceDAL.Get(
                tp => (!TourId.HasValue || tp.TourId == TourId)
            );
        }
        public static void IsValid(TourPrice tourPrice, int? Id = null)
        {
            var inputDateRange = new DateRange(tourPrice.StartDate, tourPrice.EndDate);
            var tour = TourDAL.GetById(tourPrice.TourId);
            if (tour == null) throw new Exception("Tour NotFound");

            var tourPrices = TourPriceDAL.Find(tp => (tp.TourId == tour.Id) && (!Id.HasValue || tp.Id != Id));
            foreach (var x in tourPrices)
            {
                var sourceDateRange = new DateRange(x.StartDate, x.EndDate);
                if (!inputDateRange.BeforeOrAfter(sourceDateRange)) throw new Exception("Invalid StartDate or Enddatte");
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