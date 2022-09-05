using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("~/threejs/editor/index.html");
        }
    }
}
