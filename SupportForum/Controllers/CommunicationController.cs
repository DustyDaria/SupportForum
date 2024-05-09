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

        public IActionResult GetCreateCommunicationVC(decimal? idInitiator, decimal idTopic, decimal? idParent = null)
        {
            if (idInitiator == null || idInitiator <= 0) return NotFound();

            CommunicationViewModel cmnVM;
            if (idParent == null)
            {
                cmnVM = new CommunicationViewModel()
                {
                    Communication = new TblCommunication() 
                    { 
                        IdInitiator = idInitiator, 
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
                _context.Add(cmnVM.Communication);
                await _context.SaveChangesAsync();
                
                cmnVM.Communication.CmnPath = _context.TblCommunications
                    .Where(w => w.Id == cmnVM.Communication.IdParent)
                    .Select(s => s.CmnPath).FirstOrDefault() + $"{cmnVM.Communication.Id}/";
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Topic", new { id = cmnVM.IdTopic });
            }

            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            return GetCreateCommunicationVC(cmnVM.Communication.IdInitiator == null ? 0 : (decimal)cmnVM.Communication.IdInitiator, 
                cmnVM.IdTopic, cmnVM.Communication.IdParent);
        }
    }
}
