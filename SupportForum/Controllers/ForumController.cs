using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SupportForum.Models;
using SupportForum.Models.ViewModels;

namespace SupportForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly DataContext _context;

        public ForumController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: All Forums by category
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(decimal? idCategory)
        {
            var forums = _context.TblForums
                .Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .Include(t => t.TblTopics)
                .Where(w => w.IdCategory == idCategory || w.IdCategory == null)
                .ToList();
            var category = _context.TblCategories
                    .Where(w => w.Id == idCategory)
                    .First();

            ForumsTreeViewModel forumVM = new ForumsTreeViewModel(category, forums);

            return View(forumVM);
        }

        // GET: Forum/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (_context.TblForums == null) return Problem("Entity set 'DataContext.TblForums'  is null.");

            if (id == null) return NotFound();

            var tblForum = await _context.TblForums
                .Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .Include(t => t.TblTopics)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblForum == null) return NotFound();

            return View(tblForum);
        }

        public IActionResult GetCreateForumVC(decimal idInitiator, decimal idCategory, decimal? idParent = null)
        {
            ForumViewModel forumVM;
            if(idParent == null)
            {
                forumVM = new ForumViewModel()
                {
                    Forum = new TblForum() { IdInitiator = idInitiator, IdCategory = idCategory },
                    IdForumCategory = idCategory
                };
            }
            else
            {
                forumVM = new ForumViewModel()
                {
                    Forum = new TblForum() { IdInitiator = idInitiator, IdParent = idParent },
                    IdForumCategory = idCategory
                };
            }

            return ViewComponent("CreateForum", forumVM);
        }

        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Forum,IdForumCategory")] ForumViewModel forumVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumVM.Forum);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { idCategory = forumVM.IdForumCategory });
            }

            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            return GetCreateForumVC(forumVM.Forum.IdInitiator, forumVM.IdForumCategory, forumVM.Forum.IdParent);
        }

        public async Task<IActionResult> GetEditForumVC(decimal idForum, decimal idForumCat)
        {
            if (_context.TblForums == null) return Problem("Entity set 'DataContext.TblForums'  is null.");

            var forum = await _context.TblForums.FindAsync(idForum);
            if(forum == null) return NotFound();

            return ViewComponent("EditForum", new ForumViewModel()
            {
                Forum = forum,
                IdForumCategory = idForumCat
            });
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdForumCategory,Forum")] ForumViewModel forumVM)
        {
            if (_context.TblForums == null) return Problem("Entity set 'DataContext.TblForums'  is null.");

            if (forumVM.IdForumCategory == 0 || forumVM.Forum.Id == 0) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumVM.Forum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblForumExists(forumVM.Forum.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index", new { idCategory = forumVM.IdForumCategory });
            }

            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            var forum = await _context.TblForums.FindAsync(forumVM.Forum.Id);
            if (forum == null) return NotFound();
            return ViewComponent("EditForum", new ForumViewModel()
            {
                Forum = forum,
                IdForumCategory = forumVM.IdForumCategory
            });
        }

        public async Task<IActionResult> GetDeleteForumVC(decimal idForum, decimal idForumCat)
        {
            if (_context.TblForums == null) return Problem("Entity set 'DataContext.TblForums'  is null.");

            var forum = await _context.TblForums
                .Include(t => t.IdCategoryNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.IdParentNavigation)
                .FirstOrDefaultAsync(m => m.Id == idForum);
            if (forum == null) return NotFound();

            return ViewComponent("DeleteForum", new ForumViewModel()
            {
                Forum = forum,
                IdForumCategory = idForumCat
            });
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("IdForumCategory,Forum")] ForumViewModel forumVM)
        {
            if (_context.TblForums == null) return Problem("Entity set 'DataContext.TblForums'  is null.");
            if (forumVM.IdForumCategory == 0 || forumVM.Forum.Id == 0) return NotFound();

            try
            {
                if (_context.Database.ExecuteSql($"dbo.sp_delForumTree @idForumNode = {forumVM.Forum.Id}") > 0)
                    return RedirectToAction("Index", new { idCategory = forumVM.IdForumCategory });
                else
                {
                    ViewData["Errors"] = "Ошибка при удалении данных";
                    return ViewComponent("DeleteForum", forumVM);
                }
            }
            catch (Exception exc)
            {
                ViewData["Errors"] = exc.Message;
                return ViewComponent("DeleteForum", forumVM);
            }
        }

        private bool TblForumExists(decimal id) 
            => (_context.TblForums?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
