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
    public class ComentariosController : Controller
    {
        private readonly MZ_MJ_Proyecto1Context _context;

        public ComentariosController(MZ_MJ_Proyecto1Context context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            var mZ_MJ_Proyecto1Context = _context.Comentario.Include(c => c.Habitacion);
            return View(await mZ_MJ_Proyecto1Context.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comentario == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario
                .Include(c => c.Habitacion)
                .FirstOrDefaultAsync(m => m.ComentarioId == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion, "HabitacionId", "Descripcion");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComentarioId,Texto,HabitacionId")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion, "HabitacionId", "Descripcion", comentario.HabitacionId);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comentario == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion, "HabitacionId", "Descripcion", comentario.HabitacionId);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComentarioId,Texto,HabitacionId")] Comentario comentario)
        {
            if (id != comentario.ComentarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.ComentarioId))
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
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion, "HabitacionId", "Descripcion", comentario.HabitacionId);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comentario == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario
                .Include(c => c.Habitacion)
                .FirstOrDefaultAsync(m => m.ComentarioId == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comentario == null)
            {
                return Problem("Entity set 'MZ_MJ_Proyecto1Context.Comentario'  is null.");
            }
            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentario.Remove(comentario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioExists(int id)
        {
          return (_context.Comentario?.Any(e => e.ComentarioId == id)).GetValueOrDefault();
        }
    }
}
