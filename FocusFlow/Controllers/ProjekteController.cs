using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class ProjekteController : Controller
    {
        private readonly AppDbContext _context;

        public ProjekteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projekte
        public async Task<IActionResult> Index()
        {
            var projekte = await _context.Projekte
                .Include(p => p.Vorgehensmodell)
                .Include(p => p.Mitarbeiter)
                .ToListAsync();

            return View(projekte);
        }

        // GET: Projekte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var projekt = await _context.Projekte
                .Include(p => p.Vorgehensmodell)
                .Include(p => p.Mitarbeiter)
                .Include(p => p.Projektphasen)
                .ThenInclude(pp => pp.Aktivitaeten)
                .FirstOrDefaultAsync(m => m.ProjektId == id);

            if (projekt == null) return NotFound();

            return View(projekt);
        }

        // GET: Projekte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projekte/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projekt);
        }

        // GET: Projekte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var projekt = await _context.Projekte.FindAsync(id);
            if (projekt == null) return NotFound();

            return View(projekt);
        }

        // POST: Projekte/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projekt projekt)
        {
            if (id != projekt.ProjektId) return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projekt);
        }

        // GET: Projekte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var projekt = await _context.Projekte
                .FirstOrDefaultAsync(m => m.ProjektId == id);

            if (projekt == null) return NotFound();

            return View(projekt);
        }

        // POST: Projekte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projekt = await _context.Projekte.FindAsync(id);
            _context.Projekte.Remove(projekt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
            return _context.Projekte.Any(e => e.ProjektId == id);
        }
    }
}
