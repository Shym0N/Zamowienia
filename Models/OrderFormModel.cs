using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class OrderFormModel
    {
        // Przykład atrybutu walidacji
        [Required(ErrorMessage = "Pole uwagi jest wymagane.")]
        public string? uwagi { get; set; }

        public List<PrzedmiotViewModel> Produkty { get; set; }
    }

}
