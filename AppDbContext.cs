using polina.classes;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace polina;

public sealed class AppDbContext : DbContext
{
    public DbSet<user> users { get; set; }
    public DbSet<thing> things { get; set; }
    public DbSet<message> messages { get; set; }

    public AppDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Trusted_Connection=True;TrustServerCertificate=True;Database=polina;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<user>()
            .HasIndex(u => u.phone)
            .IsUnique();
        
        modelBuilder.Entity<thing>()
            .HasIndex(c => c.name)
            .IsUnique();
    }
}