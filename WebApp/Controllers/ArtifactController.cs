using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ArtifactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
