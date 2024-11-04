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
        public async Task<IActionResult> Index(string campo, string buscar, int page = 1)
        {
            int pageSize = 10; // Cambia esto a 10 si deseas mostrar hasta 10 elementos por página
            var promociones = _context.Promociones.Include(p => p.IdTratamientoNavigation).AsQueryable(); // Usa AsQueryable para que puedas aplicar filtros posteriormente

            // Filtrar promociones según el campo y término de búsqueda
            if (!String.IsNullOrEmpty(buscar))
            {
                switch (campo)
                {
                    case "IdTratamientoNavigation.Nombre":
                        promociones = promociones.Where(c => c.IdTratamientoNavigation.Nombre.Contains(buscar));
                        break;
                    case "Nombre":
                        promociones = promociones.Where(c => c.Nombre.Contains(buscar));
                        break;
                    case "Estatus":
                        promociones = promociones.Where(c => c.Estatus.Contains(buscar));
                        break;
                    default:
                        break; // Si no coincide, no aplicamos ningún filtro adicional
                }
            }

            // Contar el total de elementos después de aplicar los filtros
            int totalCount = await promociones.CountAsync();

            // Obtener las promociones paginadas
            var promocionesPaginated = await promociones
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calcular el total de páginas y la página actual
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            return View(promocionesPaginated);
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