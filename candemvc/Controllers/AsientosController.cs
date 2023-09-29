using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CandelariaP.Data;
using CandelariaP.Models;

namespace CandelariaP.Controllers
{
    public class AsientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asientos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Asientos.Include(a => a.IdCalleNavigation).Include(a => a.IdZonaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Asientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.IdCalleNavigation)
                .Include(a => a.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // GET: Asientos/Create
        public IActionResult Create()
        {
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle");
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona");
            return View();
        }

        // POST: Asientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsiento,IdZona,IdCalle,Disponible")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", asiento.IdCalle);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", asiento.IdZona);
            return View(asiento);
        }

        // GET: Asientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", asiento.IdCalle);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", asiento.IdZona);
            return View(asiento);
        }

        // POST: Asientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsiento,IdZona,IdCalle,Disponible")] Asiento asiento)
        {
            if (id != asiento.IdAsiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoExists(asiento.IdAsiento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", asiento.IdCalle);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", asiento.IdZona);
            return View(asiento);
        }

        // GET: Asientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.IdCalleNavigation)
                .Include(a => a.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asiento = await _context.Asientos.FindAsync(id);
            _context.Asientos.Remove(asiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoExists(int id)
        {
            return _context.Asientos.Any(e => e.IdAsiento == id);
        }
    }
}
