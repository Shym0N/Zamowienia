using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Models;

namespace Zamowienia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Pracownik> Pracownicy { get; set; }
        //public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Order> Zamowienia { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
    }
}