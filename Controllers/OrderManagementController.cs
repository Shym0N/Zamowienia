using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using System.Threading.Tasks;

public class OrderManagementController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderManagementController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var zamowienia = await _context.Zamowienia.ToListAsync();
        return View(zamowienia);
    }
    [HttpPost]
    public async Task<IActionResult> RealizujZamowienie(int id)
    {
        var zamowienie = await _context.Zamowienia.FindAsync(id);
        if (zamowienie == null)
        {
            return NotFound();
        }

        zamowienie.czyZrealizowano = 'T';
        _context.Update(zamowienie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
