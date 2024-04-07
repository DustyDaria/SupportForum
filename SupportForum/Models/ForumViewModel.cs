namespace SupportForum.Models
{
    public class ForumViewModel
    {
        public TblCategory Category { get; set; }
        public virtual ICollection<TblForum> Forums { get; set; } = new List<TblForum>();
    }
}
