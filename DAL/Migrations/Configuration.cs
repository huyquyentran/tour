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
            if (!context.Staffs.Any())
            {
                IEnumerable<Staff> staffs = new List<Staff>
                {
                    new Staff {
                        Name = "Nhân viên 1",
                        Address="Địa chỉ 1",
                        IdentificationNumber="111111111",
                        PhoneNumber="08311111111",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Nhân viên 2",
                        Address="Địa chỉ 2",
                        IdentificationNumber="222222222",
                        PhoneNumber="08322222222",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Nhân viên 3",
                        Address="Địa chỉ 3",
                        IdentificationNumber="333333333",
                        PhoneNumber="08333333333",
                        Gender = Enums.Gender.Nam,
                    },
                    new Staff {
                        Name = "Nhân viên 4",
                        Address="Địa chỉ 4",
                        IdentificationNumber="444444444",
                        PhoneNumber="08344444444",
                        Gender = Enums.Gender.Nu,
                    },
                    new Staff {
                        Name = "Nhân viên 5",
                        Address="Địa chỉ 5",
                        IdentificationNumber="555555555",
                        PhoneNumber="08355555555",
                        Gender = Enums.Gender.Nam,
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
                        Name = "Khách hàng 1",
                        Address="Địa chỉ 1",
                        IdentificationNumber="111111111",
                        PhoneNumber="08311111111",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Khách hàng 2",
                        Address="Địa chỉ 2",
                        IdentificationNumber="222222222",
                        PhoneNumber="08322222222",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Khách hàng 3",
                        Address="Địa chỉ 3",
                        IdentificationNumber="333333333",
                        PhoneNumber="08333333333",
                        Gender = Enums.Gender.Nam,
                    },
                    new Customer {
                        Name = "Khách hàng 4",
                        Address="Địa chỉ 4",
                        IdentificationNumber="444444444",
                        PhoneNumber="08344444444",
                        Gender = Enums.Gender.Nu,
                    },
                    new Customer {
                        Name = "Khách hàng 5",
                        Address="Địa chỉ 5",
                        IdentificationNumber="555555555",
                        PhoneNumber="08355555555",
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
                    new TourType( "Thể loại tour 1"),
                    new TourType( "Thể loại tour 2"),
                    new TourType( "Thể loại tour 3"),
                    new TourType( "Thể loại tour 4"),
                    new TourType( "Thể loại tour 5")
                };
                context.TourTypes.AddRange(tourTypes);
                context.SaveChanges();
            }
            if (!context.Locations.Any())
            {
                IEnumerable<Location> locations = new List<Location>
                {
                    new Location( "Địa điểm 1"),
                    new Location( "Địa điểm 2"),
                    new Location( "Địa điểm 3"),
                    new Location( "Địa điểm 4"),
                    new Location( "Địa điểm 5")
                };
                context.Locations.AddRange(locations);
                context.SaveChanges();
            }
            if (!context.CostTypes.Any())
            {
                IEnumerable<CostType> costTypes = new List<CostType>
                {
                    new CostType( "Thể loại chi phí 1"),
                    new CostType( "Khách sạn"),
                    new CostType( "Thể loại chi phí 3"),
                    new CostType( "Thể loại chi phí 4"),
                    new CostType( "Thể loại chi phí 5")
                };
                context.CostTypes.AddRange(costTypes);
                context.SaveChanges();
            }
            if (!context.Tours.Any())
            {
                IEnumerable<Tour> tours = new List<Tour>
                {
                    new Tour("Tour thứ 1",1111111,"Mô tả tour 1",1),
                    new Tour("Tour thứ 2",2222222,"Mô tả tour 2",2),
                    new Tour("Tour thứ 3",3333333,"Mô tả tour 3",3),
                    new Tour("Tour thứ 4",4444444,"Mô tả tour 4",4),
                    new Tour("Tour thứ 5",5555555,"Mô tả tour 5",5)
                };
                context.Tours.AddRange(tours);
                context.SaveChanges();
            }
            if (!context.TourPrices.Any())
            {
                IEnumerable<TourPrice> tourPrices = new List<TourPrice>
                {
                    new TourPrice(1, new DateTime(2020, 7, 20), new DateTime(2020, 8, 20), 3000000,"Chú thích 1"),
                    new TourPrice(1, new DateTime(2020, 9, 20), new DateTime(2020, 11, 20), 4000000,"Chú thích 2"),
                    new TourPrice(2, new DateTime(2020, 7, 20), new DateTime(2020, 8, 20), 2000000,"Chú thích 3"),
                    new TourPrice(3, new DateTime(2020, 8, 20), new DateTime(2020, 9, 20), 5000000,"Chú thích 4"),
                    new TourPrice(4, new DateTime(2020, 7, 20), new DateTime(2020, 10, 20), 1500000,"Chú thích 5"),
                    new TourPrice(5, new DateTime(2020, 7, 20), new DateTime(2020, 10, 20), 1500000,"Chú thích 6"),
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
                };
                context.TourLocations.AddRange(tourLocations);
                context.SaveChanges();
            }
            if (!context.Groups.Any())
            {
                IEnumerable<Group> groups = new List<Group>
                {
                    new Group(new DateTime(2020, 7, 20), new DateTime(2020, 7, 24), 1),
                    new Group(new DateTime(2020, 8, 11), new DateTime(2020, 8, 15), 1),
                    new Group(new DateTime(2020, 7, 25), new DateTime(2020, 7, 29), 2),
                    new Group(new DateTime(2020, 10, 11), new DateTime(2020, 10, 15), 2),
                    new Group(new DateTime(2020, 11, 1), new DateTime(2020, 11, 2), 2),
                    new Group(new DateTime(2020, 7, 30), new DateTime(2020, 8, 5), 3),
                    new Group(new DateTime(2020, 7, 20), new DateTime(2020, 7, 22), 4),
                    new Group(new DateTime(2020, 11, 5), new DateTime(2020, 10, 8), 4),
                    new Group(new DateTime(2020, 11, 5), new DateTime(2020, 11, 8), 4),
                    new Group(new DateTime(2020, 7, 20), new DateTime(2020, 7, 27), 5),
                    new Group(new DateTime(2020, 9, 20), new DateTime(2020, 9, 21), 5),
                    new Group(new DateTime(2020,11, 1), new DateTime(2020, 11, 6), 5),
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
                        Note="Note của Cost 1",
                    },
                    new Cost{
                        CostTypeId=1,
                        GroupId=1,
                        Price=30000,
                        Note="Note của Cost 2",
                    },
                    new Cost{
                        CostTypeId=2,
                        GroupId=2,
                        Price=20000,
                        Note="Note của Cost 3",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=3,
                        Price=20000,
                        Note="Note của Cost 4",
                    },
                    new Cost{
                        CostTypeId=5,
                        GroupId=4,
                        Price=20000,
                        Note="Note của Cost 5",
                    },
                    new Cost{
                        CostTypeId=3,
                        GroupId=1,
                        Price=20000,
                        Note="Note của Cost 6",
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
                        StaffId=3,
                        Position=Enums.Position.TaiXe,
                    },
                    new Assignment{
                        GroupId=2,
                        StaffId=1,
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
