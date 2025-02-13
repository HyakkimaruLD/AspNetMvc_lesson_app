using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AspNetMvc_lesson_app.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [DisplayName("Назва навички")]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Title { get; set; } = null!;

        [DisplayName("Рівень")]
        [Range(1, 10, ErrorMessage = "Рівень повинен бути від 1 до 10")]
        public int Level { get; set; }

        [DisplayName("Колір")]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Color { get; set; } = "#ffffff";

        [DisplayName("Логове зображення")]
        public string? LogoFileName { get; set; }
    }
}