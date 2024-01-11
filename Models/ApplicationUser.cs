using Microsoft.AspNetCore.Identity;

namespace Zamowienia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? TypUzytkownika { get; set; }

    }
}
