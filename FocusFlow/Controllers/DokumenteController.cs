using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class DokumenteController : Controller
    {
        private readonly AppDbContext _context;

        public DokumenteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dokumente
        public async Task<IActionResult> Index()
        {
            // Hier könntest du je nach Bedarf Includes machen:
            var dokumente = _context.Dokumente
                .Include(d => d.Projekt)
                .Include(d => d.Projektphase)
                .Include(d => d.Aktivitaet);
            return View(await dokumente.ToListAsync());
        }

        // GET: Dokumente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dokument = await _context.Dokumente
                .Include(d => d.Projekt)
                .Include(d => d.Projektphase)
                .Include(d => d.Aktivitaet)
                .FirstOrDefaultAsync(m => m.DokumentId == id);
            if (dokument == null) return NotFound();

            return View(dokument);
        }

        // GET: Dokumente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dokumente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dokument dokument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dokument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dokument);
        }

        // GET: Dokumente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dokument = await _context.Dokumente.FindAsync(id);
            if (dokument == null) return NotFound();

            return View(dokument);
        }

        // POST: Dokumente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dokument dokument)
        {
            if (id != dokument.DokumentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dokument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DokumentExists(dokument.DokumentId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dokument);
        }

        // GET: Dokumente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dokument = await _context.Dokumente
                .FirstOrDefaultAsync(m => m.DokumentId == id);
            if (dokument == null) return NotFound();

            return View(dokument);
        }

        // POST: Dokumente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dokument = await _context.Dokumente.FindAsync(id);
            if (dokument != null)
            {
                _context.Dokumente.Remove(dokument);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DokumentExists(int id)
        {
            return _context.Dokumente.Any(e => e.DokumentId == id);
        }
    }
}
