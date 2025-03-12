using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class PersonelleRessourcenController : Controller
    {
        private readonly AppDbContext _context;

        public PersonelleRessourcenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonelleRessourcen
        public async Task<IActionResult> Index()
        {
            var ressourcen = _context.PersonelleRessourcen
                .Include(r => r.Aktivitaet);
            return View(await ressourcen.ToListAsync());
        }

        // GET: PersonelleRessourcen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ressource = await _context.PersonelleRessourcen
                .Include(r => r.Aktivitaet)
                .FirstOrDefaultAsync(m => m.PersonelleRessourceId == id);

            if (ressource == null) return NotFound();

            return View(ressource);
        }

        // GET: PersonelleRessourcen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonelleRessourcen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonelleRessource ressource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ressource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ressource);
        }

        // GET: PersonelleRessourcen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ressource = await _context.PersonelleRessourcen.FindAsync(id);
            if (ressource == null) return NotFound();

            return View(ressource);
        }

        // POST: PersonelleRessourcen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonelleRessource ressource)
        {
            if (id != ressource.PersonelleRessourceId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ressource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelleRessourceExists(ressource.PersonelleRessourceId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ressource);
        }

        // GET: PersonelleRessourcen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ressource = await _context.PersonelleRessourcen
                .FirstOrDefaultAsync(m => m.PersonelleRessourceId == id);
            if (ressource == null) return NotFound();

            return View(ressource);
        }

        // POST: PersonelleRessourcen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ressource = await _context.PersonelleRessourcen.FindAsync(id);
            if (ressource != null)
            {
                _context.PersonelleRessourcen.Remove(ressource);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelleRessourceExists(int id)
        {
            return _context.PersonelleRessourcen.Any(e => e.PersonelleRessourceId == id);
        }
    }
}
