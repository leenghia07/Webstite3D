using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MuseumDataContext _context;
        public HomeController(ILogger<HomeController> logger, MuseumDataContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Artifact = new VMArtifact();
            Artifact.Museums = await _context.Museum.ToListAsync();
            return View(Artifact);
        }
        public IActionResult ErrorPage()
        {
            ViewData["Message"] = "Bạn không có quyền truy cập,hãy đăng nhập tài khoản";
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}