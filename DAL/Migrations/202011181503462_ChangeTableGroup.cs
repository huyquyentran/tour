namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Name", c => c.String());
            DropColumn("dbo.Groups", "PriceTour");
            DropColumn("dbo.Groups", "PriceCosts");
            DropColumn("dbo.Groups", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "TotalPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "PriceCosts", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "PriceTour", c => c.Int(nullable: false));
            DropColumn("dbo.Groups", "Name");
        }
    }
}
