using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FocusFlow.Data;
using FocusFlow.Models;
using System.Reflection.Metadata;

namespace FocusFlow.Controllers
{
    public class DokumenteController : Controller
    {
        private readonly AppDbContext _context;

        public DokumenteController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Dokumente/Create
        public IActionResult Create(int parentId, string parentType)
        {
            ViewBag.ParentId = parentId;
            ViewBag.ParentType = parentType;
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(new Dokument());
        }

        // POST: Dokumente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dokument dokument, int parentId, string parentType, string returnUrl)
        {
            dokument.Pfad = "Not implemented";
            if (!ModelState.IsValid)
            {
                return View(dokument);
            }
            switch (parentType)
            {
                case "Projekt":
                    var projekt = await _context.Projekte
                        .Include(p => p.Dokumente)
                        .FirstOrDefaultAsync(p => p.ProjektId == parentId);
                    if (projekt == null)
                    {
                        ModelState.AddModelError("", "Projekt nicht gefunden.");
                        return View(dokument);
                    }
                    if (projekt.Dokumente == null)
                        projekt.Dokumente = new List<Dokument>();
                    projekt.Dokumente.Add(dokument);
                    break;

                case "Projektphase":
                    var phase = await _context.Projektphasen
                        .Include(p => p.Dokumente)
                        .FirstOrDefaultAsync(p => p.ProjektphaseId == parentId);
                    if (phase == null)
                    {
                        ModelState.AddModelError("", "Projektphase nicht gefunden.");
                        return View(dokument);
                    }
                    if (phase.Dokumente == null)
                        phase.Dokumente = new List<Dokument>();
                    phase.Dokumente.Add(dokument);
                    break;

                case "Aktivitaet":
                    var aktivitaet = await _context.Aktivitaeten 
                        .Include(a => a.Dokumente)
                        .FirstOrDefaultAsync(a => a.AktivitaetId == parentId);
                    if (aktivitaet == null)
                    {
                        ModelState.AddModelError("", "Aktivität nicht gefunden.");
                        return View(dokument);
                    }
                    if (aktivitaet.Dokumente == null)
                        aktivitaet.Dokumente = new List<Dokument>();
                    aktivitaet.Dokumente.Add(dokument);
                    break;

                default:
                    ModelState.AddModelError("", "Ungültiger ParentType.");
                    return View(dokument);
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

        // GET: Dokumente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokument = await _context.Dokumente.FindAsync(id);
            if (dokument == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(dokument);
        }

        // POST: Dokumente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dokument dokument, string returnUrl)
        {
            dokument.Pfad = "Not implemented";
            if (id != dokument.DokumentId)
            {
                return NotFound();
            }

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
            return RedirectToAction("Index");
        }

        // GET: Dokumente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokument = await _context.Dokumente
                .FirstOrDefaultAsync(m => m.DokumentId == id);
            if (dokument == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return View(dokument);
        }

        // POST: Dokumente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            var dokument = await _context.Dokumente.FindAsync(id);
            if (dokument != null)
            {
                _context.Dokumente.Remove(dokument);
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

        private bool DokumentExists(int id)
        {
            return _context.Dokumente.Any(e => e.DokumentId == id);
        }
    }
}
