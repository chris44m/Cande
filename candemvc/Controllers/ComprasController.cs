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
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Compras.Include(c => c.IdAsientoNavigation).Include(c => c.IdCalleNavigation).Include(c => c.IdCompradorNavigation).Include(c => c.IdZonaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdAsientoNavigation)
                .Include(c => c.IdCalleNavigation)
                .Include(c => c.IdCompradorNavigation)
                .Include(c => c.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento");
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle");
            ViewData["IdComprador"] = new SelectList(_context.Compradores, "IdComprador", "IdComprador");
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,IdAsiento,IdZona,IdCalle,IdComprador,ImagenQr,FechaCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento", compra.IdAsiento);
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", compra.IdCalle);
            ViewData["IdComprador"] = new SelectList(_context.Compradores, "IdComprador", "IdComprador", compra.IdComprador);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", compra.IdZona);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento", compra.IdAsiento);
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", compra.IdCalle);
            ViewData["IdComprador"] = new SelectList(_context.Compradores, "IdComprador", "IdComprador", compra.IdComprador);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", compra.IdZona);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,IdAsiento,IdZona,IdCalle,IdComprador,ImagenQr,FechaCompra")] Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento", compra.IdAsiento);
            ViewData["IdCalle"] = new SelectList(_context.Calles, "IdCalle", "IdCalle", compra.IdCalle);
            ViewData["IdComprador"] = new SelectList(_context.Compradores, "IdComprador", "IdComprador", compra.IdComprador);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", compra.IdZona);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdAsientoNavigation)
                .Include(c => c.IdCalleNavigation)
                .Include(c => c.IdCompradorNavigation)
                .Include(c => c.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.IdCompra == id);
        }
    }
}
