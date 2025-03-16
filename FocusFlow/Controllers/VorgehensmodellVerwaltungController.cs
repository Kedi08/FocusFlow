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
        public async Task<IActionResult> Create(Vorgehensmodell vorgehensmodell, List<Projektphase> projektphasen)
        {
            vorgehensmodell.IstVorlage = true;

            if (projektphasen != null && projektphasen.Any())
            {
                for (int i = 0; i < projektphasen.Count; i++)
                {
                    projektphasen[i].Reihenfolge = i + 1;
                    projektphasen[i].Vorgehensmodell = vorgehensmodell;
                }
                vorgehensmodell.Projektphasen = projektphasen;
            }

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

            var vorgehensmodell = await _context.Vorgehensmodelle
                .Include(vm => vm.Projektphasen)
                .FirstOrDefaultAsync(vm => vm.VorgehensmodellId == id);

            if (vorgehensmodell == null)
            {
                return NotFound();
            }
            // Phasen nach Reihenfolge sortieren
            vorgehensmodell.Projektphasen = vorgehensmodell.Projektphasen
                .OrderBy(p => p.Reihenfolge)
                .ToList();
            return View(vorgehensmodell);
        }

        // POST: VorgehensmodellVerwaltung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Vorgehensmodell model)
        {
            

            var dbVorgehensmodell = await _context.Vorgehensmodelle
                .Include(vm => vm.Projektphasen)
                .FirstOrDefaultAsync(vm => vm.VorgehensmodellId == model.VorgehensmodellId);

            if (dbVorgehensmodell == null)
            {
                return NotFound();
            }

            dbVorgehensmodell.Name = model.Name;
            var aktuelleIds = model.Projektphasen
                .Where(p => p.ProjektphaseId != 0)
                .Select(p => p.ProjektphaseId)
                .ToList();

            //  Entferne Phasen, die im alten Objekt vorhanden sind, aber im neuen Model nicht mehr übergeben wurden
            foreach (var phase in dbVorgehensmodell.Projektphasen
                .Where(p => !aktuelleIds.Contains(p.ProjektphaseId))
                .ToList())
            {
                dbVorgehensmodell.Projektphasen.Remove(phase);
            }

            foreach (var phaseForm in model.Projektphasen)
            {
                if (phaseForm.ProjektphaseId != 0)
                {
                    var dbPhase = dbVorgehensmodell.Projektphasen
                        .FirstOrDefault(p => p.ProjektphaseId == phaseForm.ProjektphaseId);
                    if (dbPhase != null)
                    {
                        dbPhase.ProjektphaseName = phaseForm.ProjektphaseName;
                        dbPhase.DauerInTagen = phaseForm.DauerInTagen;
                        dbPhase.Reihenfolge = phaseForm.Reihenfolge;
                    }
                }
                else
                {
                    // Neue Phase: Hinzufügen
                    dbVorgehensmodell.Projektphasen.Add(new Projektphase
                    {
                        ProjektphaseName = phaseForm.ProjektphaseName,
                        DauerInTagen = phaseForm.DauerInTagen,
                        Reihenfolge = phaseForm.Reihenfolge
                    });
                }
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _context.SaveChangesAsync();

            // 5) Weiterleitung (z. B. zurück zur Übersicht)
            return RedirectToAction(nameof(Index));
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
