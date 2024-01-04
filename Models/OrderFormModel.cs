using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class OrderFormModel
    {
        [Required]
        [Display(Name = "Lista Przedmiotów")]
        public string ListaPrzedmiotow { get; set; }
    }
}
