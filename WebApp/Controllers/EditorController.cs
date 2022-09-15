using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
namespace WebApp.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        private readonly MuseumDataContext _context;
        public EditorController(MuseumDataContext context)
        {
            _context = context;
        }

        /*  public async Task<IActionResult> Index(Guid id)
          {
              var DetailArtifact = new VMArtifact();
              DetailArtifact.Artifact = await _context.Aritifact.FindAsync(id);

              return Redirect("~/threejs/editor/index.html");
          }*/
        public async Task<IActionResult> Index(Guid id)
        {
            /*var DetailArtifact = new VMArtifact();
            DetailArtifact.Artifact = await _context.Aritifact.FindAsync(id);

            return View(DetailArtifact);*/
            return View();
        }
    }
}
