using System.Text.Json;

namespace AspNetMvc_lesson_app.Models.Services
{
    public class SkillService
    {
        private string skillDataFile = "skills.json";
        private List<Skill> Skills { get; set; } = new List<Skill>();

        public SkillService()
        {
            Load();
        }

        private void Load()
        {
            if (File.Exists(skillDataFile))
            {
                Skills = JsonSerializer.Deserialize<List<Skill>>(File.ReadAllText(skillDataFile)) ?? new List<Skill>();
            }
        }

        public List<Skill> GetAll()
        {
            return Skills;
        }

        public Skill? FindById(int id)
        {
            return Skills.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Skill skill)
        {
            Skills.Add(skill);
            SaveChanges();
        }

        public void Update(Skill skill)
        {
            var existingSkill = FindById(skill.Id);
            if (existingSkill != null)
            {
                existingSkill.Title = skill.Title;
                existingSkill.Level = skill.Level;
                existingSkill.Color = skill.Color;
                SaveChanges();
            }
        }

        public void Remove(Skill skill)
        {
            Skills.Remove(skill);
            SaveChanges();
        }

        private void SaveChanges()
        {
            File.WriteAllText(skillDataFile, JsonSerializer.Serialize(Skills));
        }
    }
}
