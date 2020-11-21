using Core.Data;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TourDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(TourDbContext context)
        { 
            //if (context.Database.Exists())
            //{
            //    return;
            //}
            if (!context.Staffs.Any())
            {
                IEnumerable<Staff> staffs = new List<Staff>
                {
                    new Staff {
                        Name = "Nguyễn Nhân Viên",
                        Address="Quận 1",
                        IdentificationNumber="025025025",
                        PhoneNumber="0909090909",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Võ Nhân Viên",
                        Address="Quận 2",
                        IdentificationNumber="025025022",
                        PhoneNumber="0832522622",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Trần Nhân Viên",
                        Address="Quận 3",
                        IdentificationNumber="025025023",
                        PhoneNumber="08333333333",
                        Gender = Enums.Gender.Nu,
                    },
                    new Staff {
                        Name = "Trương Nhân Viên",
                        Address="Quận 4",
                        IdentificationNumber="012151313",
                        PhoneNumber="0909090999",
                        Gender = Enums.Gender.Nu,
                    },
                    new Staff {
                        Name = "Nguyễn Làm Công",
                        Address="Quận 5",
                        IdentificationNumber="0512025215",
                        PhoneNumber="08355555555",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Trần Làm Công",
                        Address="Quận 6",
                        IdentificationNumber="1235136514",
                        PhoneNumber="08355523525",
                        Gender = Enums.Gender.Nu,
                    },
                };
                context.Staffs.AddRange(staffs);
                context.SaveChanges();
            }
            if (!context.Customers.Any())
            {
                IEnumerable<Customer> customers = new List<Customer>
                {
                    new Customer {
                        Name = "Trần Văn Hoàng",
                        Address="Tân Bình",
                        IdentificationNumber="3117410000",
                        PhoneNumber="0909090909",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Trần Lê Huy Quyền",
                        Address="Nhà Bè",
                        IdentificationNumber="0253522565",
                        PhoneNumber="083254666",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Trương Đình Thiện",
                        Address="Củ Chi",
                        IdentificationNumber="025025023",
                        PhoneNumber="0834525253",
                        Gender = Enums.Gender.Nu,
                    },
                    new Customer {
                        Name = "Nguyễn Thiên Hữu",
                        Address="Phú Thọ",
                        IdentificationNumber="025025022",
                        PhoneNumber="083487524",
                        Gender = Enums.Gender.Nu,
                    },
                    new Customer {
                        Name = "Nguyễn Văn A",
                        Address="Buôn Ma Thuộc",
                        IdentificationNumber="025456023",
                        PhoneNumber="083558555",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Nguyễn Văn B",
                        Address="Gò Vấp",
                        IdentificationNumber="025456023",
                        PhoneNumber="083558555",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Nguyễn Văn C",
                        Address="Hồ Chí Minh",
                        IdentificationNumber="025456023",
                        PhoneNumber="083558555",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Nguyễn Văn D",
                        Address="Gia Lai",
                        IdentificationNumber="025456023",
                        PhoneNumber="083558555",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Nguyễn Văn Phú",
                        Address="Phú Nhuận",
                        IdentificationNumber="025456023",
                        PhoneNumber="083558555",
                        Gender = Enums.Gender.Nam,
                    },
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
            if (!context.TourTypes.Any())
            {
                IEnumerable<TourType> tourTypes = new List<TourType>
                {
                    new TourType( "Du lịch tham quan"),
                    new TourType( "Du lịch ẩm thực"),
                    new TourType( "Du lịch văn hóa"),
                    new TourType( "Du lịch MICE"),
                    new TourType( "Team building")
                };
                context.TourTypes.AddRange(tourTypes);
                context.SaveChanges();
            }
            if (!context.Locations.Any())
            {
                IEnumerable<Location> locations = new List<Location>
                {
                    new Location( "Vườn Quốc Gia Ba Bể"),
                    new Location( "Động Nàng Tiên"),
                    new Location( "Động Puông"),
                    new Location( "Thác Đầu Đẳng"),
                    new Location( "Chùa Thạch Long"),
                    new Location( "Thác Đầu Đẳng"),
                    new Location( "Chợ Đông Kinh"),
                    new Location( "Hồ Núi Cốc"),
                    new Location( "Bảo tàng dân tộc Việt Nam"),
                    new Location( "Suối Mỏ Gà"),
                    new Location( "Hang Phượng Hoàng"),
                    new Location( "Đồi chè Tân Cương"),
                    new Location( "Khu di tích Suối Mỡ"),
                    new Location( "Hồ Cấm Sơn"),
                    new Location( "Làng rượu Vân Hà"),
                    new Location( "Vịnh Hạ Long"),
                    new Location( "Đảo Tuần Châu"),
                    new Location( "Núi Yên Tử"),
                    new Location( "Đền Quốc Mẫu Âu Cơ"),
                    new Location( "Chùa Thạch Long")
                };
                context.Locations.AddRange(locations);
                context.SaveChanges();
            }
            if (!context.CostTypes.Any())
            {
                IEnumerable<CostType> costTypes = new List<CostType>
                {
                    new CostType( "Ăn sáng"),
                    new CostType( "Khách sạn"),
                    new CostType( "Ăn trưa"),
                    new CostType( "Sửa xe"),
                    new CostType( "Phát sinh"),
                    new CostType( "Ăn tối")
                };
                context.CostTypes.AddRange(costTypes);
                context.SaveChanges();
            }
            if (!context.Tours.Any())
            {
                IEnumerable<Tour> tours = new List<Tour>
                {
                    new Tour("Tham quan đảo khỉ",1000000,"Có kèo solo với khỉ",1),
                    new Tour("Tham quan Tây Nguyên",2000000,"Có kèo solo với người Tây Nguyên",3),
                    new Tour("Tham quan Master Chief",5000000,"Gạ kèo solo Gordon Ramsey",2),
                    new Tour("Tham quan Củ Chi",10000000,"Một ngày ăn ngủ dưới địa đạo",4),
                    new Tour("Tham quan Nhà Trắng",900000000,"Một ngày ngủ thử giường tổng thống",5)
                };
                context.Tours.AddRange(tours);
                context.SaveChanges();
            }
            if (!context.TourPrices.Any())
            {
                IEnumerable<TourPrice> tourPrices = new List<TourPrice>
                {
                    new TourPrice(1, new DateTime(2020, 7, 20), new DateTime(2020, 8, 20), 3000000,"Giảm giá đặc biệt"),
                    new TourPrice(1, new DateTime(2020, 9, 20), new DateTime(2020, 11, 20), 4000000,"Giá khởi điểm"),
                    new TourPrice(2, new DateTime(2020, 7, 20), new DateTime(2020, 8, 20), 2000000,"Giá khởi điểm"),
                    new TourPrice(2, new DateTime(2020, 8, 21), new DateTime(2020, 8, 25), 1000000,"Giảm giá nhẹ"),
                    new TourPrice(2, new DateTime(2020, 8, 26), new DateTime(2020, 8, 30), 500000,"Giảm thêm tí"),
                    new TourPrice(3, new DateTime(2020, 8, 20), new DateTime(2020, 9, 20), 5000000,"Giá khởi điểm"),
                    new TourPrice(3, new DateTime(2020, 9, 21), new DateTime(2020, 9, 26), 2500000,"Giảm giá giữa tháng"),
                    new TourPrice(4, new DateTime(2020, 7, 20), new DateTime(2020, 10, 20), 1500000,"Giá khởi điểm"),
                    new TourPrice(5, new DateTime(2020, 7, 20), new DateTime(2020, 10, 20), 1500000,"Giá khởi điểm6"),
                };
                context.TourPrices.AddRange(tourPrices);
                context.SaveChanges();
            }
            if (!context.TourLocations.Any())
            {
                IEnumerable<TourLocations> tourLocations = new List<TourLocations>
                {
                    new TourLocations{
                        LocationId=1,
                        TourId=1,
                        Order=1,
                    },
                    new TourLocations{
                        LocationId=2,
                        TourId=1,
                        Order=2,
                    },
                    new TourLocations{
                        LocationId=3,
                        TourId=1,
                        Order=3,
                    },
                    new TourLocations{
                        LocationId=1,
                        TourId=2,
                        Order=1,
                    },
                    new TourLocations{
                        LocationId=1,
                        TourId=3,
                        Order=1,
                    },
                    new TourLocations{
                        LocationId=1,
                        TourId=4,
                        Order=1,
                    },
                    new TourLocations{
                        LocationId=1,
                        TourId=5,
                        Order=1,
                    },
                    new TourLocations{
                        LocationId=2,
                        TourId=5,
                        Order=2,
                    },
                    new TourLocations{
                        LocationId=2,
                        TourId=3,
                        Order=2,
                    },

                    new TourLocations{
                        LocationId=6,
                        TourId=2,
                        Order=2,
                    },
                    new TourLocations{
                        LocationId=8,
                        TourId=2,
                        Order=3,
                    },
                    new TourLocations{
                        LocationId=7,
                        TourId=2,
                        Order=4,
                    },
                    new TourLocations{
                        LocationId=10,
                        TourId=2,
                        Order=5,
                    },
                    new TourLocations{
                        LocationId=9,
                        TourId=3,
                        Order=2,
                    },
                    new TourLocations{
                        LocationId=12,
                        TourId=3,
                        Order=3,
                    },
                    new TourLocations{
                        LocationId=11,
                        TourId=3,
                        Order=4,
                    },
                    new TourLocations{
                        LocationId=6,
                        TourId=3,
                        Order=5,
                    },
                };
                context.TourLocations.AddRange(tourLocations);
                context.SaveChanges();
            }
            if (!context.Groups.Any())
            {
                IEnumerable<Group> groups = new List<Group>
                {
                    new Group("Đoàn Khỉ 1", new DateTime(2020, 7, 20), new DateTime(2020, 7, 24), 1),
                    new Group("Đoàn Khỉ 2", new DateTime(2020, 8, 11), new DateTime(2020, 8, 15), 1),
                    new Group("Đoàn Tây Nguyên 1", new DateTime(2020, 7, 25), new DateTime(2020, 7, 29), 2),
                    new Group("Đoàn Tây Nguyên 2", new DateTime(2020, 10, 11), new DateTime(2020, 10, 15), 2),
                    new Group("Đoàn Ramsey 1", new DateTime(2020, 11, 1), new DateTime(2020, 11, 2), 3),
                    new Group("Đoàn Ramsey 2", new DateTime(2020, 7, 30), new DateTime(2020, 8, 5), 3),
                    new Group("Đoàn Củ Chi 1", new DateTime(2020, 7, 20), new DateTime(2020, 7, 22), 4),
                    new Group("Đoàn Củ Chi 2", new DateTime(2020, 11, 5), new DateTime(2020, 10, 8), 4),
                    new Group("Đoàn Củ Chi 3", new DateTime(2020, 11, 5), new DateTime(2020, 11, 8), 4),
                    new Group("Đoàn Nhà Trắng 1", new DateTime(2020, 7, 20), new DateTime(2020, 7, 27), 5),
                    new Group("Đoàn Nhà Trắng 2", new DateTime(2020, 9, 20), new DateTime(2020, 9, 21), 5),
                    new Group("Đoàn Nhà Trắng 3", new DateTime(2020,11, 1), new DateTime(2020, 11, 6), 5),
                };
                context.Groups.AddRange(groups);
                context.SaveChanges();
            }
            if (!context.Costs.Any())
            {
                IEnumerable<Cost> costs = new List<Cost>
                {
                    new Cost{
                        CostTypeId=1,
                        GroupId=1,
                        Price=20000,
                        Note="Ăn bánh mì",
                    },
                    new Cost{
                        CostTypeId=2,
                        GroupId=1,
                        Price=80000,
                        Note="Khách muốn đổi phòng",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=1,
                        Price=80000,
                        Note="Bánh bao",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=1,
                        Price=80000,
                        Note="Bánh bao",
                    },
                    new Cost{
                        CostTypeId=2,
                        GroupId=2,
                        Price=200000,
                        Note="Nhận phòng trễ bị phạt thêm",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=2,
                        Price=20000,
                        Note="Ăn bánh mì",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=2,
                        Price=80000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=2,
                        Price=80000,
                        Note="Bánh bao",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=3,
                        Price=80000,
                        Note="Bánh bao",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=3,
                        Price=500000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=3,
                        Price=20000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=4,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=4,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=4,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=5,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=5,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=5,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=6,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=6,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=6,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=7,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=7,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=7,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=8,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=8,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=8,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=9,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=9,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=9,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=10,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=10,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=10,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=11,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=11,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=11,
                        Price=50000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=12,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=12,
                        Price=30000,
                        Note="",
                    },
                    new Cost{
                        CostTypeId=6,
                        GroupId=12,
                        Price=50000,
                        Note="",
                    },

                };
                context.Costs.AddRange(costs);
                context.SaveChanges();
            }
            if (!context.CustomerGroups.Any())
            {
                IEnumerable<CustomerGroups> customerGroups = new List<CustomerGroups>
                {
                    //Lười seed quá ... Đáng lẽ nên rằng buộc thời gian join và thời gian group start and end
                    new CustomerGroups(1,1,DateTime.UtcNow),
                    new CustomerGroups(1,2,DateTime.UtcNow),
                    new CustomerGroups(1,3,DateTime.UtcNow),
                    new CustomerGroups(1,4,DateTime.UtcNow),
                    new CustomerGroups(1,5,DateTime.UtcNow),
                    new CustomerGroups(2,1,DateTime.UtcNow),
                    new CustomerGroups(2,2,DateTime.UtcNow),
                    new CustomerGroups(2,3,DateTime.UtcNow),
                    new CustomerGroups(2,4,DateTime.UtcNow),
                    new CustomerGroups(2,5,DateTime.UtcNow),
                    new CustomerGroups(3,3,DateTime.UtcNow),
                    new CustomerGroups(3,4,DateTime.UtcNow),
                    new CustomerGroups(3,5,DateTime.UtcNow),
                    new CustomerGroups(3,6,DateTime.UtcNow),
                    new CustomerGroups(4,7,DateTime.UtcNow),
                    new CustomerGroups(4,8,DateTime.UtcNow),
                    new CustomerGroups(4,9,DateTime.UtcNow),
                    new CustomerGroups(4,1,DateTime.UtcNow),
                    new CustomerGroups(5,2,DateTime.UtcNow),
                    new CustomerGroups(5,3,DateTime.UtcNow),
                    new CustomerGroups(8,3,DateTime.UtcNow),
                    new CustomerGroups(9,4,DateTime.UtcNow),
                    new CustomerGroups(6,5,DateTime.UtcNow),
                    new CustomerGroups(7,4,DateTime.UtcNow),
                    new CustomerGroups(8,4,DateTime.UtcNow),

                };
                context.CustomerGroups.AddRange(customerGroups);
                context.SaveChanges();
            }
            if (!context.Assignments.Any())
            {
                IEnumerable<Assignment> assignments = new List<Assignment>
                {
                    //Lười seed quá ... Đáng lẽ nên rằng buộc thời gian join và thời gian group start and end
                    new Assignment{
                        GroupId=1,
                        StaffId=1,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=1,
                        StaffId=2,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=1,
                        StaffId=3,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=1,
                        StaffId=4,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=2,
                        StaffId=5,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=2,
                        StaffId=6,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=2,
                        StaffId=1,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=2,
                        StaffId=2,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=3,
                        StaffId=3,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=3,
                        StaffId=4,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=3,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=3,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=4,
                        StaffId=1,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=4,
                        StaffId=2,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=4,
                        StaffId=3,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=4,
                        StaffId=4,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=5,
                        StaffId=5,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=5,
                        StaffId=6,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=5,
                        StaffId=1,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=5,
                        StaffId=2,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=6,
                        StaffId=3,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=6,
                        StaffId=4,
                        Position=Enums.Position.HuongDanVien,
                    },
                    new Assignment{
                        GroupId=6,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=6,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=7,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=7,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=8,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=8,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=9,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=9,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=10,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=10,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=11,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=11,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },
                    new Assignment{
                        GroupId=12,
                        StaffId=5,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=12,
                        StaffId=6,
                        Position=Enums.Position.TruongDoan,
                    },

                };
                context.Assignments.AddRange(assignments);
                context.SaveChanges();
            }
            //context.Database.Delete();
        }
    }
}
