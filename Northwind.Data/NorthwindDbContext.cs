using Microsoft.EntityFrameworkCore;

namespace Northwind.Data
{
    public class NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<SalesReportData> SalesReportData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetailsExtendedView>(eb =>
            //{
            //    eb.HasNoKey();
            //    eb.ToView("OrderDetailsExtended"); // exact view name
            //    eb.Property(e => e.OrderId).HasColumnName("OrderID");
            //    eb.Property(e => e.ProductId).HasColumnName("ProductID");
            //});
            modelBuilder.Entity<SalesReportData>(e =>
            {
                e.HasNoKey();
                e.Property(eb => eb.OrderId).HasColumnName("OrderId");
                e.Property(eb => eb.ProductId).HasColumnName("ProductId");
                e.Property(eb => eb.EmployeeId).HasColumnName("EmployeeId");
                e.Property(eb => eb.CustomerId).HasColumnName("CustomerId");
                e.Property(eb => eb.ShipperId).HasColumnName("ShipperId");
                e.Property(eb => eb.SupplierId).HasColumnName("SupplierId");
                e.Property(eb => eb.CategoryId).HasColumnName("CategoryId");
                e.Property(eb => eb.EmployeeTerritoryId).HasColumnName("EmployeeTerritoryId");
                e.Property(eb => eb.RegionId).HasColumnName("RegionId");
                e.Property(eb => eb.OrderAt).HasColumnName("OrderAt");
                e.Property(eb => eb.OrderAtId).HasColumnName("OrderAtId");
                e.Property(eb => eb.OrderYear).HasColumnName("OrderYear");
                e.Property(eb => eb.OrderMonth).HasColumnName("OrderMonth");
                e.Property(eb => eb.OrderQuarter).HasColumnName("OrderQuarter");
                e.Property(eb => eb.OrderDay).HasColumnName("OrderDay");
                e.Property(eb => eb.RequiredAt).HasColumnName("RequiredAt");
                e.Property(eb => eb.RequiredAtId).HasColumnName("RequiredAtId");
                e.Property(eb => eb.RequiredYear).HasColumnName("RequiredYear");
                e.Property(eb => eb.RequiredMonth).HasColumnName("RequiredMonth");
                e.Property(eb => eb.RequiredQuarter).HasColumnName("RequiredQuarter");
                e.Property(eb => eb.RequiredDay).HasColumnName("RequiredDay");
                e.Property(eb => eb.ShippedAt).HasColumnName("ShippedAt");
                e.Property(eb => eb.ShippedAtId).HasColumnName("ShippedAtId");
                e.Property(eb => eb.ShippedYear).HasColumnName("ShippedYear");
                e.Property(eb => eb.ShippedMonth).HasColumnName("ShippedMonth");
                e.Property(eb => eb.ShippedQuarter).HasColumnName("ShippedQuarter");
                e.Property(eb => eb.ShippedDay).HasColumnName("ShippedDay");
                e.Property(eb => eb.UnitPrice).HasColumnName("UnitPrice");
                e.Property(eb => eb.Quantity).HasColumnName("Quantity");
                e.Property(eb => eb.Discount).HasColumnName("Discount");
                e.Property(eb => eb.Country).HasColumnName("Country");
                e.Property(eb => eb.Region).HasColumnName("Region");
                e.Property(eb => eb.City).HasColumnName("City");
                e.Property(eb => eb.ZipCode).HasColumnName("ZipCode");
                e.ToView("vwSalesReport");
            });
        }
    }
}
