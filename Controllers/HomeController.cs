using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MZ_MJ_Proyecto1.Data; // Importante: Donde vive tu Context
using MZ_MJ_Proyecto1.Models;
using Proyecto1_MZ_MJ.Models;
using System.Diagnostics;

namespace MZ_MJ_Proyecto1.Controllers
{
    public class HomeController : Controller
    {
        // 1. Declaramos la variable que usará la base de datos
        private readonly MZ_MJ_Proyecto1Context _context;
        private readonly ILogger<HomeController> _logger;
        // 2. El CONSTRUCTOR: Aquí es donde se soluciona el error CS0103
        // ASP.NET busca el Context y lo asigna a _context
        public HomeController(MZ_MJ_Proyecto1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Creamos el ViewModel y lo llenamos con datos de las dos tablas
            var viewModel = new HomeIndexViewModel
            {
                // Usamos .Queja y .Evento porque así los llamaste en tu archivo DataContext
                Quejas = await _context.Queja.ToListAsync() ?? new List<Queja>(),
                Eventos = await _context.Evento.ToListAsync() ?? new List<Evento>()
            };

            // Pasamos el paquete (viewModel) a la vista
            return View(viewModel);
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