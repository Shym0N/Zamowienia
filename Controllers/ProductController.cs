using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using Zamowienia.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Zamowienia.Attributes;

namespace Zamowienia.Controllers
{
    [CustomAuthorize("Administrator")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductIndexViewModel
            {
                Przedmioty = await _context.Przedmioty.ToListAsync(),
                NowyPrzedmiot = new Przedmiot()
            };
            return View(viewModel);
        }



        public IActionResult Create()
        {
            return View();
        }


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

            var viewModel = new ProductIndexViewModel
            {
                Przedmioty = await _context.Przedmioty.ToListAsync(),
                NowyPrzedmiot = nowyPrzedmiot
            };
            return View("Index", viewModel);
        }


    


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

