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
    public class CompradoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompradoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compradores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Compradores.ToListAsync());
        }

        // GET: Compradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compradore = await _context.Compradores
                .FirstOrDefaultAsync(m => m.IdComprador == id);
            if (compradore == null)
            {
                return NotFound();
            }

            return View(compradore);
        }

        // GET: Compradores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComprador,NombreComprador,ApellidoComprador,Dni,Email,Teléfono")] Compradore compradore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compradore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compradore);
        }

        // GET: Compradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compradore = await _context.Compradores.FindAsync(id);
            if (compradore == null)
            {
                return NotFound();
            }
            return View(compradore);
        }

        // POST: Compradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComprador,NombreComprador,ApellidoComprador,Dni,Email,Teléfono")] Compradore compradore)
        {
            if (id != compradore.IdComprador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compradore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompradoreExists(compradore.IdComprador))
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
            return View(compradore);
        }

        // GET: Compradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compradore = await _context.Compradores
                .FirstOrDefaultAsync(m => m.IdComprador == id);
            if (compradore == null)
            {
                return NotFound();
            }

            return View(compradore);
        }

        // POST: Compradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compradore = await _context.Compradores.FindAsync(id);
            _context.Compradores.Remove(compradore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompradoreExists(int id)
        {
            return _context.Compradores.Any(e => e.IdComprador == id);
        }
    }
}
