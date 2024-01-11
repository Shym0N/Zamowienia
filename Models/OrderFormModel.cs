using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class OrderFormModel
    {
       
        //[Required(ErrorMessage = "Pole uwagi jest wymagane.")]
        public string? uwagi { get; set; }

        public List<PrzedmiotViewModel> Produkty { get; set; }
    }

}
