using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupportForum.Models.Data;

namespace SupportForum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
              return _context.TblCategories != null ? 
                          View(await _context.TblCategories.ToListAsync()) :
                          Problem("Entity set 'DataContext.TblCategories'  is null.");
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblCategories == null)
            {
                return NotFound();
            }

            var tblCategory = await _context.TblCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return View(tblCategory);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Descriptions,IsModeration,IdInitiator,TimeCreate")] TblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCategory);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblCategories == null) return NotFound();

            TblCategory? category = await _context.TblCategories.FindAsync(id);

            return category == null ? NotFound() : View(category);
        }


        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Descriptions,IsModeration")] TblCategory tblCategory)
        {
            if (id != tblCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCategoryExists(tblCategory.Id))
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
            return View(tblCategory);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblCategories == null) return NotFound();
            
            var category = await _context.TblCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            return category == null ? NotFound()  : View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblCategories == null)
            {
                return Problem("Entity set 'DataContext.TblCategories'  is null.");
            }
            var tblCategory = await _context.TblCategories.FindAsync(id);
            if (tblCategory != null)
            {
                _context.TblCategories.Remove(tblCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCategoryExists(decimal id)
        {
          return (_context.TblCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
