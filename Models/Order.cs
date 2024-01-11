using Microsoft.AspNetCore.Mvc;

namespace Zamowienia.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime? dataZlozenia { get; set; }
        public string listaPrzedmiotow { get; set; }
        public int pracownikId { get; set; }
        public string UserName { get; set; }
        public string czyZrealizowano { get; set; }
        public DateTime? dataRealizacji { get; set; }
        public string? uwagi { get; set; }
    }
}
