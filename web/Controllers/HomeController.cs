using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Data;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlagajnaContext _context;

    public HomeController(BlagajnaContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        decimal totalAmount = _context.Transactions.Sum(t => t.Amount);
        decimal balance = 1800 - totalAmount;
        ViewData["balance"] = balance;
        ViewData["TotalAmount"] = totalAmount;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
