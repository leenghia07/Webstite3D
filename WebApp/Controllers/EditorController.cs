using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using WebApp.Core;
namespace WebApp.Controllers
{
    [Authorize(Roles = Permission.Manager + "," + Permission.Employee)]
    public class EditorController : Controller
    {
        private readonly MuseumDataContext _context;
        public EditorController(MuseumDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var DetailArtifact = new VMArtifact();
            DetailArtifact.Artifact = await _context.Aritifact.FindAsync(id);

            return View(DetailArtifact);
        }
    }
}
