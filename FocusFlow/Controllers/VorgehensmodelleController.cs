using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class VorgehensmodelleController : Controller
    {
        private readonly AppDbContext _context;

        public VorgehensmodelleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vorgehensmodelle
        public async Task<IActionResult> Index()
        {
            var modelle = await _context.Vorgehensmodelle
                .Include(vm => vm.Phasen)
                .ToListAsync();
            return View(modelle);
        }

        // GET: Vorgehensmodelle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var modell = await _context.Vorgehensmodelle
                .Include(vm => vm.Phasen)
                .FirstOrDefaultAsync(m => m.VorgehensmodellId == id);

            if (modell == null) return NotFound();

            return View(modell);
        }

        // GET: Vorgehensmodelle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vorgehensmodelle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vorgehensmodell vorgehensmodell)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vorgehensmodell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vorgehensmodell);
        }

        // GET: Vorgehensmodelle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var modell = await _context.Vorgehensmodelle.FindAsync(id);
            if (modell == null) return NotFound();

            return View(modell);
        }

        // POST: Vorgehensmodelle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vorgehensmodell vorgehensmodell)
        {
            if (id != vorgehensmodell.VorgehensmodellId)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vorgehensmodell);
        }

        // GET: Vorgehensmodelle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var modell = await _context.Vorgehensmodelle
                .FirstOrDefaultAsync(m => m.VorgehensmodellId == id);

            if (modell == null) return NotFound();

            return View(modell);
        }

        // POST: Vorgehensmodelle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modell = await _context.Vorgehensmodelle.FindAsync(id);
            if (modell != null)
            {
                _context.Vorgehensmodelle.Remove(modell);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VorgehensmodellExists(int id)
        {
            return _context.Vorgehensmodelle.Any(e => e.VorgehensmodellId == id);
        }
    }
}
