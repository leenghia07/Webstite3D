using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MuseumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
