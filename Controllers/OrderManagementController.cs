using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Data;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Zamowienia.Attributes;


[CustomAuthorize("Administrator")]
public class OrderManagementController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderManagementController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var zamowienia = await _context.Zamowienia
            .Where(z => z.listaPrzedmiotow != null) 
            .ToListAsync();

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

        zamowienie.czyZrealizowano = "TAK";
        zamowienie.dataRealizacji = DateTime.Now;
        _context.Update(zamowienie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CofnijRealizacje(int id)
    {
        var zamowienie = await _context.Zamowienia.FindAsync(id);
        if (zamowienie == null)
        {
            return NotFound();
        }

        zamowienie.czyZrealizowano = "NIE";
        zamowienie.dataRealizacji = DateTime.Now;
        _context.Update(zamowienie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

 
    public  IActionResult PobierzPlikTekstowy(int id)
    {
        var zamowienie = _context.Zamowienia.FirstOrDefault(o => o.id == id);
        if (zamowienie == null)
        {
            return NotFound();
        }

        var trescPliku = "- "+zamowienie.listaPrzedmiotow.Replace(",","\n-");

        var plikBytes = Encoding.UTF8.GetBytes(trescPliku);
        var nazwaPliku = $"Zamowienia_{id}.txt";

        return File(plikBytes, "text/plain", nazwaPliku);
    }


}

