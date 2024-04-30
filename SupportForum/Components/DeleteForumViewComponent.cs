using Microsoft.AspNetCore.Mvc;
using SupportForum.Models.ViewModels;
using SupportForum.Models;

namespace SupportForum.Components
{
    public class DeleteForumViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public DeleteForumViewComponent(DataContext context)
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
