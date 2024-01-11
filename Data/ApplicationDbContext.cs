using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Models;

namespace Zamowienia.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
             
        }

        public DbSet<Order>? Zamowienia { get; set; }
        public DbSet<Przedmiot>? Przedmioty { get; set; }

       protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Order>(o =>
            {
                o.Property(o => o.dataZlozenia)
                .HasDefaultValueSql("GETDATE()");
            });
            builder.Entity <Przedmiot>(p =>
            {
                p.HasIndex(p => p.NazwaProduktu)
                .IsUnique();
            });

        }


        // Usunięte: public DbSet<User> Pracownicy { get; set; }
    }
}
