using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu_TabGida.Data;
using QRMenu_TabGida.Models;

namespace QRMenu_TabGida.Controllers
{
    public class RestaurantUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RestaurantUsers.Include(r => r.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RestaurantUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RestaurantUsers == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // GET: RestaurantUsers/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: RestaurantUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Name,SurName,Email,PhoneNumber,RegisterDate,RestaurantId")] RestaurantUser restaurantUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurantUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RestaurantUsers == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers.FindAsync(id);
            if (restaurantUser == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // POST: RestaurantUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Name,SurName,Email,PhoneNumber,RegisterDate,RestaurantId")] RestaurantUser restaurantUser)
        {
            if (id != restaurantUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantUserExists(restaurantUser.Id))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RestaurantUsers == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // POST: RestaurantUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RestaurantUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RestaurantUsers'  is null.");
            }
            var restaurantUser = await _context.RestaurantUsers.FindAsync(id);
            if (restaurantUser != null)
            {
                _context.RestaurantUsers.Remove(restaurantUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantUserExists(int id)
        {
          return (_context.RestaurantUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
