using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClaimTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClaimTypes.ToListAsync());
        }

        // GET: ClaimTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimType = await _context.ClaimTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimType == null)
            {
                return NotFound();
            }

            return View(claimType);
        }

        // GET: ClaimTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClaimTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimType claimType)
        {
            claimType.CreatedById = "Kamogelo";
            claimType.CreatedOn = DateTime.Now;
            claimType.ModifiedById = "Kamogelo";
            claimType.ModifiedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(claimType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claimType);
        }

        // GET: ClaimTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimType = await _context.ClaimTypes.FindAsync(id);
            if (claimType == null)
            {
                return NotFound();
            }
            return View(claimType);
        }

        // POST: ClaimTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] ClaimType claimType)
        {
            if (id != claimType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimTypeExists(claimType.Id))
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
            return View(claimType);
        }

        // GET: ClaimTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimType = await _context.ClaimTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimType == null)
            {
                return NotFound();
            }

            return View(claimType);
        }

        // POST: ClaimTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimType = await _context.ClaimTypes.FindAsync(id);
            if (claimType != null)
            {
                _context.ClaimTypes.Remove(claimType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimTypeExists(int id)
        {
            return _context.ClaimTypes.Any(e => e.Id == id);
        }
    }
}
