namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Costs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CostTypeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostTypes", t => t.CostTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.CostTypeId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.CostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.StaffId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DoB = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        IdentificationNumber = c.String(),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerGroups",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.GroupId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        IdentificationNumber = c.String(),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        TourTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourTypes", t => t.TourTypeId, cascadeDelete: true)
                .Index(t => t.TourTypeId);
            
            CreateTable(
                "dbo.TourLocations",
                c => new
                    {
                        TourId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TourId, t.LocationId })
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TourPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes");
            DropForeignKey("dbo.TourPrices", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourLocations", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourLocations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.CustomerGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.CustomerGroups", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Costs", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Assignments", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Assignments", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Costs", "CostTypeId", "dbo.CostTypes");
            DropIndex("dbo.TourPrices", new[] { "TourId" });
            DropIndex("dbo.TourLocations", new[] { "LocationId" });
            DropIndex("dbo.TourLocations", new[] { "TourId" });
            DropIndex("dbo.Tours", new[] { "TourTypeId" });
            DropIndex("dbo.CustomerGroups", new[] { "GroupId" });
            DropIndex("dbo.CustomerGroups", new[] { "CustomerId" });
            DropIndex("dbo.Assignments", new[] { "StaffId" });
            DropIndex("dbo.Assignments", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "TourId" });
            DropIndex("dbo.Costs", new[] { "GroupId" });
            DropIndex("dbo.Costs", new[] { "CostTypeId" });
            DropTable("dbo.TourTypes");
            DropTable("dbo.TourPrices");
            DropTable("dbo.Locations");
            DropTable("dbo.TourLocations");
            DropTable("dbo.Tours");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerGroups");
            DropTable("dbo.Staffs");
            DropTable("dbo.Assignments");
            DropTable("dbo.Groups");
            DropTable("dbo.CostTypes");
            DropTable("dbo.Costs");
        }
    }
}
