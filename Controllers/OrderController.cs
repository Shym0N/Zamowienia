using Microsoft.AspNetCore.Mvc;
using Zamowienia.Data;
using Zamowienia.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zamowienia.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Zamowienia.Attributes;

public class OrderController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public OrderController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
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
        var user = await _userManager.GetUserAsync(User);
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
            dataZlozenia = DateTime.Now,
            listaPrzedmiotow = string.Join(", ", selectedProducts),
            
            czyZrealizowano = "NIE",
            dataRealizacji = DateTime.Now, 
            uwagi = orderForm.uwagi,
            UserName = user.UserName
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
