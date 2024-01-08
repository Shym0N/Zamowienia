using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using Zamowienia.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Zamowienia.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductIndexViewModel
            {
                Przedmioty = await _context.Przedmioty.ToListAsync(),
                NowyPrzedmiot = new Przedmiot()
            };
            return View(viewModel);
        }


        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var przedmiot = await _context.Przedmioty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaProduktu")] Przedmiot nowyPrzedmiot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nowyPrzedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeśli model nie jest prawidłowy, zwróć do widoku z pełnym modelem
            var viewModel = new ProductIndexViewModel
            {
                Przedmioty = await _context.Przedmioty.ToListAsync(),
                NowyPrzedmiot = nowyPrzedmiot
            };
            return View("Index", viewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var przedmiot = await _context.Przedmioty.FindAsync(id);
            if (przedmiot == null)
            {
                return NotFound();
            }
            return View(przedmiot);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaProduktu")] Przedmiot przedmiot)
        {
            if (id != przedmiot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotExists(przedmiot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(przedmiot);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var przedmiot = await _context.Przedmioty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przedmiot = await _context.Przedmioty.FindAsync(id);
            _context.Przedmioty.Remove(przedmiot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotExists(int id)
        {
            return _context.Przedmioty.Any(e => e.Id == id);
        }
    }
}
