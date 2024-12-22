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
                var userTransactions = _context.Transactions.Where(t => t.User.Id == currentUser.Id).ToList();

                int totalTransactions = userTransactions.Count();
                decimal totalAmount = userTransactions.Sum(t => t.Amount);

                decimal balance = 1800 - totalAmount;
                int saved = totalTransactions * 5;

                ViewData["saved"] = saved;
                ViewData["balance"] = balance - saved;
                ViewData["TotalAmount"] = totalAmount;
                ViewData["TotalTransactions"] = totalTransactions;
            }
            else
            {
                ViewData["TotalTransactions"] = 0;
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
