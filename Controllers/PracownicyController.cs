/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using Zamowienia.Models;
using System.Threading.Tasks;

namespace Zamowienia.Controllers
{
    public class PracownicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracownicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var pracownicy = await _context.Pracownicy.ToListAsync();
            return View("index");
        }
    }
}
*/