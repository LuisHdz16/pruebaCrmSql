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
        public async Task<IActionResult> Index()
        {
            var crmContext = _context.InscripcionCursos.Include(i => i.IdClienteNavigation).Include(i => i.IdCursoNavigation);
            return View(await crmContext.ToListAsync());
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
