using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class Przedmiot
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa Produktu")]
        [StringLength(35, ErrorMessage = "Maksymalna długość nazwy to 35 znaków.")]
        [UniqueProduct]
        public string? NazwaProduktu { get; set; }
    }
}
