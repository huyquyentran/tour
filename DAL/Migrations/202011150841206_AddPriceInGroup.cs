namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceInGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "PriceTour", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "PriceCosts", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "TotalPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "TotalPrice");
            DropColumn("dbo.Groups", "PriceCosts");
            DropColumn("dbo.Groups", "PriceTour");
        }
    }
}
