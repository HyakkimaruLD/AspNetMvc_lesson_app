using AspNetMvc_lesson_app.Models;
using AspNetMvc_lesson_app.Models.Forms;
using AspNetMvc_lesson_app.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcExample.Controllers;

public class UserInfoController : Controller
{
    private readonly ILogger<UserInfoController> _logger;
    private readonly UserInfoService _service;
    private readonly UserSkill _userSkill;
    private readonly IWebHostEnvironment _webHost;

    public UserInfoController(ILogger<UserInfoController> logger, UserInfoService service, IWebHostEnvironment webHost)
    {
        _logger = logger;
        _service = service;
        _webHost = webHost;
    }

    public IActionResult Index()
    {
        var models = _service.GetAll().ToList();
        return View(models);
    }

    public IActionResult View(int id)
    {
        return View(new UserInfoForms(_service.FindById(id)));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new UserInfoForms(new UserInfo());
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UserInfoForms form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var model = new UserInfo();
        form.Update(model);

        if (form.Image != null)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + form.Image.FileName;
            var filename = Path.Combine(_webHost.WebRootPath, "Uploads", uniqueFileName);
            using var fs = new FileStream(filename, FileMode.Create);
            await form.Image.CopyToAsync(fs);
            model.ImageFile = uniqueFileName;
        }

        if (form.Gallery != null)
        {
            foreach (var item in form.Gallery)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                var filename = Path.Combine(_webHost.WebRootPath, "Uploads", uniqueFileName);
                using var fs = new FileStream(filename, FileMode.Create);
                await item.CopyToAsync(fs);
                model.GalleryFiles.Add(uniqueFileName);
            }
        }

        var random = new Random();
        do
        {
            var id = random.Next(1, 1000);
            if (_service.FindById(id) != null)
            {
                continue;
            }
            model.Id = id;
        } while (model.Id == 0);

        _service.Add(model);
        _service.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewData["id"] = id;
        return View(new UserInfoForms(_service.FindById(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] UserInfoForms forms)
    {
        if (!ModelState.IsValid)
        {
            ViewData["id"] = id;
            return View(forms);
        }

        var savedModel = _service.FindById(id);
        forms.Update(savedModel);

        if (forms.Image != null)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + forms.Image.FileName;
            var filename = Path.Combine(_webHost.WebRootPath, "Uploads", uniqueFileName);
            using var fs = new FileStream(filename, FileMode.Create);
            await forms.Image.CopyToAsync(fs);
            savedModel.ImageFile = uniqueFileName;
        }

        if (forms.Gallery != null)
        {
            foreach (var item in forms.Gallery)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                var filename = Path.Combine(_webHost.WebRootPath, "Uploads", uniqueFileName);
                using var fs = new FileStream(filename, FileMode.Create);
                await item.CopyToAsync(fs);
                savedModel.GalleryFiles.Add(uniqueFileName);
            }
        }

        _service.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var user = _service.FindById(id);
        if (user != null)
        {
            if (!string.IsNullOrEmpty(user.ImageFile))
            {
                var imagePath = Path.Combine(_webHost.WebRootPath, "Uploads", user.ImageFile);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (user.GalleryFiles != null)
            {
                foreach (var galleryFile in user.GalleryFiles)
                {
                    var galleryPath = Path.Combine(_webHost.WebRootPath, "Uploads", galleryFile);
                    if (System.IO.File.Exists(galleryPath))
                    {
                        System.IO.File.Delete(galleryPath);
                    }
                }
            }

            _service.Remove(user);
            _service.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}