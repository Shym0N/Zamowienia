using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class Przedmiot
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa Produktu")]
        [UniqueProduct]
        public string? NazwaProduktu { get; set; }
    }
}
