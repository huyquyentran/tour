using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Group
    {
        public Group () { }
        public Group(DateTime startDate, DateTime endDate, int tourId)
        {
            StartDate = startDate;
            EndDate = endDate;
            TourId = tourId;
        }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int PriceTour {
            get
            {
                if (Tour == null) return 0;
                //Hiện tại giá tour được áp dụng vào đoạn dựa trên ngày bắt đầu của đoàn
                //Thực tế có thể nghiệp vụ là dựa vào ngày khách hàng tham gia vào đoàn 
                var tourPrices = Tour.TourPrices;
                foreach(var tourPrice in tourPrices)
                {
                    //tourPrice.StareDate <= StartDate <= tourPrice.EndDate
                    if (DateTime.Compare(StartDate, tourPrice.StartDate) >= 0 && 
                        DateTime.Compare(StartDate, tourPrice.EndDate) <= 0)
                    {
                        //Return thằng đầu tiên thỏa lun
                        return tourPrice.Price;
                    }    
                }
                //Nếu kiếm k ra thì return về 696969 ( đáng lẽ bên Tour phải có một Giá mặc địch
                //để return ở đay mà ông thầy đéo chịu, ai có idea gì thì bàn thêm sửa lại)
                return 696969;
            }
            set {;} 
        }
        public int PriceCosts
        {
            get
            {
                int temp = 0;
                if (Costs == null) return temp;
                foreach(var cost in Costs)
                {
                    temp += cost.Price;
                }
                return temp;
            }
            set {;}
        }
        public int TotalPrice
        {
            get
            {
                return PriceCosts + PriceTour;
            }
            set {; }
        }
        public ICollection<CustomerGroups> CustomerGroups { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Cost> Costs { get; set; }
    }
}
