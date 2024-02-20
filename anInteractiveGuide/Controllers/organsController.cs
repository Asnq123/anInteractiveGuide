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
    public class organsController : Controller
    {
        private readonly anInteractiveGuideContext _context;

        public organsController(anInteractiveGuideContext context)
        {
            _context = context;
        }

        // GET: organs
        public async Task<IActionResult> Index()
        {
              return _context.organs != null ? 
                          View(await _context.organs.ToListAsync()) :
                          Problem("Entity set 'anInteractiveGuideContext.organs'  is null.");
        }

        // GET: organs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.organs == null)
            {
                return NotFound();
            }

            var organs = await _context.organs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organs == null)
            {
                return NotFound();
            }

            return View(organs);
        }

        // GET: organs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: organs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, IFormFile file1, [Bind("Id,title,info,poss_dis,video")] organs organs)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                organs.img = filename;
            }

            


            if (file1 != null)
            {
                string filename = file1.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file1.CopyToAsync(filestream); }

                organs.video = filename;
            }

            _context.Add(organs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // _context.Add(organs);
            // await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));

        }

        // GET: organs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.organs == null)
            {
                return NotFound();
            }

            var organs = await _context.organs.FindAsync(id);
            if (organs == null)
            {
                return NotFound();
            }
            return View(organs);
        }

        // POST: organs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,info,poss_dis,img,video")] organs organs)
        {
            if (id != organs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!organsExists(organs.Id))
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
            return View(organs);
        }

        // GET: organs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.organs == null)
            {
                return NotFound();
            }

            var organs = await _context.organs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organs == null)
            {
                return NotFound();
            }

            return View(organs);
        }

        // POST: organs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.organs == null)
            {
                return Problem("Entity set 'anInteractiveGuideContext.organs'  is null.");
            }
            var organs = await _context.organs.FindAsync(id);
            if (organs != null)
            {
                _context.organs.Remove(organs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool organsExists(int id)
        {
          return (_context.organs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
