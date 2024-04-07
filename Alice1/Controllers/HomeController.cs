using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alice1.Controllers
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

        [HttpGet("/skill")]
        //public Skill GetSkill()
        //{
        //    //return new Skill() { Email = "victor_k02@mail.ru", Name = "VictorBot", Id = 1 };
                
        //}
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
