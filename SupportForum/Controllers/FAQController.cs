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
    public class FAQController : Controller
    {
        private readonly DataContext _context;

        public FAQController(DataContext context)
        {
            _context = context;
        }

        // GET: FAQ
        public async Task<IActionResult> Index()
        {
              return _context.TblInstructions != null ? 
                          View(await _context.TblInstructions
                            .Where(w => w.IsShort != true)
                            .ToListAsync()) :
                          Problem("Entity set 'DataContext.TblInstructions'  is null.");
        }

        // GET: FAQ/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblInstructions == null)
            {
                return NotFound();
            }

            var tblInstruction = await _context.TblInstructions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblInstruction == null)
            {
                return NotFound();
            }

            return View(tblInstruction);
        }

        // GET: FAQ/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQ/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Instruction,IsShort,Entity")] TblInstruction tblInstruction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInstruction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInstruction);
        }

        // GET: FAQ/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblInstructions == null)
            {
                return NotFound();
            }

            var tblInstruction = await _context.TblInstructions.FindAsync(id);
            if (tblInstruction == null)
            {
                return NotFound();
            }
            return View(tblInstruction);
        }

        // POST: FAQ/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Instruction,IsShort,Entity")] TblInstruction tblInstruction)
        {
            if (id != tblInstruction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInstruction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInstructionExists(tblInstruction.Id))
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
            return View(tblInstruction);
        }

        // GET: FAQ/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblInstructions == null)
            {
                return NotFound();
            }

            var tblInstruction = await _context.TblInstructions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblInstruction == null)
            {
                return NotFound();
            }

            return View(tblInstruction);
        }

        // POST: FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblInstructions == null)
            {
                return Problem("Entity set 'DataContext.TblInstructions'  is null.");
            }
            var tblInstruction = await _context.TblInstructions.FindAsync(id);
            if (tblInstruction != null)
            {
                _context.TblInstructions.Remove(tblInstruction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInstructionExists(decimal id)
        {
          return (_context.TblInstructions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
