using AspNetMvc_lesson_app.Models;
using AspNetMvc_lesson_app.Models.Forms;
using AspNetMvc_lesson_app.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvc_lesson_app.Controllers
{
    public class SkillController : Controller
    {
        private readonly SkillService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SkillController(SkillService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var skills = _service.GetAll();
            return View(skills);
        }

        public IActionResult Create()
        {
            var model = new SkillForm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var skill = new Skill();
            form.Update(skill);

            if (form.Logo != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + form.Logo.FileName;
                var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", uniqueFileName);
                using var fs = new FileStream(logoPath, FileMode.Create);
                await form.Logo.CopyToAsync(fs);
                skill.LogoFileName = uniqueFileName;
            }

            skill.Id = _service.GetAll().Count + 1;
            _service.Add(skill);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var skill = _service.FindById(id);
            if (skill == null)
            {
                return NotFound();
            }

            var model = new SkillForm(skill);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SkillForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var skill = _service.FindById(id);
            if (skill == null)
            {
                return NotFound();
            }

            if (form.Logo != null)
            {
                if (!string.IsNullOrEmpty(skill.LogoFileName))
                {
                    var existingLogoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", skill.LogoFileName);
                    if (System.IO.File.Exists(existingLogoPath))
                    {
                        System.IO.File.Delete(existingLogoPath);
                    }
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + form.Logo.FileName;
                var newLogoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", uniqueFileName);
                using var fs = new FileStream(newLogoPath, FileMode.Create);
                await form.Logo.CopyToAsync(fs);
                skill.LogoFileName = uniqueFileName;
            }

            form.Update(skill);
            _service.Update(skill);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var skill = _service.FindById(id);
            if (skill != null)
            {
                if (!string.IsNullOrEmpty(skill.LogoFileName))
                {
                    var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", skill.LogoFileName);
                    if (System.IO.File.Exists(logoPath))
                    {
                        System.IO.File.Delete(logoPath);
                    }
                }
                _service.Remove(skill);
            }

            return RedirectToAction("Index");
        }
    }
}