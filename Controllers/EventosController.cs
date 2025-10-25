using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MZ_MJ_Proyecto1.Data;
using Proyecto1_MZ_MJ.Models;

namespace MZ_MJ_Proyecto1.Controllers
{
    public class EventosController : Controller
    {
        private readonly MZ_MJ_Proyecto1Context _context;
        private readonly IWebHostEnvironment _env;

        public EventosController(MZ_MJ_Proyecto1Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return _context.Evento != null ?
                        View(await _context.Evento.OrderBy(e => e.FechaEvento).ToListAsync()) :
                        Problem("Entity set 'MZ_MJ_Proyecto1Context.Evento' is null.");
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Titulo,Descripcion,FechaEvento,Ubicacion,Organizador,TipoEvento,CapacidadMaxima,Activo,RequiereConfirmacion,NotasAdicionales")] Evento evento, IFormFile? imagen)
        {
            if (ModelState.IsValid)
            {
                // Guardar imagen si existe
                if (imagen != null && imagen.Length > 0)
                {
                    var carpeta = Path.Combine(_env.WebRootPath, "imagenes_eventos");
                    Directory.CreateDirectory(carpeta);
                    var archivo = Guid.NewGuid() + Path.GetExtension(imagen.FileName);
                    var ruta = Path.Combine(carpeta, archivo);
                    using (var stream = new FileStream(ruta, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }
                    evento.ImagenEvento = "/imagenes_eventos/" + archivo;
                }

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Titulo,Descripcion,FechaEvento,Ubicacion,Organizador,TipoEvento,CapacidadMaxima,ImagenEvento,Activo,RequiereConfirmacion,NotasAdicionales")] Evento evento, IFormFile? imagen)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Guardar nueva imagen si existe
                    if (imagen != null && imagen.Length > 0)
                    {
                        var carpeta = Path.Combine(_env.WebRootPath, "imagenes_eventos");
                        Directory.CreateDirectory(carpeta);
                        var archivo = Guid.NewGuid() + Path.GetExtension(imagen.FileName);
                        var ruta = Path.Combine(carpeta, archivo);
                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await imagen.CopyToAsync(stream);
                        }
                        evento.ImagenEvento = "/imagenes_eventos/" + archivo;
                    }

                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoId))
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
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evento == null)
            {
                return Problem("Entity set 'MZ_MJ_Proyecto1Context.Evento' is null.");
            }
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return (_context.Evento?.Any(e => e.EventoId == id)).GetValueOrDefault();
        }
    }
}