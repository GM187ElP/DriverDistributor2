using BlazorApp1.Entities;
using Microsoft.EntityFrameworkCore;
using Route = BlazorApp1.Entities.Route;

namespace BlazorApp1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Distributor> Distributors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<ShipmentNumber> ShipmentNumbers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users", "Identity");
        modelBuilder.Entity<Role>().ToTable("Roles", "Identity");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles", "Identity");
        modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "Identity");
        modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", "Identity");

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
    }
}


