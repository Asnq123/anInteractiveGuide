using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using anInteractiveGuide.Data;
using anInteractiveGuide.Models;

namespace anInteractiveGuide.Controllers
{
    public class disesController : Controller
    {
        private readonly anInteractiveGuideContext _context;

        public disesController(anInteractiveGuideContext context)
        {
            _context = context;
        }

        // GET: dises
        public async Task<IActionResult> Index()
        {
              return _context.dise != null ? 
                          View(await _context.dise.ToListAsync()) :
                          Problem("Entity set 'anInteractiveGuideContext.dise'  is null.");
        }

        // GET: dises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.dise == null)
            {
                return NotFound();
            }

            var dise = await _context.dise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dise == null)
            {
                return NotFound();
            }

            return View(dise);
        }

        // GET: dises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: dises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,title,info,symp,treat")] dise dise)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                dise.img = filename;
            }

            _context.Add(dise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //_context.Add(dise);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

        }

        // GET: dises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.dise == null)
            {
                return NotFound();
            }

            var dise = await _context.dise.FindAsync(id);
            if (dise == null)
            {
                return NotFound();
            }
            return View(dise);
        }

        // POST: dises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,info,symp,img,treat")] dise dise)
        {
            if (id != dise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!diseExists(dise.Id))
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
            return View(dise);
        }

        // GET: dises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.dise == null)
            {
                return NotFound();
            }

            var dise = await _context.dise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dise == null)
            {
                return NotFound();
            }

            return View(dise);
        }

        // POST: dises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.dise == null)
            {
                return Problem("Entity set 'anInteractiveGuideContext.dise'  is null.");
            }
            var dise = await _context.dise.FindAsync(id);
            if (dise != null)
            {
                _context.dise.Remove(dise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool diseExists(int id)
        {
          return (_context.dise?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
