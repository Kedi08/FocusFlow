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
    public class AktivitaetenController : Controller
    {
        private readonly AppDbContext _context;

        public AktivitaetenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Aktivitaeten/Create
        public IActionResult Create(int phaseid)
        {
            ViewData["MitarbeiterId"] = new SelectList(
                _context.Mitarbeiter.Select(m => new
                {
                    MitarbeiterId = m.MitarbeiterId,
                    Name = m.Vorname + " " + m.Nachname
                }),
                "MitarbeiterId",
                "Name"
            );
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            ViewBag.ProjektphaseId = phaseid;
            return View();
        }

        // POST: Aktivitaeten/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aktivitaet aktivitaet, string returnUrl)
        {
            aktivitaet.Fortschritt = 0;
            if (ModelState.IsValid)
            {
                _context.Add(aktivitaet);
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
            ViewData["MitarbeiterId"] = new SelectList(
                _context.Mitarbeiter.Select(m => new
                {
                    MitarbeiterId = m.MitarbeiterId,
                    Name = m.Vorname + " " + m.Nachname
                }),
                "MitarbeiterId",
                "Name"
            );
            return View(aktivitaet);
        }

        // GET: Aktivitaeten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitaet = await _context.Aktivitaeten.FindAsync(id);
            if (aktivitaet == null)
            {
                return NotFound();
            }
            ViewData["MitarbeiterId"] = new SelectList(
                _context.Mitarbeiter.Select(m => new
                {
                    MitarbeiterId = m.MitarbeiterId,
                    Name = m.Vorname + " " + m.Nachname
                }),
                "MitarbeiterId",
                "Name"
            );
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(aktivitaet);
        }

        // POST: Aktivitaeten/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aktivitaet aktivitaet, string returnUrl)
        {
            if (id != aktivitaet.AktivitaetId)
            {
                return NotFound();
            }

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
            ViewData["MitarbeiterId"] = new SelectList(_context.Mitarbeiter, "MitarbeiterId", "MitarbeiterId", aktivitaet.MitarbeiterId);
            return View(aktivitaet);
        }

        // GET: Aktivitaeten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitaet = await _context.Aktivitaeten
                .Include(a => a.Mitarbeiter)
                .FirstOrDefaultAsync(m => m.AktivitaetId == id);
            if (aktivitaet == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(aktivitaet);
        }

        // POST: Aktivitaeten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            var aktivitaet = await _context.Aktivitaeten.FindAsync(id);
            if (aktivitaet != null)
            {
                _context.Aktivitaeten.Remove(aktivitaet);
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

        private bool AktivitaetExists(int id)
        {
            return _context.Aktivitaeten.Any(e => e.AktivitaetId == id);
        }
    }
}
