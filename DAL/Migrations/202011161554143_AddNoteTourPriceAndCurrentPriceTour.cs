namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteTourPriceAndCurrentPriceTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "CurrentPrice", c => c.Int(nullable: false));
            AddColumn("dbo.TourPrices", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourPrices", "Note");
            DropColumn("dbo.Tours", "CurrentPrice");
        }
    }
}
