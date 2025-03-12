using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class ProjektphasenController : Controller
    {
        private readonly AppDbContext _context;

        public ProjektphasenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projektphasen
        public async Task<IActionResult> Index()
        {
            var phasen = _context.Projektphasen
                .Include(pp => pp.Projekt)
                .Include(pp => pp.Aktivitaeten);
            return View(await phasen.ToListAsync());
        }

        // GET: Projektphasen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var projektphase = await _context.Projektphasen
                .Include(pp => pp.Projekt)
                .Include(pp => pp.Aktivitaeten)
                .Include(pp => pp.Dokumente)
                .FirstOrDefaultAsync(m => m.ProjektphaseId == id);

            if (projektphase == null) return NotFound();

            return View(projektphase);
        }

        // GET: Projektphasen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projektphasen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projektphase projektphase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projektphase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projektphase);
        }

        // GET: Projektphasen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase == null) return NotFound();

            return View(projektphase);
        }

        // POST: Projektphasen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projektphase projektphase)
        {
            if (id != projektphase.ProjektphaseId)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projektphase);
        }

        // GET: Projektphasen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var projektphase = await _context.Projektphasen
                .FirstOrDefaultAsync(m => m.ProjektphaseId == id);

            if (projektphase == null) return NotFound();

            return View(projektphase);
        }

        // POST: Projektphasen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase != null)
            {
                _context.Projektphasen.Remove(projektphase);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektphaseExists(int id)
        {
            return _context.Projektphasen.Any(e => e.ProjektphaseId == id);
        }
    }
}
