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
    public class ForumController : Controller
    {
        private readonly DataContext _context;

        public ForumController(DataContext context)
        {
            _context = context;
        }

        // GET: Forum
        public async Task<IActionResult> Index()
        {
            Task<List<TblForum>>? dataContext = _context.TblForums.Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .ToListAsync();
            return View(await dataContext);
        }

        // GET: Forum/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblForums == null)
            {
                return NotFound();
            }

            var tblForum = await _context.TblForums
                .Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblForum == null)
            {
                return NotFound();
            }

            return View(tblForum);
        }

        // GET: Forum/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.TblCategories, "Id", "Id");
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id");
            ViewData["IdParent"] = new SelectList(_context.TblForums, "Id", "Id");
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Descriptions,IdParent,IdInitiator,IdCategory")] TblForum tblForum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblForum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.TblCategories, "Id", "Id", tblForum.IdCategory);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblForum.IdInitiator);
            ViewData["IdParent"] = new SelectList(_context.TblForums, "Id", "Id", tblForum.IdParent);
            return View(tblForum);
        }

        // GET: Forum/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblForums == null)
            {
                return NotFound();
            }

            var tblForum = await _context.TblForums.FindAsync(id);
            if (tblForum == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.TblCategories, "Id", "Id", tblForum.IdCategory);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblForum.IdInitiator);
            ViewData["IdParent"] = new SelectList(_context.TblForums, "Id", "Id", tblForum.IdParent);
            return View(tblForum);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Descriptions,IdParent,IdInitiator,IdCategory")] TblForum tblForum)
        {
            if (id != tblForum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblForumExists(tblForum.Id))
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
            ViewData["IdCategory"] = new SelectList(_context.TblCategories, "Id", "Id", tblForum.IdCategory);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblForum.IdInitiator);
            ViewData["IdParent"] = new SelectList(_context.TblForums, "Id", "Id", tblForum.IdParent);
            return View(tblForum);
        }

        // GET: Forum/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblForums == null)
            {
                return NotFound();
            }

            var tblForum = await _context.TblForums
                .Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblForum == null)
            {
                return NotFound();
            }

            return View(tblForum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblForums == null)
            {
                return Problem("Entity set 'DataContext.TblForums'  is null.");
            }
            var tblForum = await _context.TblForums.FindAsync(id);
            if (tblForum != null)
            {
                _context.TblForums.Remove(tblForum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblForumExists(decimal id)
        {
          return (_context.TblForums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
