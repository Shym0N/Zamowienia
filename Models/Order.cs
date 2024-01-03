using Microsoft.AspNetCore.Mvc;

namespace Zamowienia.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public string listaPrzedmiotow { get; set; }
        public int pracownik_id { get; set; }
        public char czyZrealizowano { get; set; }
    }
}
