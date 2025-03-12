using FocusFlow.Data;
using FocusFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Controllers
{
    public class MeilensteineController : Controller
    {
        private readonly AppDbContext _context;

        public MeilensteineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Meilensteine
        public async Task<IActionResult> Index()
        {
            var meilensteine = _context.Meilensteine
                .Include(m => m.Projekt);
            return View(await meilensteine.ToListAsync());
        }

        // GET: Meilensteine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var meilenstein = await _context.Meilensteine
                .Include(m => m.Projekt)
                .FirstOrDefaultAsync(m => m.MeilensteinId == id);

            if (meilenstein == null) return NotFound();

            return View(meilenstein);
        }

        // GET: Meilensteine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meilensteine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meilenstein meilenstein)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meilenstein);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meilenstein);
        }

        // GET: Meilensteine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var meilenstein = await _context.Meilensteine.FindAsync(id);
            if (meilenstein == null) return NotFound();

            return View(meilenstein);
        }

        // POST: Meilensteine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Meilenstein meilenstein)
        {
            if (id != meilenstein.MeilensteinId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meilenstein);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeilensteinExists(meilenstein.MeilensteinId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meilenstein);
        }

        // GET: Meilensteine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var meilenstein = await _context.Meilensteine
                .FirstOrDefaultAsync(m => m.MeilensteinId == id);
            if (meilenstein == null) return NotFound();

            return View(meilenstein);
        }

        // POST: Meilensteine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meilenstein = await _context.Meilensteine.FindAsync(id);
            if (meilenstein != null)
            {
                _context.Meilensteine.Remove(meilenstein);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MeilensteinExists(int id)
        {
            return _context.Meilensteine.Any(e => e.MeilensteinId == id);
        }
    }
}
