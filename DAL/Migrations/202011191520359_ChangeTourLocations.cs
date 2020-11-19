namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTourLocations : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TourLocations");
            AddColumn("dbo.TourLocations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TourLocations", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TourLocations");
            DropColumn("dbo.TourLocations", "Id");
            AddPrimaryKey("dbo.TourLocations", new[] { "TourId", "LocationId" });
        }
    }
}
