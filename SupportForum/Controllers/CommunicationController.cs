using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SupportForum.Models;
using SupportForum.Models.ViewModels;

namespace SupportForum.Controllers
{
    public class CommunicationController : Controller
    {
        private readonly DataContext _context;

        public CommunicationController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetCreateCommunicationVC(decimal idInitiator, decimal idTopic, decimal? idParent = null)
        {
            CommunicationViewModel cmnVM;
            var user = await _context.TblUsers
                .FirstAsync(w => w.Id == idInitiator);
            if (idParent == null)
            {
                cmnVM = new CommunicationViewModel()
                {
                    Communication = new TblCommunication() 
                    { 
                        IdInitiator = idInitiator, 
                        IdInitiatorNavigation = user,
                        IdTopic = idTopic 
                    },
                    IdTopic = idTopic
                };
            }
            else
            {
                cmnVM = new CommunicationViewModel()
                {
                    Communication = new TblCommunication() 
                    { 
                        IdInitiator = idInitiator, 
                        IdInitiatorNavigation = user,
                        IdParent = idParent 
                    },
                    IdTopic = idTopic
                };
            }

            return ViewComponent("CreateCommunication", cmnVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Communication,IdTopic")] CommunicationViewModel cmnVM)
        {
            if (ModelState.IsValid)
            {
                cmnVM.Communication.IdInitiatorNavigation = await _context.TblUsers
                    .Where(w => w.Id == cmnVM.Communication.IdInitiator)
                    .Select(s => s)
                    .FirstAsync();
                _context.Add(cmnVM.Communication);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Topic", new { id = cmnVM.IdTopic });
            }

            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            return await GetCreateCommunicationVC(cmnVM.Communication.IdInitiator, cmnVM.IdTopic, cmnVM.Communication.IdParent);
        }
    }
}
