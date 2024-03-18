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
    public class BrandUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BrandUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BrandUsers.Include(b => b.Brand);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BrandUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrandUsers == null)
            {
                return NotFound();
            }

            var brandUser = await _context.BrandUsers
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandUser == null)
            {
                return NotFound();
            }

            return View(brandUser);
        }

        // GET: BrandUsers/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            return View();
        }

        // POST: BrandUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Name,SurName,Email,PhoneNumber,RegisterDate,BrandId")] BrandUser brandUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brandUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", brandUser.BrandId);
            return View(brandUser);
        }

        // GET: BrandUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrandUsers == null)
            {
                return NotFound();
            }

            var brandUser = await _context.BrandUsers.FindAsync(id);
            if (brandUser == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", brandUser.BrandId);
            return View(brandUser);
        }

        // POST: BrandUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Name,SurName,Email,PhoneNumber,RegisterDate,BrandId")] BrandUser brandUser)
        {
            if (id != brandUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandUserExists(brandUser.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", brandUser.BrandId);
            return View(brandUser);
        }

        // GET: BrandUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrandUsers == null)
            {
                return NotFound();
            }

            var brandUser = await _context.BrandUsers
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandUser == null)
            {
                return NotFound();
            }

            return View(brandUser);
        }

        // POST: BrandUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrandUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BrandUsers'  is null.");
            }
            var brandUser = await _context.BrandUsers.FindAsync(id);
            if (brandUser != null)
            {
                _context.BrandUsers.Remove(brandUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandUserExists(int id)
        {
          return (_context.BrandUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
