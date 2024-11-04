using CRMBASEDEDATOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace CRMBASEDEDATOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrmContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(CrmContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Inicio()
        {
            var hoyOnly = DateOnly.FromDateTime(DateTime.Today); // Convertir hoy a DateOnly

            var model = new DashboardViewModel
            {
                CitasHoy = _context.Citas
                    .Where(c => DateOnly.FromDateTime(c.Fecha) == hoyOnly) // Asegúrate de que c.Fecha sea de tipo DateTime
                    .ToList(),
                PromocionesActivas = _context.Promociones
                    .Where(p => p.Estatus == "Disponible")
                    .ToList()
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
