using Core.Migrations;
using Core.Models;
using System.Data.Entity;

namespace Core.Data
{
    public class TourDbContext : DbContext
    {
        public TourDbContext() : base("name=TourDBConnectionString")
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TourDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Assignment>().HasKey(a => new { a.GroupId, a.StaffId });
            builder.Entity<CustomerGroups>().HasKey(cg => new { cg.CustomerId, cg.GroupId });
        }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLocations> TourLocations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<TourPrice> TourPrices { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<CustomerGroups> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
