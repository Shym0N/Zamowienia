using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class OrderFormModel
    {
       
        //[Required(ErrorMessage = "Pole uwagi jest wymagane.")] //odkomentować jeśli uwagmi mają byc wymagane
        public string? uwagi { get; set; }

        public List<PrzedmiotViewModel> Produkty { get; set; }
    }

}
