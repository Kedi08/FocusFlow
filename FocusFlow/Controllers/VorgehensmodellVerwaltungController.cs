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
    public class VorgehensmodellVerwaltungController : Controller
    {
        private readonly AppDbContext _context;

        public VorgehensmodellVerwaltungController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VorgehensmodellVerwaltung
        public async Task<IActionResult> Index()
        {
            //nur vorlagen sind sichtbar!
              return _context.Vorgehensmodelle != null ? 
                          View(await _context.Vorgehensmodelle.Where(vm => vm.IstVorlage).ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Vorgehensmodelle'  is null.");
        }

        // GET: VorgehensmodellVerwaltung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vorgehensmodelle == null)
            {
                return NotFound();
            }

            var vorgehensmodell = await _context.Vorgehensmodelle
                .FirstOrDefaultAsync(m => m.VorgehensmodellId == id);
            if (vorgehensmodell == null)
            {
                return NotFound();
            }

            return View(vorgehensmodell);
        }

        // GET: VorgehensmodellVerwaltung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VorgehensmodellVerwaltung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vorgehensmodell vorgehensmodell)
        {
            vorgehensmodell.IstVorlage = true;
            if (ModelState.IsValid)
            {
                _context.Add(vorgehensmodell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vorgehensmodell);
        }

        // GET: VorgehensmodellVerwaltung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vorgehensmodelle == null)
            {
                return NotFound();
            }

            var vorgehensmodell = await _context.Vorgehensmodelle.FindAsync(id);
            if (vorgehensmodell == null)
            {
                return NotFound();
            }
            return View(vorgehensmodell);
        }

        // POST: VorgehensmodellVerwaltung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vorgehensmodell vorgehensmodell)
        {
            if (id != vorgehensmodell.VorgehensmodellId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vorgehensmodell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VorgehensmodellExists(vorgehensmodell.VorgehensmodellId))
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
            return View(vorgehensmodell);
        }

        // GET: VorgehensmodellVerwaltung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vorgehensmodelle == null)
            {
                return NotFound();
            }

            var vorgehensmodell = await _context.Vorgehensmodelle
                .FirstOrDefaultAsync(m => m.VorgehensmodellId == id);
            if (vorgehensmodell == null)
            {
                return NotFound();
            }

            return View(vorgehensmodell);
        }

        // POST: VorgehensmodellVerwaltung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vorgehensmodelle == null)
            {
                return Problem("Entity set 'AppDbContext.Vorgehensmodelle'  is null.");
            }
            var vorgehensmodell = await _context.Vorgehensmodelle.FindAsync(id);
            if (vorgehensmodell != null)
            {
                _context.Vorgehensmodelle.Remove(vorgehensmodell);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VorgehensmodellExists(int id)
        {
          return (_context.Vorgehensmodelle?.Any(e => e.VorgehensmodellId == id)).GetValueOrDefault();
        }
    }
}
