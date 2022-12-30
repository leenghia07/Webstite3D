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
            var Artifact = _context.Aritifact.SingleOrDefault(i => i.Id == id);
            DetailArtifact.Artifact = Artifact;
            return View(DetailArtifact);
        }
        public async Task<IActionResult> EditRoom(Guid id)
        {
            var ExhRoom = new VMExhibitionRoom();
            var Exh = _context.ExhibitionRoom.SingleOrDefault(i => i.Id == id);
            ExhRoom.ExhibitionRoom = Exh;
            return View(ExhRoom);
        }
    }
}
