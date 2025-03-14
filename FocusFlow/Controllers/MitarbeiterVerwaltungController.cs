using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FocusFlow.Data;
using FocusFlow.Models;

namespace FocusFlow.Controllers
{
    public class MitarbeiterVerwaltungController : Controller
    {
        private readonly AppDbContext _context;

        public MitarbeiterVerwaltungController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MitarbeiterVerwaltung
        public async Task<IActionResult> Index()
        {
              return _context.Mitarbeiter != null ? 
                          View(await _context.Mitarbeiter.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Mitarbeiter'  is null.");
        }

        // GET: MitarbeiterVerwaltung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mitarbeiter == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter
                .FirstOrDefaultAsync(m => m.MitarbeiterId == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // GET: MitarbeiterVerwaltung/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: MitarbeiterVerwaltung/Create
        public async Task<IActionResult> Create(Mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mitarbeiter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mitarbeiter);
        }

        // GET: MitarbeiterVerwaltung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mitarbeiter == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }
            return View(mitarbeiter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mitarbeiter mitarbeiter)
        {
            if (id != mitarbeiter.MitarbeiterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mitarbeiter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MitarbeiterExists(mitarbeiter.MitarbeiterId))
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
            return View(mitarbeiter);
        }

        // GET: MitarbeiterVerwaltung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mitarbeiter == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter
                .FirstOrDefaultAsync(m => m.MitarbeiterId == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // POST: MitarbeiterVerwaltung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mitarbeiter == null)
            {
                return Problem("Entity set 'AppDbContext.Mitarbeiter'  is null.");
            }
            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter != null)
            {
                _context.Mitarbeiter.Remove(mitarbeiter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MitarbeiterExists(int id)
        {
          return (_context.Mitarbeiter?.Any(e => e.MitarbeiterId == id)).GetValueOrDefault();
        }
    }
}
