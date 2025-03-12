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
    public class ProjektsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjektsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projekts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Projekte.Include(p => p.Vorgehensmodell);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Projekts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte
                .Include(p => p.Vorgehensmodell)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // GET: Projekts/Create
        public IActionResult Create()
        {
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId");
            return View();
        }

        // POST: Projekts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjektId,Projektreferenz,Titel,Beschreibung,Bewilligungsdatum,Prioritaet,Status,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,Fortschritt,VorgehensmodellId")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projekt.VorgehensmodellId);
            return View(projekt);
        }

        // GET: Projekts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projekt.VorgehensmodellId);
            return View(projekt);
        }

        // POST: Projekts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektId,Projektreferenz,Titel,Beschreibung,Bewilligungsdatum,Prioritaet,Status,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,Fortschritt,VorgehensmodellId")] Projekt projekt)
        {
            if (id != projekt.ProjektId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektExists(projekt.ProjektId))
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
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projekt.VorgehensmodellId);
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte
                .Include(p => p.Vorgehensmodell)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projekte == null)
            {
                return Problem("Entity set 'AppDbContext.Projekte'  is null.");
            }
            var projekt = await _context.Projekte.FindAsync(id);
            if (projekt != null)
            {
                _context.Projekte.Remove(projekt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
          return (_context.Projekte?.Any(e => e.ProjektId == id)).GetValueOrDefault();
        }
    }
}
