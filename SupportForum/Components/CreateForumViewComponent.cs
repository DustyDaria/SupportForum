using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForum.Models.Data;
using SupportForum.Models.ViewModels;

namespace SupportForum.ViewComponents
{
    public class CreateForumViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CreateForumViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(ForumViewModel forumVM)
        {
            ViewData["Errors"] = new List<string>();
            return View(forumVM);
        }
    }
}
