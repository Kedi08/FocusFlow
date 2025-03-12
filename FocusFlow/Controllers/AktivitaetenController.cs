using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FocusFlow.Controllers
{
    public class AktivitaetenController : Controller
    {
        private readonly AppDbContext _context;

        public AktivitaetenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Aktivitaeten
        public async Task<IActionResult> Index()
        {
            var aktivitaeten = _context.Aktivitaeten
                .Include(a => a.VerantwortlichePerson)
                .Include(a => a.Projektphase);
            return View(await aktivitaeten.ToListAsync());
        }

        // GET: Aktivitaeten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var aktivitaet = await _context.Aktivitaeten
                .Include(a => a.VerantwortlichePerson)
                .Include(a => a.Projektphase)
                .Include(a => a.Ressourcen)
                .Include(a => a.ExterneKosten)
                .Include(a => a.Dokumente)
                .FirstOrDefaultAsync(m => m.AktivitaetId == id);

            if (aktivitaet == null) return NotFound();

            return View(aktivitaet);
        }

        // GET: Aktivitaeten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aktivitaeten/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aktivitaet aktivitaet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktivitaet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aktivitaet);
        }

        // GET: Aktivitaeten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var aktivitaet = await _context.Aktivitaeten.FindAsync(id);
            if (aktivitaet == null) return NotFound();

            return View(aktivitaet);
        }

        // POST: Aktivitaeten/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aktivitaet aktivitaet)
        {
            if (id != aktivitaet.AktivitaetId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktivitaet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktivitaetExists(aktivitaet.AktivitaetId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aktivitaet);
        }

        // GET: Aktivitaeten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var aktivitaet = await _context.Aktivitaeten
                .FirstOrDefaultAsync(m => m.AktivitaetId == id);
            if (aktivitaet == null) return NotFound();

            return View(aktivitaet);
        }

        // POST: Aktivitaeten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktivitaet = await _context.Aktivitaeten.FindAsync(id);
            if (aktivitaet != null)
            {
                _context.Aktivitaeten.Remove(aktivitaet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AktivitaetExists(int id)
        {
            return _context.Aktivitaeten.Any(e => e.AktivitaetId == id);
        }
    }
}
