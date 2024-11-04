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
    public class CitasController : Controller
    {
        private readonly CrmContext _context;

        public CitasController(CrmContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index(string campo, string buscar, int page = 1)
        {
            int pageSize = 10; // Cambia esto a 10 si deseas mostrar hasta 10 elementos por página
            var citas = _context.Citas
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdTratamientoNavigation)
                .Include(c => c.IdPromocionNavigation)
                .AsQueryable(); // Usa AsQueryable para que puedas aplicar filtros posteriormente

            // Filtrar citas según el campo y término de búsqueda
            if (!String.IsNullOrEmpty(buscar))
            {
                switch (campo)
                {
                    case "IdClienteNavigation.Nombre":
                        citas = citas.Where(c => c.IdClienteNavigation.Nombre.Contains(buscar));
                        break;
                    case "IdTratamientoNavigation.Nombre":
                        citas = citas.Where(c => c.IdTratamientoNavigation.Nombre.Contains(buscar));
                        break;
                    case "IdPromocionNavigation.Nombre":
                        citas = citas.Where(c => c.IdPromocionNavigation.Nombre.Contains(buscar));
                        break;
                    case "Fecha":
                        if (DateTime.TryParse(buscar, out DateTime fechaBuscada))
                        {
                            var fechaInicio = fechaBuscada.Date;
                            var fechaFin = fechaBuscada.Date.AddDays(1); // Día siguiente para definir el rango
                            citas = citas.Where(c => c.Fecha >= fechaInicio && c.Fecha < fechaFin);
                        }
                        break;
                    case "Estatus":
                        citas = citas.Where(c => c.Estatus.Contains(buscar));
                        break;
                    default:
                        break; // Si no coincide, no aplicamos ningún filtro adicional
                }
            }

            // Contar el total de elementos después de aplicar los filtros
            int totalCount = await citas.CountAsync();

            // Obtener las citas paginadas
            var citasPaginated = await citas
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calcular el total de páginas y la página actual
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            return View(citasPaginated);
        }


        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdPromocionNavigation)
                .Include(c => c.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewData["IdPromocion"] = new SelectList(_context.Promociones, "IdPromocion", "Nombre");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdCliente,IdTratamiento,IdPromocion,Fecha,Precio,Estatus")] Cita cita)
        {

                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", cita.IdCliente);
            ViewData["IdPromocion"] = new SelectList(_context.Promociones, "IdPromocion", "Nombre", cita.IdPromocion);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", cita.IdTratamiento);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdCliente,IdTratamiento,IdPromocion,Fecha,Precio,Estatus")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdPromocionNavigation)
                .Include(c => c.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
