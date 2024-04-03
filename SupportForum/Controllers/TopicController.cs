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
    public class TopicController : Controller
    {
        private readonly DataContext _context;

        public TopicController(DataContext context)
        {
            _context = context;
        }

        // GET: Topic
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.TblTopics
                .Include(t => t.IdForumNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdTags)
                .Include(t => t.TblCommunications);
            return View(await dataContext.ToListAsync());
        }

        // GET: Topic/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblTopics == null)
            {
                return NotFound();
            }

            var tblTopic = await _context.TblTopics
                .Include(t => t.IdForumNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdTags)
                .Include(t => t.TblCommunications)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblTopic == null)
            {
                return NotFound();
            }

            return View(tblTopic);
        }

        // GET: Topic/Create
        public IActionResult Create()
        {
            ViewData["IdForum"] = new SelectList(_context.TblForums, "Id", "Id");
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id");
            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Question,IsAnonymous,IdForum,IdInitiator")] TblTopic tblTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdForum"] = new SelectList(_context.TblForums, "Id", "Id", tblTopic.IdForum);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblTopic.IdInitiator);
            return View(tblTopic);
        }

        // GET: Topic/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.TblTopics == null)
            {
                return NotFound();
            }

            var tblTopic = await _context.TblTopics.FindAsync(id);
            if (tblTopic == null)
            {
                return NotFound();
            }
            ViewData["IdForum"] = new SelectList(_context.TblForums, "Id", "Id", tblTopic.IdForum);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblTopic.IdInitiator);
            return View(tblTopic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Question,IsAnonymous,IdForum,IdInitiator")] TblTopic tblTopic)
        {
            if (id != tblTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTopicExists(tblTopic.Id))
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
            ViewData["IdForum"] = new SelectList(_context.TblForums, "Id", "Id", tblTopic.IdForum);
            ViewData["IdInitiator"] = new SelectList(_context.TblUsers, "Id", "Id", tblTopic.IdInitiator);
            return View(tblTopic);
        }

        // GET: Topic/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.TblTopics == null)
            {
                return NotFound();
            }

            var tblTopic = await _context.TblTopics
                .Include(t => t.IdForumNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblTopic == null)
            {
                return NotFound();
            }

            return View(tblTopic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.TblTopics == null)
            {
                return Problem("Entity set 'DataContext.TblTopics'  is null.");
            }
            var tblTopic = await _context.TblTopics.FindAsync(id);
            if (tblTopic != null)
            {
                _context.TblTopics.Remove(tblTopic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTopicExists(decimal id)
        {
          return (_context.TblTopics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
