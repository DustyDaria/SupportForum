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
    public class KeyWordController : Controller
    {
        private readonly DataContext _context;

        public KeyWordController(DataContext context)
        {
            _context = context;
        }

        // GET: KeyWord
        public async Task<IActionResult> Index()
        {
              return _context.TblKeyWords != null ? 
                          View(await _context.TblKeyWords.ToListAsync()) :
                          Problem("Entity set 'DataContext.TblKeyWords'  is null.");
        }

        // GET: KeyWord/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblKeyWords == null)
            {
                return NotFound();
            }

            var tblKeyWord = await _context.TblKeyWords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblKeyWord == null)
            {
                return NotFound();
            }

            return View(tblKeyWord);
        }

        // GET: KeyWord/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KeyWord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KeyWord")] TblKeyWord tblKeyWord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblKeyWord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblKeyWord);
        }

        // GET: KeyWord/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblKeyWords == null)
            {
                return NotFound();
            }

            var tblKeyWord = await _context.TblKeyWords.FindAsync(id);
            if (tblKeyWord == null)
            {
                return NotFound();
            }
            return View(tblKeyWord);
        }

        // POST: KeyWord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,KeyWord")] TblKeyWord tblKeyWord)
        {
            if (id != tblKeyWord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblKeyWord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblKeyWordExists(tblKeyWord.Id))
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
            return View(tblKeyWord);
        }

        // GET: KeyWord/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblKeyWords == null)
            {
                return NotFound();
            }

            var tblKeyWord = await _context.TblKeyWords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblKeyWord == null)
            {
                return NotFound();
            }

            return View(tblKeyWord);
        }

        // POST: KeyWord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblKeyWords == null)
            {
                return Problem("Entity set 'DataContext.TblKeyWords'  is null.");
            }
            var tblKeyWord = await _context.TblKeyWords.FindAsync(id);
            if (tblKeyWord != null)
            {
                _context.TblKeyWords.Remove(tblKeyWord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblKeyWordExists(decimal id)
        {
          return (_context.TblKeyWords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
