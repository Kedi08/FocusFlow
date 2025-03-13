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
    public class ProjektphasenVerwaltungController : Controller
    {
        private readonly AppDbContext _context;

        public ProjektphasenVerwaltungController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjektphasenVerwaltung
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Projektphasen.Include(p => p.Projekt).Include(p => p.Vorgehensmodell);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProjektphasenVerwaltung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projektphasen == null)
            {
                return NotFound();
            }

            var projektphase = await _context.Projektphasen
                .Include(p => p.Projekt)
                .Include(p => p.Vorgehensmodell)
                .FirstOrDefaultAsync(m => m.ProjektphaseId == id);
            if (projektphase == null)
            {
                return NotFound();
            }

            return View(projektphase);
        }

        // GET: ProjektphasenVerwaltung/Create
        public IActionResult Create()
        {
            ViewBag.VorgehensmodellId = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Bitte wählen...", Selected = true }
            }
            .Concat(_context.Vorgehensmodelle
                .Select(v => new SelectListItem { Value = v.VorgehensmodellId.ToString(), Text = v.Name }))
            .ToList();

            return View();
        }

        // POST: ProjektphasenVerwaltung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjektphaseId,ProjektphaseName,DefinierteZeitspanne,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,ReviewdatumGeplant,ReviewdatumEffektiv,Freigabedatum,Freigabevermerk,Status,Fortschritt,ProjektId,VorgehensmodellId")] Projektphase projektphase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projektphase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjektId"] = new SelectList(_context.Projekte, "ProjektId", "ProjektId", projektphase.ProjektId);
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projektphase.VorgehensmodellId);
            return View(projektphase);
        }

        // GET: ProjektphasenVerwaltung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projektphasen == null)
            {
                return NotFound();
            }

            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase == null)
            {
                return NotFound();
            }
            ViewData["ProjektId"] = new SelectList(_context.Projekte, "ProjektId", "ProjektId", projektphase.ProjektId);
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projektphase.VorgehensmodellId);
            return View(projektphase);
        }

        // POST: ProjektphasenVerwaltung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektphaseId,ProjektphaseName,DefinierteZeitspanne,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,ReviewdatumGeplant,ReviewdatumEffektiv,Freigabedatum,Freigabevermerk,Status,Fortschritt,ProjektId,VorgehensmodellId")] Projektphase projektphase)
        {
            if (id != projektphase.ProjektphaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projektphase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektphaseExists(projektphase.ProjektphaseId))
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
            ViewData["ProjektId"] = new SelectList(_context.Projekte, "ProjektId", "ProjektId", projektphase.ProjektId);
            ViewData["VorgehensmodellId"] = new SelectList(_context.Vorgehensmodelle, "VorgehensmodellId", "VorgehensmodellId", projektphase.VorgehensmodellId);
            return View(projektphase);
        }

        // GET: ProjektphasenVerwaltung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projektphasen == null)
            {
                return NotFound();
            }

            var projektphase = await _context.Projektphasen
                .Include(p => p.Projekt)
                .Include(p => p.Vorgehensmodell)
                .FirstOrDefaultAsync(m => m.ProjektphaseId == id);
            if (projektphase == null)
            {
                return NotFound();
            }

            return View(projektphase);
        }

        // POST: ProjektphasenVerwaltung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projektphasen == null)
            {
                return Problem("Entity set 'AppDbContext.Projektphasen'  is null.");
            }
            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase != null)
            {
                _context.Projektphasen.Remove(projektphase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektphaseExists(int id)
        {
          return (_context.Projektphasen?.Any(e => e.ProjektphaseId == id)).GetValueOrDefault();
        }
    }
}
