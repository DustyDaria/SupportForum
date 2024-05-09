using SupportForum.Models.Data;

namespace SupportForum.Models.ViewModels
{
    public class TopicViewModel
    {
        public TblTopic Topic { get; set; }
        public decimal? IdForumCategory { get; set; }

        public List<TblCommunication> communicationVM { get; set; } = new List<TblCommunication>();  
    }
}
