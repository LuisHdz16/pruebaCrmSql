using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRMBASEDEDATOS.Models;

namespace CRMBASEDEDATOS.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly CrmContext _context;

        public PromocionesController(CrmContext context)
        {
            _context = context;
        }

        // GET: Promociones
        public async Task<IActionResult> Index()
        {
            var crmContext = _context.Promociones.Include(p => p.IdTratamientoNavigation);
            return View(await crmContext.ToListAsync());
        }

        // GET: Promociones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocione = await _context.Promociones
                .Include(p => p.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdPromocion == id);
            if (promocione == null)
            {
                return NotFound();
            }

            return View(promocione);
        }

        // GET: Promociones/Create
        public IActionResult Create()
        {
            // Cambia "IdTratamiento" a "Nombre" para mostrar los nombres en la lista
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre");
            return View();
        }


        // POST: Promociones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPromocion,IdTratamiento,Nombre,Descripcion,Estatus, IdTratamientoNavigation")] Promocione promocione)
        {

                _context.Add(promocione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }


        // GET: Promociones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocione = await _context.Promociones.FindAsync(id);
            if (promocione == null)
            {
                return NotFound();
            }
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", promocione.IdTratamiento);
            return View(promocione);
        }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPromocion,IdTratamiento,Nombre,Descripcion,Estatus")] Promocione promocione)
        {
            if (id != promocione.IdPromocion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocioneExists(promocione.IdPromocion))
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
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", promocione.IdTratamiento);
            return View(promocione);
        }

        // GET: Promociones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocione = await _context.Promociones
                .Include(p => p.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdPromocion == id);
            if (promocione == null)
            {
                return NotFound();
            }

            return View(promocione);
        }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promocione = await _context.Promociones.FindAsync(id);
            if (promocione != null)
            {
                _context.Promociones.Remove(promocione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocioneExists(int id)
        {
            return _context.Promociones.Any(e => e.IdPromocion == id);
        }
    }
}