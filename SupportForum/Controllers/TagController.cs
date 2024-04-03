using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupportForum.Models;

namespace SupportForum.Controllers
{
    public class TagController : Controller
    {
        private readonly DataContext _context;

        public TagController(DataContext context)
        {
            _context = context;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
              return _context.TblTags != null ? 
                          View(await _context.TblTags.ToListAsync()) :
                          Problem("Entity set 'DataContext.TblTags'  is null.");
        }

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblTags == null)
            {
                return NotFound();
            }

            var TblTag = await _context.TblTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (TblTag == null)
            {
                return NotFound();
            }

            return View(TblTag);
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tag")] TblTag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblTags == null)
            {
                return NotFound();
            }

            var TblTag = await _context.TblTags.FindAsync(id);
            if (TblTag == null)
            {
                return NotFound();
            }
            return View(TblTag);
        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Tag")] TblTag TblTag)
        {
            if (id != TblTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(TblTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTagExists(TblTag.Id))
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
            return View(TblTag);
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblTags == null)
            {
                return NotFound();
            }

            var TblTag = await _context.TblTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (TblTag == null)
            {
                return NotFound();
            }

            return View(TblTag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblTags == null)
            {
                return Problem("Entity set 'DataContext.TblTags'  is null.");
            }
            var TblTag = await _context.TblTags.FindAsync(id);
            if (TblTag != null)
            {
                _context.TblTags.Remove(TblTag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTagExists(decimal id)
        {
          return (_context.TblTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
