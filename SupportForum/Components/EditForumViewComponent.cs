using Microsoft.AspNetCore.Mvc;
using SupportForum.Models.Data;
using SupportForum.Models.ViewModels;

namespace SupportForum.Components
{
    public class EditForumViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public EditForumViewComponent(DataContext context)
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
