using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspNetMvc_lesson_app.Models.Forms;

public class UserInfoForms
{
    public UserInfoForms() { }
public UserInfoForms(UserInfo model)
{
    Name = model.Name;
    Email = model.Email;
    Description = model.Description;
    Birthday = model.Birthday;
    IsActive = model.IsActive;
    ExpirienseYears = model.ExpirienseYears;
    Salary = model.Salary;
    if (model.Profession != null)
    {
        ProfessionId = Professions.FindIndex(x => x == model.Profession);
    }
}
    public void Update(UserInfo model)
    {
        model.Name = Name;
        model.Email = Email;
        model.Description = Description;
        model.Birthday = Birthday;
        model.IsActive = IsActive;
        model.ExpirienseYears = ExpirienseYears;
        model.Salary = Salary;
        model.Profession = Professions[ProfessionId ?? 0];
    }
    public int Id { get; set; }

    [DisplayName("Повне ім'я")]
    [Required(ErrorMessage = "Це поле обов'язкове")]
    [MinLength(3, ErrorMessage = "Мінімум 3 символи")]
    [MaxLength(20, ErrorMessage = "Максимум 20 символів")]
    public string Name { get; set; } = null!; [DisplayName("Електронна пошта")]
    [EmailAddress(ErrorMessage = "Введіть валідну електронну пошту")]
    public string? Email { get; set; }
    [DisplayName("Про себе")]
    public string? Description { get; set; }
    [DisplayName("Дата народження")]
    public DateTime Birthday { get; set; }
    [DisplayName("Профіль активний")]
    public bool IsActive { get; set; }
    [DisplayName("Років досвіду")]
    public int ExpirienseYears { get; set; }
    [DisplayName("Очікувана зарплатня")]
    public decimal Salary { get; set; }
    [DisplayName("Професія")]
    [Required(ErrorMessage = "Оберіть з варіантів")]
    public int? ProfessionId { get; set; }
    public List<string> Professions => [
        "Frontend розробник",
            "Backend розробник",
            "Fullstack розробник",
            "Дизайнер",
            "Тестувальник"
        ];
    [DisplayName("Аватарка")]
    public IFormFile? Image { get; set; }

    [DisplayName("Зображення галереї")]
    public ICollection<IFormFile>? Gallery { get; set; }

    public List<Skill>? Skills { get; set; } = [];
}