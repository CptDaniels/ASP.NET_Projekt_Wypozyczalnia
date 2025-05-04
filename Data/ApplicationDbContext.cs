using ASP.NET_Projekt_Wypozyczalnia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Projekt_Wypozyczalnia.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<RentalItems> RentalItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RentalItems>()
            .HasOne(p => p.Client)
            .WithMany(k => k.RentalItems)
            .HasForeignKey(p => p.ClientID);

        modelBuilder.Entity<RentalItems>()
            .HasOne(p => p.Car)
            .WithMany(s => s.RentalItems)
            .HasForeignKey(p => p.CarID);
    }
}

}
