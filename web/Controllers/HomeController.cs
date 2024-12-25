using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;



namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlagajnaContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

     public HomeController(BlagajnaContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

    public async Task<IActionResult> Index()
    {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var currentMonth = DateTime.Now.Month;
                var currentYear = DateTime.Now.Year;

                // Pridobimo transakcije trenutnega uporabnika za trenutni mesec
                var userTransactions = _context.Transactions
                                            .Where(t => t.User.Id == currentUser.Id &&
                                                        t.Date.Month == currentMonth &&
                                                        t.Date.Year == currentYear)
                                            .ToList();

                decimal totalAmount = userTransactions.Sum(t => t.Amount);

                // Pridobimo prihodke trenutnega uporabnika za trenutni mesec
                var userIncomes = _context.Incomes
                                        .Where(i => i.User.Id == currentUser.Id &&
                                                    i.Date.Month == currentMonth &&
                                                    i.Date.Year == currentYear)
                                        .ToList();

                decimal totalIncome = userIncomes.Sum(i => i.Amount);
                
                decimal saved = 0;
                // Izračun stanja in prihrankov
                decimal balance = totalIncome - totalAmount - saved;

                // Shranimo izračune v ViewData za prikaz v View
                ViewData["saved"] = saved;
                ViewData["balance"] = balance - saved;
                ViewData["TotalAmount"] = totalAmount;
            }
            else
            {
                ViewData["TotalAmount"] = 0;
                ViewData["balance"] = 0;
                ViewData["saved"] = 0;
            }
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
