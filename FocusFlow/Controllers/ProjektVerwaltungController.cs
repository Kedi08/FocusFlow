﻿using System;
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
    public class ProjektVerwaltungController : Controller
    {
        private readonly AppDbContext _context;

        public ProjektVerwaltungController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjektVerwaltung
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Projekte.Include(p => p.Projektleiter);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProjektVerwaltung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte
                .Include(p => p.Projektleiter)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // GET: ProjektVerwaltung/Create
        public IActionResult Create()
        {
            // Dropdown für Projektleiter
            ViewBag.ProjektleiterId = new SelectList(
                _context.Mitarbeiter
                    .Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }),
                "MitarbeiterId", "Name");

            // Dropdown für Vorgehensmodell-Vorlagen
            ViewBag.VorlageVorgehensmodellId = new SelectList(
                _context.Vorgehensmodelle.Where(vm => vm.IstVorlage),
                "VorgehensmodellId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projekt projekt, int VorlageVorgehensmodellId)
        {
            if (ModelState.IsValid)
            {
                // 1) Vorlage laden
                var original = await _context.Vorgehensmodelle
                    .Include(vm => vm.Projektphasen)
                    .FirstOrDefaultAsync(vm => vm.VorgehensmodellId == VorlageVorgehensmodellId);

                if (original == null)
                {
                    // Falls nichts gefunden
                    ModelState.AddModelError("", "Vorgehensmodell nicht gefunden.");
                    return View(projekt);
                }

                // 2) Neue Kopie erstellen
                var kopie = new Vorgehensmodell
                {
                    Name = original.Name + " (Kopie)",
                    IstVorlage = false,
                    Projektphasen = new List<Projektphase>()
                };

                // 3) Phasen kopieren
                foreach (var phase in original.Projektphasen)
                {
                    kopie.Projektphasen.Add(new Projektphase
                    {
                        ProjektphaseName = phase.ProjektphaseName,
                        DauerInTagen = phase.DauerInTagen
                        // etc.
                    });
                }

                // 4) Kopie dem Projekt zuweisen
                projekt.Vorgehensmodell = kopie;

                // 5) Projekt an DB anhängen und speichern
                _context.Add(projekt);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // 6️⃣ Falls Fehler auftreten, DropDown-Menüs erneut befüllen
            ViewData["VorlageVorgehensmodellId"] = new SelectList(
                _context.Vorgehensmodelle.Where(vm => vm.IstVorlage), "VorgehensmodellId", "Name");

            ViewData["ProjektleiterId"] = new SelectList(
                _context.Mitarbeiter.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }),
                "MitarbeiterId", "Name");

            return View(projekt);
        }

        // GET: ProjektVerwaltung/Edit/5
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
            ViewData["ProjektleiterId"] = new SelectList(_context.Mitarbeiter, "MitarbeiterId", "MitarbeiterId", projekt.ProjektleiterId);
            return View(projekt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projekt projekt)
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
            ViewData["ProjektleiterId"] = new SelectList(_context.Mitarbeiter, "MitarbeiterId", "MitarbeiterId", projekt.ProjektleiterId);
            return View(projekt);
        }

        // GET: ProjektVerwaltung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte
                .Include(p => p.Projektleiter)
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // POST: ProjektVerwaltung/Delete/5
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
