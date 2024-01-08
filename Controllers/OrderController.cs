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
            return View(orderForm);
        }

        var selectedProducts = orderForm.Produkty
            .Where(p => p.IsSelected)
            .Select(p => p.NazwaProduktu)
            .ToList();

        if (!selectedProducts.Any())
        {
            ModelState.AddModelError("", "Nie wybrano żadnych produktów.");
            return View(orderForm);
        }

        // Tworzenie nowego zamówienia
        var zamowienie = new Order
        {    
            listaPrzedmiotow = string.Join(", ", selectedProducts),
            pracownikId = 1, // przykładowy ID pracownika
            czyZrealizowano = "NIE",
            dataRealizacji = DateTime.Now,
            uwagi = orderForm.uwagi
        };

        _context.Zamowienia.Add(zamowienie);
        await _context.SaveChangesAsync();

        return RedirectToAction("OrderSuccess");
    }


    [HttpGet]
    public IActionResult OrderSuccess()
    {
        return View();
    }
}
