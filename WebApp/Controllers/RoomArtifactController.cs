using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class RoomArtifactController : Controller
    {
        private readonly MuseumDataContext _context;
        public RoomArtifactController(MuseumDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            VMRoomArtifact roomArtifact = new VMRoomArtifact();
            var ListArtifact = await _context.Aritifact.ToListAsync();
            roomArtifact.Artifacts = ListArtifact;
            return View(roomArtifact);
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            VMRoomArtifact roomArtifact = new VMRoomArtifact();
            var detailArtifact = await _context.Aritifact
                                                .Include(i => i.Museum)
                                                .Include(i => i.TypeOfArtifact)
                                                .SingleAsync(i => i.Id.Equals(Id));
            roomArtifact.Artifact = detailArtifact;
            return View(roomArtifact);
        }
    }
}
