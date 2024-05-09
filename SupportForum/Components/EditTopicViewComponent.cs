using Microsoft.AspNetCore.Mvc;
using SupportForum.Models.ViewModels;
using SupportForum.Models.Data;

namespace SupportForum.Components
{
    public class EditTopicViewComponent : ViewComponent
    {

        private readonly DataContext _context;

        public EditTopicViewComponent(DataContext context)
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
