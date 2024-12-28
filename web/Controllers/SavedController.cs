using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using web.Data;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class SavedController : Controller
    {
        private readonly BlagajnaContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public SavedController(BlagajnaContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Saved
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var savedMoney = _context.SavedMoney.Where(s => s.User == currentUser); // Filter by user
            return View(await savedMoney.ToListAsync());
        }

        // GET: Saved/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saved = await _context.SavedMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saved == null)
            {
                return NotFound();
            }

            return View(saved);
        }

        // GET: Saved/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saved/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date")] Saved saved)
        {
             var currentUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                // Associate the saved entity with the current user
                saved.Date = DateTime.Now; // Nastavite trenutni datum

                saved.User = currentUser;

                _context.Add(saved);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saved); 
        }

        // GET: Saved/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saved = await _context.SavedMoney.FindAsync(id);
            if (saved == null)
            {
                return NotFound();
            }
            return View(saved);
        }

        // POST: Saved/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date")] Saved saved)
        {
            if (id != saved.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedExists(saved.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saved);
        }

        // GET: Saved/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saved = await _context.SavedMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saved == null)
            {
                return NotFound();
            }

            return View(saved);
        }

        // POST: Saved/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saved = await _context.SavedMoney.FindAsync(id);
            if (saved != null)
            {
                _context.SavedMoney.Remove(saved);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedExists(int id)
        {
            return _context.SavedMoney.Any(e => e.Id == id);
        }
    }
}
