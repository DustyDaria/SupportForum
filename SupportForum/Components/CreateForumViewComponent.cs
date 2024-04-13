using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForum.Models;
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

        public async Task<IViewComponentResult> InvokeAsync(ForumViewModel tblForum)
        {
            ViewData["Errors"] = new List<string>();
            return View(tblForum);
        }
    }
}
