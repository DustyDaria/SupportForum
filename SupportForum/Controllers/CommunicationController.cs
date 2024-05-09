using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SupportForum.Helper;
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
            if (idInitiator == null || idInitiator <= 0)
                return Problem(Error.IncorrectInitiator);

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
            return GetCreateCommunicationVC((decimal)cmnVM.Communication.IdInitiator, 
                cmnVM.IdTopic, cmnVM.Communication.IdParent);
        }

        public async Task<IActionResult> GetEditCommunicationVC(decimal idCmn, decimal idTopic)
        {
            if (_context.TblCommunications == null) return Problem("Entity set 'DataContext.TblCommunications'  is null.");

            var cmn = await _context.TblCommunications.FindAsync(idCmn);
            if (cmn == null) return NotFound();

            return ViewComponent("EditCommunication", new CommunicationViewModel()
            {
                Communication = cmn,
                IdTopic = idTopic
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Communication,IdTopic")] CommunicationViewModel cmnVM)
        {
            if (_context.TblCommunications == null) return Problem(Error.EntityIsNull(_context.TblCommunications));

            if (cmnVM.IdTopic == 0 || cmnVM.Communication.Id == 0) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cmnVM.Communication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCommunicationExists(cmnVM.Communication.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "Topic", new { id = cmnVM.IdTopic });

            }
            ViewData["Errors"] = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y?.Count > 0)
                           .ToList();
            var cmn = await _context.TblCommunications.FindAsync(cmnVM.Communication.Id);
            if (cmn == null) return NotFound();
            return ViewComponent("EditCommunication", new CommunicationViewModel()
            {
                Communication = cmn,
                IdTopic = cmnVM.IdTopic
            });
        }

        private bool TblCommunicationExists(decimal id)
            => (_context.TblCommunications?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
