using SupportForum.Models.Data;

namespace SupportForum.Models.ViewModels
{
    public class ForumsTreeViewModel
    {
        public TblCategory Category { get; set; }

        public virtual ICollection<TblForum> Forums { get; set; } 

        public ForumsTreeViewModel(TblCategory category, List<TblForum> forum)
        {
            Category = category;
            Forums = forum ;
        }
    }
}
