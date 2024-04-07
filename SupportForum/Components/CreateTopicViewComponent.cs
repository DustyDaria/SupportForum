using Microsoft.AspNetCore.Mvc;
using SupportForum.Models;

namespace SupportForum.Components
{
    public class CreateTopicViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CreateTopicViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(TopicViewModel topicVM)
        {
            ViewData["Errors"] = new List<string>();
            return View(topicVM);
        }
    }
}
