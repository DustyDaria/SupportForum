using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupportForum.Helper;
using SupportForum.Models;
using SupportForum.Models.ViewModels;

namespace SupportForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly DataContext _context;

        public TopicController(DataContext context)
        {
            _context = context;
        }

        // GET: Topic/Details/id
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.TblTopics == null) return NotFound();
            
            var tblTopic = await _context.TblTopics
                .Include(t => t.IdForumNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .Include(t => t.TblCommunications)
                .Include(t => t.IdTags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblTopic == null) return NotFound();

            var topicVM = new TopicViewModel()
            {
                Topic = tblTopic,
                IdForumCategory = null
            };

            foreach(var item in tblTopic.TblCommunications)
            {
                topicVM.communicationVM.AddRange(await _context.TblCommunications
                    .Where(w => w.CmnPath.Contains(item.CmnPath))
                    .Include(t => t.IdParentNavigation)
                    .Include(t => t.InverseIdParentNavigation)
                    //.Include(t => t.IdInitiatorNavigation)
                    .ToListAsync());
            }

            return View(topicVM);
        }

        public IActionResult GetCreateTopicVC(decimal? idInitiator, decimal idForum, decimal idForumCat)
        {
            if (idInitiator == null || idInitiator <= 0)
                return Problem(Error.IncorrectInitiator);
            var topic = new TblTopic()
            {
                IdInitiator = idInitiator,
                IdForum = idForum
            }; 

            return ViewComponent("CreateTopic", new TopicViewModel()
            {
                Topic = topic,
                IdForumCategory = idForumCat
            });
        }

        // POST: Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdForumCategory,Topic")] TopicViewModel topicVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicVM.Topic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Forum", new { idCategory = topicVM.IdForumCategory });
            }
            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                            .Where(y => y?.Count > 0)
                            .ToList();
            return ViewComponent("CreateTopic", new
            {
                idInitiator = topicVM.Topic.IdInitiator,
                idForum = topicVM.Topic.IdForum
            });
        }

        public async Task<IActionResult> GetEditTopicVC(decimal idTopic, decimal idForumCat)
        {
            if (_context.TblTopics == null) return Problem("Entity set 'DataContext.TblTopics'  is null.");

            var topic = await _context.TblTopics.FindAsync(idTopic);
            if (topic == null) return NotFound();

            return ViewComponent("EditTopic", new TopicViewModel()
            {
                Topic = topic,
                IdForumCategory = idForumCat
            });
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdForumCategory,Topic")] TopicViewModel topicVM)
        {
            if (_context.TblTopics == null) return Problem("Entity set 'DataContext.TblTopics'  is null.");

            if (topicVM.IdForumCategory == 0 || topicVM.Topic.Id == 0) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicVM.Topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTopicExists(topicVM.Topic.Id)) return NotFound();
                    else  throw;
                }
                return RedirectToAction("Index", "Forum", new { idCategory = topicVM.IdForumCategory });

            }
            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            var topic = await _context.TblTopics.FindAsync(topicVM.Topic.Id);
            if (topic == null) return NotFound();
            return ViewComponent("EditTopic", new TopicViewModel()
            {
                Topic = topic,
                IdForumCategory = topicVM.IdForumCategory
            });
        }

        public async Task<IActionResult> GetDeleteTopicVC(decimal idTopic, decimal idForumCat)
        {
            if (_context.TblTopics == null) return Problem("Entity set 'DataContext.TblTopics'  is null.");

            var topic = await _context.TblTopics
                .Include(t => t.IdForumNavigation)
                .Include(t => t.IdInitiatorNavigation)
                .FirstOrDefaultAsync(m => m.Id == idTopic);
            if (topic == null) return NotFound();

            return ViewComponent("DeleteTopic", new TopicViewModel()
            {
                Topic = topic,
                IdForumCategory = idForumCat
            });
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("IdForumCategory,Topic")] TopicViewModel topicVM)
        {
            if (_context.TblTopics == null) return Problem("Entity set 'DataContext.TblTopics'  is null.");
            if (topicVM.IdForumCategory == 0 || topicVM.Topic.Id == 0) return NotFound();

            try
            {
                // В начале удалим все сообщения
                if (await _context.Database.ExecuteSqlAsync($"dbo.sp_GetCommunicationByTopicForDel @idTopicNode = {topicVM.Topic.Id}") > 0)
                {
                    var tblTopic = await _context.TblTopics.FindAsync(topicVM.Topic.Id);
                    if (tblTopic != null)
                    {
                        _context.TblTopics.Remove(tblTopic);
                    }
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Forum", new { idCategory = topicVM.IdForumCategory });
                }   
                else
                {
                    ViewData["Errors"] = "Ошибка при удалении данных";
                    return Problem("Ошибка при удалении данных");
                }
            }
            catch (Exception exc)
            {
                ViewData["Errors"] = exc.Message;
                return Problem(exc.Message);
            }
        }

        private bool TblTopicExists(decimal id)
            => (_context.TblTopics?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
