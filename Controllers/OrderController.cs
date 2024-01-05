using Microsoft.AspNetCore.Mvc;
using Zamowienia.Data;
using Zamowienia.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        if (_context.Przedmioty == null)
        {
            return View("Error", "Błąd: Brak dostępu do Przedmiotów w bazie danych.");
        }

        var model = new OrderFormModel
        {
            Produkty = _context.Przedmioty.Select(p => new PrzedmiotViewModel
            {
                Id = p.Id,
                NazwaProduktu = p.NazwaProduktu,
                IsSelected = false
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderFormModel orderForm)
    {
        if (!ModelState.IsValid)
        {
            var selectedProductIds = orderForm.Produkty
                .Where(p => p.IsSelected)
                .Select(p => p.Id)
                .ToList();

            if (!selectedProductIds.Any())
            {
                ModelState.AddModelError("", "Nie wybrano żadnych produktów.");
                return View(orderForm);
            }

            if (_context.Przedmioty == null)
            {
                return View("Error", "Błąd: Brak dostępu do Przedmiotów w bazie danych.");
            }

            var selectedProducts = _context.Przedmioty
                .Where(p => selectedProductIds.Contains(p.Id))
                .ToList();

            var zamowienie = new Order
            {
                dataZlozenia = DateTime.Now,
                listaPrzedmiotow = string.Join(", ", selectedProducts.Select(p => p.NazwaProduktu)),
                pracownikId = 1,
                czyZrealizowano = "NIE",
                dataRealizacji = DateTime.Now,
                uwagi = orderForm.uwagi
            };

            if (_context.Zamowienia == null)
            {
                return View("Error", "Błąd: Brak dostępu do Zamówień w bazie danych.");
            }

            _context.Zamowienia.Add(zamowienie);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderSuccess");
        }

        // Ponowne ładowanie produktów w przypadku błędu walidacji
        if (_context.Przedmioty != null)
        {
            var produktyZBazy = await _context.Przedmioty.ToListAsync();
            orderForm.Produkty = produktyZBazy.Select(p => new PrzedmiotViewModel
            {
                Id = p.Id,
                NazwaProduktu = p.NazwaProduktu,
                IsSelected = orderForm.Produkty.Any(x => x.Id == p.Id && x.IsSelected)
            }).ToList();
        }

        return View(orderForm);
    }

    [HttpGet]
    public IActionResult OrderSuccess()
    {
        return View();
    }
}
