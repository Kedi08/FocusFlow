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
    public class ProjektphaseVerwaltungController : Controller
    {
        private readonly AppDbContext _context;

        public ProjektphaseVerwaltungController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjektphaseVerwaltung/Create
        public IActionResult Create()
        {
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View();
        }

        // POST: ProjektphaseVerwaltung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projektphase projektphase, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projektphase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: ProjektphaseVerwaltung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projektphasen == null)
            {
                return NotFound();
            }

            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(projektphase);
        }

        // POST: ProjektphaseVerwaltung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,   Projektphase projektphase, string returnUrl)
        {
            if (id != projektphase.ProjektphaseId)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(projektphase);
        }

        // GET: ProjektphaseVerwaltung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projektphasen == null)
            {
                return NotFound();
            }

            var projektphase = await _context.Projektphasen
                .FirstOrDefaultAsync(m => m.ProjektphaseId == id);
            if (projektphase == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(projektphase);
        }

        // POST: ProjektphaseVerwaltung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            if (_context.Projektphasen == null)
            {
                return Problem("Entity set 'AppDbContext.Projektphasen'  is null.");
            }
            var projektphase = await _context.Projektphasen.FindAsync(id);
            if (projektphase != null)
            {
                _context.Projektphasen.Remove(projektphase);
            }
            
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private bool ProjektphaseExists(int id)
        {
          return (_context.Projektphasen?.Any(e => e.ProjektphaseId == id)).GetValueOrDefault();
        }
    }
}
