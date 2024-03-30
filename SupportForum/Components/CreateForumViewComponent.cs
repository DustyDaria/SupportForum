using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForum.Models;

namespace SupportForum.ViewComponents
{
    public class CreateForumViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CreateForumViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(decimal idInitiator, 
            decimal? idCategory, decimal? idParent)
        {
            ViewData["Errors"] = new List<string>();
            return View(new TblForum() { 
                IdInitiator = idInitiator, 
                IdCategory = idCategory, 
                IdParent = idParent });
        }

    }
}
