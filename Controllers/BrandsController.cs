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
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Brands.Include(b => b.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebAddress,CompanyId")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", brand.CompanyId);
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", brand.CompanyId);
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebAddress,CompanyId")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", brand.CompanyId);
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .Include(b => b.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Brands'  is null.");
            }
            bool status = DConfirm(id);
            if (status)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                throw new Exception("İşlem gerçekleştirilemedi.");
        }

        private bool BrandExists(int id)
        {
          return (_context.Brands?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool DConfirm(int id)
        {
            try
            {
                var brand = _context.Brands.Where(c => c.Id == id).Include(c => c.Restaurants).FirstOrDefault();
                if (brand != null)
                {
                    brand.Status.StatusId = 0;
                    _context.Brands.Update(brand);
                    IQueryable<Restaurant> restaurants = _context.Restaurants.Where(r => r.BrandId == id);
                    //RestaurantsController restaurantsController = new RestaurantsController(_context);
                    foreach (Restaurant restaurant in restaurants)
                    {
                        //restaurantsController.DeleteRestaurant(restaurant.Id);
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
                    }
                    IQueryable<BrandUser> users = _context.BrandUsers.Where(u => u.BrandId == id);
                    foreach (BrandUser user in users)
                    {
                        user.Status.StatusId = 0;
                        _context.BrandUsers.Update(user);
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
    
    }
}
