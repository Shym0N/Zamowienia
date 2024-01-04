using Microsoft.AspNetCore.Mvc;
using Zamowienia.Data;
using Zamowienia.Models;
using System;
using System.Threading.Tasks;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        var model = new OrderFormModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderFormModel orderForm)
    {
        if (ModelState.IsValid)
        {
            var zamowienie = new Order
            {
                data = DateTime.Now,
                listaPrzedmiotow = orderForm.ListaPrzedmiotow,
                pracownik_id = 1,
                czyZrealizowano = 'N'
            };

            _context.Zamowienia.Add(zamowienie);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderSuccess");
        }

        return View(orderForm);
    }

    public IActionResult OrderSuccess()
    {
        return View();
    }
}
