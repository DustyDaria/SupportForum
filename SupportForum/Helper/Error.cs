using SupportForum.Models;

namespace SupportForum.Helper
{
    public class Error
    {
        public static readonly string IncorrectInitiator = "Некорректная передача инициатора. ";
        
        public static string EntityIsNull<T>(T entity)
        {
            return $"Entity set '{entity?.GetType()}'  is null.";
        }
    }
}
