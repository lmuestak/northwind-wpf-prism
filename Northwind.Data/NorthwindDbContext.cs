using Microsoft.EntityFrameworkCore;

namespace Northwind.Data
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailsExtendedView> OrderDetailsExtends { get; set; }
        public DbSet<SalesOverviewByEmployeeView> SalesOverviewByEmployees { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Territory> Territories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetailsExtendedView>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("OrderDetailsExtended"); // exact view name
                eb.Property(e => e.OrderId).HasColumnName("OrderID");
                eb.Property(e => e.ProductId).HasColumnName("ProductID");
            });
            modelBuilder.Entity<SalesOverviewByEmployeeView>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("SalesPerson"); // exact view name
                eb.Property(e => e.OrderId).HasColumnName("OrderID");
            });
        }

    }

}
