using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace AspNetMvc_lesson_app.Models.Forms
{
    public class SkillForm
    {
        public SkillForm() { }

        public SkillForm(Skill skill)
        {
            Id = skill.Id;
            Title = skill.Title;
            Level = skill.Level;
            Color = skill.Color;
            ExistingLogoFileName = skill.LogoFileName;
        }

        public void Update(Skill skill)
        {
            skill.Title = Title;
            skill.Level = Level;
            skill.Color = Color;
            if (Logo != null)
            {
                skill.LogoFileName = Logo.FileName;
            }
        }

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

        [DisplayName("Логотип")]
        public IFormFile? Logo { get; set; } 

        public string? ExistingLogoFileName { get; set; }
    }
}