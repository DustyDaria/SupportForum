using Microsoft.AspNetCore.Mvc;
using SupportForum.Models.ViewModels;
using SupportForum.Models.Data;

namespace SupportForum.Components
{
    public class EditCommunicationViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public EditCommunicationViewComponent(DataContext context)
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
