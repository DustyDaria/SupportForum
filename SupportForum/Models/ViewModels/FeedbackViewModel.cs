using System.ComponentModel.DataAnnotations;

namespace SupportForum.Models.ViewModels
{
    public class FeedbackViewModel
    {
        public FeedbackType type { get; set; }


        [StringLength(1000)]
        public string? message { get; set; }
    }

    public enum FeedbackType
    {
        [Display(Name = "Замечание")]
        remark,
        [Display(Name = "Предложение")]
        proposal,
        [Display(Name = "Обнаружена ошибка")]
        error,
        [Display(Name = "Неверные данные")]
        incorrectData,
        [Display(Name = "Другое")]
        another
    }
}
