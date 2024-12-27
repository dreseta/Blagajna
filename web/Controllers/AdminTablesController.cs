using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using web.Data;

namespace web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminTablesController : Controller
    {
        private readonly BlagajnaContext _context;

        public AdminTablesController(BlagajnaContext context)
        {
            _context = context;
        }

        public IActionResult Index(string tableName)
        {
            ViewBag.Tables = new List<string>
            {
                "Transactions",
                "Categories",
                "ApplicationUsers"
            };

            if (string.IsNullOrEmpty(tableName))
            {
                return View("SelectTable"); // Pokaže stran za izbiro tabele.
            }

            dynamic data;
            switch (tableName)
            {
                case "Transactions":
                    data = _context.Transactions.Include(t => t.Category).Include(t => t.User).ToList();
                    break;
                case "Categories":
                    data = _context.Categories.ToList();
                    break;
                case "ApplicationUsers":
                    data = _context.Users.ToList();
                    break;
                default:
                    return NotFound();
            }

            ViewBag.TableName = tableName;
            return View("TableView", data); // Prikaz izbrane tabele.
        }
    }
}
