using ASP.NET_Projekt_Wypozyczalnia.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Projekt_Wypozyczalnia.Data
{
    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Samochod> Samochody { get; set; }
    public DbSet<Klient> Klienci { get; set; }
    public DbSet<PozycjaWypozyczenia> PozycjeWypozyczenia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PozycjaWypozyczenia>()
            .HasOne(p => p.Klient)
            .WithMany(k => k.PozycjeWypozyczenia)
            .HasForeignKey(p => p.IDKlienta);

        modelBuilder.Entity<PozycjaWypozyczenia>()
            .HasOne(p => p.Samochod)
            .WithMany(s => s.PozycjeWypozyczenia)
            .HasForeignKey(p => p.IDSamochodu);
    }
}

}
