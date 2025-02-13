﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetMvc_lesson_app.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }

        public string? Description { get; set; }

        public DateTime Birthday { get; set; }

        public bool IsActive { get; set; }

        public int ExpirienseYears { get; set; }

        public decimal Salary { get; set; }

        public string Profession { get; set; }

        public List<Skill>? Skills { get; set; } = [];
        public string? ImageFile { get; set; }
        public List<string>? GalleryFiles { get; set; } = [];

    }
}
