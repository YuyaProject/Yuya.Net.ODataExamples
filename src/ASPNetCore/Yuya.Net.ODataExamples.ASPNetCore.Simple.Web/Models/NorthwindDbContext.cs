using Microsoft.EntityFrameworkCore;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models
{
  public class NorthwindDbContext : DbContext
  {
    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Territory> Territories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<EmployeeTerritory>().HasKey(x => new { x.EmployeeID, x.TerritoryID });

      modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.OrderID, x.ProductID });
    }
  }
}
