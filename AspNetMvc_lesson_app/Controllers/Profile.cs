using AspNetMvc_lesson_app.Models;

namespace AspNetMvc_lesson_app.Controllers
{
    public class Profile
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public List<Skill> Skills { get; set; }

        public Profile()
        {
            Skills = new List<Skill>();
        }
    }
}