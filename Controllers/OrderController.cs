using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using Zamowienia.Models;
using System.Threading.Tasks;

namespace Zamowienia.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Zamowienia.ToListAsync();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Zamowienia.FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: OrderController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,ListaPrzedmiotow,PracownikId,CzyZrealizowano")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrderController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Zamowienia.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,ListaPrzedmiotow,PracownikId,CzyZrealizowano")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
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
            return View(order);
        }

        // GET: OrderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Zamowienia.FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Zamowienia.FindAsync(id);
            _context.Zamowienia.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Zamowienia.Any(e => e.id == id);
        }
    }
}
