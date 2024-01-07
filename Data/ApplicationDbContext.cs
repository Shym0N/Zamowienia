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

        // Usunięte: public DbSet<User> Pracownicy { get; set; }
    }
}
