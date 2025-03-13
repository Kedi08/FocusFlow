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
              return _context.Projekte != null ? 
                          View(await _context.Projekte.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Projekte'  is null.");
        }

        // GET: ProjektVerwaltung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projekte == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekte
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
            // bsp für laden von virtual
            //var projekt = _context.Projekte
            //    .Include(p => p.Projektphase)
            //    .FirstOrDefault(p => p.Id == id);


            ViewBag.VorgehensmodellListe = _context.Vorgehensmodelle.Select(vm => new SelectListItem
            {
                Value = vm.VorgehensmodellId.ToString(),
                Text = vm.Name
            }).ToList();

            ViewBag.MitarbeiterListe = _context.Mitarbeiter.Select(m => new SelectListItem
            {
                Value = m.MitarbeiterId.ToString(),
                Text = $"{m.Vorname} {m.Nachname}"
            }).ToList();

            return View();
        }

        // POST: ProjektVerwaltung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjektId,Titel,Beschreibung,Bewilligungsdatum,Prioritaet,Status,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,Fortschritt")] Projekt projekt, List<int> MitarbeiterIds, int VorgehensmodellId)
        {
            var vorgehensmodell = _context.Vorgehensmodelle.Find(VorgehensmodellId);
            if (vorgehensmodell != null)
            {
                projekt.Vorgehensmodell = vorgehensmodell;
            }
            // Mitarbeiter dem Projekt zuweisen (falls notwendig)
            if (MitarbeiterIds != null && MitarbeiterIds.Any())
            {
                projekt.Mitarbeiter = _context.Mitarbeiter.Where(m => MitarbeiterIds.Contains(m.MitarbeiterId)).ToList();
            }
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Falls Fehler auftreten, muss die Liste erneut geladen werden
            ViewBag.VorgehensmodellListe = _context.Vorgehensmodelle.Select(vm => new SelectListItem
            {
                Value = vm.VorgehensmodellId.ToString(),
                Text = vm.Name
            }).ToList();
            // Falls Fehler auftreten, muss die Liste erneut geladen werden
            ViewBag.MitarbeiterListe = _context.Mitarbeiter.Select(m => new SelectListItem
            {
                Value = m.MitarbeiterId.ToString(),
                Text = $"{m.Vorname} {m.Nachname}"
            }).ToList();
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
            return View(projekt);
        }

        // POST: ProjektVerwaltung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektId,Titel,Beschreibung,Bewilligungsdatum,Prioritaet,Status,StartdatumGeplant,EnddatumGeplant,StartdatumEffektiv,EnddatumEffektiv,Fortschritt")] Projekt projekt)
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
