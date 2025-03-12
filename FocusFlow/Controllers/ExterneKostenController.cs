using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class ExterneKostenController : Controller
    {
        private readonly AppDbContext _context;

        public ExterneKostenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExterneKosten
        public async Task<IActionResult> Index()
        {
            var externeKosten = _context.ExterneKosten
                .Include(ek => ek.Aktivitaet);
            return View(await externeKosten.ToListAsync());
        }

        // GET: ExterneKosten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kosten = await _context.ExterneKosten
                .Include(ek => ek.Aktivitaet)
                .FirstOrDefaultAsync(m => m.ExterneKostenId == id);

            if (kosten == null) return NotFound();

            return View(kosten);
        }

        // GET: ExterneKosten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExterneKosten/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExterneKosten kosten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kosten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kosten);
        }

        // GET: ExterneKosten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var kosten = await _context.ExterneKosten.FindAsync(id);
            if (kosten == null) return NotFound();

            return View(kosten);
        }

        // POST: ExterneKosten/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExterneKosten kosten)
        {
            if (id != kosten.ExterneKostenId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kosten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExterneKostenExists(kosten.ExterneKostenId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kosten);
        }

        // GET: ExterneKosten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kosten = await _context.ExterneKosten
                .FirstOrDefaultAsync(m => m.ExterneKostenId == id);
            if (kosten == null) return NotFound();

            return View(kosten);
        }

        // POST: ExterneKosten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kosten = await _context.ExterneKosten.FindAsync(id);
            if (kosten != null)
            {
                _context.ExterneKosten.Remove(kosten);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExterneKostenExists(int id)
        {
            return _context.ExterneKosten.Any(e => e.ExterneKostenId == id);
        }
    }
}
