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
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Dokumente)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Meilensteine)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Aktivitaeten)
                            .ThenInclude(a => a.Ressourcen)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Aktivitaeten)
                            .ThenInclude(a => a.ExterneKosten)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Aktivitaeten)
                            .ThenInclude(a => a.Dokumente)
                .Include(p => p.Dokumente)
                .FirstOrDefaultAsync(p => p.ProjektId == id);
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
        public async Task<IActionResult> Create(Projekt projekt, int VorlageId)
        {
            if (projekt.StartdatumGeplant != null)
            {
                if(projekt.EnddatumGeplant != null)
                {
                    if(projekt.StartdatumGeplant>projekt.EnddatumGeplant)
                    {
                        ModelState.AddModelError("StartdatumGeplant", "Geplantes Startdatum ist nach geplantem Enddatum.");
                        return View(projekt);
                    }
                }
            }
            projekt.Fortschritt = 0;
            if (ModelState.IsValid)
            {
                var original = await _context.Vorgehensmodelle
                    .Include(vm => vm.Projektphasen)
                    .FirstOrDefaultAsync(vm => vm.VorgehensmodellId == VorlageId);

                if (original == null)
                {
                    ModelState.AddModelError("", "Vorgehensmodell nicht gefunden.");
                    return View(projekt);
                }

                var kopie = new Vorgehensmodell
                {
                    Name = original.Name,
                    IstVorlage = false,
                    Projektphasen = new List<Projektphase>()
                };

                foreach (var phase in original.Projektphasen)
                {
                    kopie.Projektphasen.Add(new Projektphase
                    {
                        ProjektphaseName = phase.ProjektphaseName,
                        Status = "NEU",
                        Fortschritt = 0,
                        DauerInTagen = phase.DauerInTagen
                    });
                }

                projekt.Vorgehensmodell = kopie;

                _context.Add(projekt);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
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
            // Dropdown für Projektleiter
            ViewBag.ProjektleiterId = new SelectList(
                _context.Mitarbeiter
                    .Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }),
                "MitarbeiterId", "Name");

            // Dropdown für Vorgehensmodell-Vorlagen
            ViewBag.VorlageVorgehensmodellId = new SelectList(
                _context.Vorgehensmodelle.Where(vm => vm.IstVorlage),
                "VorgehensmodellId", "Name");
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(projekt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projekt projekt, string returnUrl)
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
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
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
            // Lade das Projekt inklusive der Dokumente (auf allen Ebenen)
            var projekt = await _context.Projekte
                .Include(p => p.Dokumente)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Dokumente)
                .Include(p => p.Vorgehensmodell)
                    .ThenInclude(vm => vm.Projektphasen)
                        .ThenInclude(pp => pp.Aktivitaeten)
                            .ThenInclude(a => a.Dokumente)
                .FirstOrDefaultAsync(p => p.ProjektId == id);

            if (projekt != null)
            {
                if (projekt.Dokumente != null && projekt.Dokumente.Any())
                {
                    _context.Dokumente.RemoveRange(projekt.Dokumente);
                }

                if (projekt.Vorgehensmodell != null && projekt.Vorgehensmodell.Projektphasen != null)
                {
                    foreach (var phase in projekt.Vorgehensmodell.Projektphasen)
                    {
                        if (phase.Dokumente != null && phase.Dokumente.Any())
                        {
                            _context.Dokumente.RemoveRange(phase.Dokumente);
                        }

                        if (phase.Aktivitaeten != null)
                        {
                            foreach (var activity in phase.Aktivitaeten)
                            {
                                if (activity.Dokumente != null && activity.Dokumente.Any())
                                {
                                    _context.Dokumente.RemoveRange(activity.Dokumente);
                                }
                            }
                        }
                    }
                }

                _context.Projekte.Remove(projekt);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
          return (_context.Projekte?.Any(e => e.ProjektId == id)).GetValueOrDefault();
        }
    }
}
