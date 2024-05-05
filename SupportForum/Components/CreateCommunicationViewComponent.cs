using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportForum.Models.ViewModels;
using SupportForum.Models;

namespace SupportForum.Components
{
    public class CreateCommunicationViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CreateCommunicationViewComponent(DataContext context)
        {
            _context = context;
        }
    
        public async Task<IViewComponentResult> InvokeAsync(CommunicationViewModel cmnVM)
        {
            ViewData["Errors"] = new List<string>();
            return View(cmnVM);
        }
    }
}
