using Microsoft.AspNetCore.Mvc;

namespace Zamowienia.Models
{
    public class Product : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
