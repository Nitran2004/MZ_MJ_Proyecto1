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
    public class QuejasController : Controller
    {
        private readonly MZ_MJ_Proyecto1Context _context;
        private readonly IWebHostEnvironment _env;

        public QuejasController(MZ_MJ_Proyecto1Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Quejas
        public async Task<IActionResult> Index()
        {
            return _context.Queja != null ?
                        View(await _context.Queja.ToListAsync()) :
                        Problem("Entity set 'MZ_MJ_Proyecto1Context.Queja' is null.");
        }

        // GET: Quejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja
                .FirstOrDefaultAsync(m => m.QuejaId == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // GET: Quejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quejas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuejaId,Nombre,QuejaTexto,Sector")] Queja queja, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                // Guardar foto si existe
                if (foto != null && foto.Length > 0)
                {
                    var carpeta = Path.Combine(_env.WebRootPath, "fotos");
                    Directory.CreateDirectory(carpeta);
                    var archivo = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                    var ruta = Path.Combine(carpeta, archivo);
                    using (var stream = new FileStream(ruta, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }
                    queja.Foto = "/fotos/" + archivo;
                }

                _context.Add(queja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(queja);
        }

        // GET: Quejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja.FindAsync(id);
            if (queja == null)
            {
                return NotFound();
            }
            return View(queja);
        }

        // POST: Quejas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuejaId,Nombre,QuejaTexto,Foto,Sector")] Queja queja, IFormFile? foto)
        {
            if (id != queja.QuejaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Guardar nueva foto si existe
                    if (foto != null && foto.Length > 0)
                    {
                        var carpeta = Path.Combine(_env.WebRootPath, "fotos");
                        Directory.CreateDirectory(carpeta);
                        var archivo = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                        var ruta = Path.Combine(carpeta, archivo);
                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }
                        queja.Foto = "/fotos/" + archivo;
                    }

                    _context.Update(queja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuejaExists(queja.QuejaId))
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
            return View(queja);
        }

        // GET: Quejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja
                .FirstOrDefaultAsync(m => m.QuejaId == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // POST: Quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Queja == null)
            {
                return Problem("Entity set 'MZ_MJ_Proyecto1Context.Queja' is null.");
            }
            var queja = await _context.Queja.FindAsync(id);
            if (queja != null)
            {
                _context.Queja.Remove(queja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuejaExists(int id)
        {
            return (_context.Queja?.Any(e => e.QuejaId == id)).GetValueOrDefault();
        }
    }
}