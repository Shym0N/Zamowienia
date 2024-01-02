using Microsoft.AspNetCore.Mvc;

namespace Zamowienia.Models
{
    public class Order : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
