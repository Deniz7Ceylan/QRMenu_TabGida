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
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Restaurants.Include(r => r.Brand);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,District,Address,PostalCode,Phone,EMail,RegisterDate,BrandId")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", restaurant.BrandId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", restaurant.BrandId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,District,Address,PostalCode,Phone,EMail,RegisterDate,BrandId")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", restaurant.BrandId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurants'  is null.");
            }
            bool status = DConfirm(id);
            if (status)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                throw new Exception("İşlem gerçekleştirilemedi.");
        }

        private bool DConfirm(int id)
        {
            try
            {
                var restaurant = _context.Restaurants.Where(c => c.Id == id).Include(c => c.Categories).FirstOrDefault();
                if (restaurant != null)
                {
                    restaurant.Status.StatusId = 0;
                    _context.Restaurants.Update(restaurant);

                    IQueryable<Category> categories = _context.Categories.Where(c => c.RestaurantId == restaurant.Id);
                    foreach (Category category in categories)
                    {
                        category.Status.StatusId = 0;
                        _context.Categories.Update(category);
                        IQueryable<Food> foods = _context.Foods.Where(f => f.CategoryId == category.Id);
                        foreach (Food food in foods)
                        {
                            food.Status.StatusId = 0;
                            _context.Foods.Update(food);
                        }
                    }
                    IQueryable<RestaurantUser> rUsers = _context.RestaurantUsers.Where(u => u.RestaurantId == id);
                    foreach (RestaurantUser user in rUsers)
                    {
                        user.Status.StatusId = 0;
                        _context.RestaurantUsers.Update(user);
                    }
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
