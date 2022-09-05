using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("~/threejs/editor/index.html");
        }
    }
}
