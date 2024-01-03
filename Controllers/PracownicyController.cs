using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                // Tutaj możesz wykonać proste zapytanie, np. "SELECT 1"
                await connection.CloseAsync();
                Console.WriteLine("Połączenie z bazą danych: udane.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z bazą danych: {ex.Message}");
            }

            var pracownicy = await _context.Pracownicy.ToListAsync();
            return View(pracownicy);
        }
    }
}
