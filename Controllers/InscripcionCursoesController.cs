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
    public class InscripcionCursoesController : Controller
    {
        private readonly CrmContext _context;

        public InscripcionCursoesController(CrmContext context)
        {
            _context = context;
        }

        // GET: InscripcionCursoes
        public async Task<IActionResult> Index(string campo, string buscar, int page = 1)
        {
            int pageSize = 10; // Cambia esto a 10 si deseas mostrar hasta 10 elementos por página
            var inscripciones = _context.InscripcionCursos
                .Include(i => i.IdClienteNavigation)
                .Include(i => i.IdCursoNavigation)
                .AsQueryable(); // Usa AsQueryable para que puedas aplicar filtros posteriormente

            // Filtrar inscripciones según el campo y término de búsqueda
            if (!String.IsNullOrEmpty(buscar))
            {
                switch (campo)
                {
                    case "IdClienteNavigation.Nombre":
                        inscripciones = inscripciones.Where(i => i.IdClienteNavigation.Nombre.Contains(buscar));
                        break;
                    case "IdCursoNavigation.Nombre":
                        inscripciones = inscripciones.Where(i => i.IdCursoNavigation.Nombre.Contains(buscar));
                        break;
                    case "FechaInicio":
                        if (DateTime.TryParse(buscar, out DateTime fechaInicioBuscada))
                        {
                            var fechaInicio = DateOnly.FromDateTime(fechaInicioBuscada);
                            inscripciones = inscripciones.Where(i => i.FechaInicio == fechaInicio);
                        }
                        break;
                    case "Estatus":
                        inscripciones = inscripciones.Where(i => i.Estatus.Contains(buscar));
                        break;
                    default:
                        break; // Si no coincide, no aplicamos ningún filtro adicional
                }
            }

            // Contar el total de elementos después de aplicar los filtros
            int totalCount = await inscripciones.CountAsync();

            // Obtener las inscripciones paginadas
            var inscripcionesPaginated = await inscripciones
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calcular el total de páginas y la página actual
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            return View(inscripcionesPaginated);
        }


        // GET: InscripcionCursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionCurso = await _context.InscripcionCursos
                .Include(i => i.IdClienteNavigation)
                .Include(i => i.IdCursoNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (inscripcionCurso == null)
            {
                return NotFound();
            }

            return View(inscripcionCurso);
        }

        // GET: InscripcionCursoes/Create
        public IActionResult Create()
        {
            // Cambia "IdTratamiento" a "Nombre" para mostrar los nombres en la lista
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre");
            return View();
        }


        // POST: InscripcionCursoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInscripcion,IdCliente,IdCurso,FechaInicio,FechaFin,Duracion,PrecioTotal,Estatus")] InscripcionCurso inscripcionCurso)
        {
            var cli = await _context.Clientes.FirstOrDefaultAsync(m => m.IdCliente == inscripcionCurso.IdCliente);
            if (cli != null)
            {
                inscripcionCurso.IdClienteNavigation = cli;
            }    

                _context.Add(inscripcionCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: InscripcionCursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionCurso = await _context.InscripcionCursos.FindAsync(id);
            if (inscripcionCurso == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", inscripcionCurso.IdCliente);
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", inscripcionCurso.IdCurso);
            return View(inscripcionCurso);
        }

        // POST: InscripcionCursoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInscripcion,IdCliente,IdCurso,FechaInicio,FechaFin,Duracion,PrecioTotal,Estatus")] InscripcionCurso inscripcionCurso)
        {
            if (id != inscripcionCurso.IdInscripcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcionCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionCursoExists(inscripcionCurso.IdInscripcion))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", inscripcionCurso.IdCliente);
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", inscripcionCurso.IdCurso);
            return View(inscripcionCurso);
        }

        // GET: InscripcionCursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionCurso = await _context.InscripcionCursos
                .Include(i => i.IdClienteNavigation)
                .Include(i => i.IdCursoNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (inscripcionCurso == null)
            {
                return NotFound();
            }

            return View(inscripcionCurso);
        }

        // POST: InscripcionCursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcionCurso = await _context.InscripcionCursos.FindAsync(id);
            if (inscripcionCurso != null)
            {
                _context.InscripcionCursos.Remove(inscripcionCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionCursoExists(int id)
        {
            return _context.InscripcionCursos.Any(e => e.IdInscripcion == id);
        }
    }
}
