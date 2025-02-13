using AspNetMvc_lesson_app.Models;
using AspNetMvc_lesson_app.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvc_lesson_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var profile = new Profile
            {
                Name = "Your Name",
                Bio = "Your biography or description.",
                Skills = new List<Skill>
                {
                    new Skill { Title = "C#", Level = 70 },
                    new Skill { Title = "ASP.NET", Level = 70 },
                    new Skill { Title = "SQL", Level = 75 }
                }
            };

            ViewData["Profile"] = profile;
            return View();
        }

        public IActionResult Hello()
        {
            //VievData
            var name = "Mio";
            ViewData["name"] = "Mio";
            ViewData["showText"] = true;


            var skills = new List<Skill>()
            {
                new Skill()
                {
                    Title = "C#",
                    Level = 99,
                },
                new Skill()
                {
                    Title = "C++",
                    Level = 90,
                },
                new Skill()
                {
                    Title = "SQL",
                    Level = 20
                }
            };

            //ViewData[Skill] = skills;

            //ViewBag
            ViewBag.Skills = skills;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
