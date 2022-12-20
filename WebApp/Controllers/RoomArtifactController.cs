using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var ListArtifact = await _context.Aritifact.OrderByDescending(i => i.DiscoveryDate).ToListAsync();
            var ListType = await _context.TypeOfArtifact.ToListAsync();
            roomArtifact.Artifacts = ListArtifact;
            roomArtifact.TypeOfArtifacts = ListType;
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
        public async Task<IActionResult> Search(VMRoomArtifact vmRoomArtifact)
        {
            VMRoomArtifact artifact = new VMRoomArtifact();
            string data = vmRoomArtifact.Year + '/' + vmRoomArtifact.Month + '/' + 01;
            DateTime myDate = DateTime.Parse(data);
            var Month = new DateTime(myDate.Year, myDate.Month, 01);
            var FistDatOfMonth = Month.ToString("yyyy/MM/dd");
            var lastDayOfMonth = Month.AddMonths(1).AddSeconds(-1).ToString("yyyy/MM/dd");
            DateTime FistDate = DateTime.Parse(FistDatOfMonth);
            DateTime LastDate = DateTime.Parse(lastDayOfMonth);
            var Artifact = _context.Aritifact.Where(i => i.DiscoveryDate >= FistDate)
                                              .Where(t => t.DiscoveryDate <= LastDate)
                                              .Where(y => y.TypeOfArtifactId == vmRoomArtifact.TypeOfArtifact)
                                              .OrderByDescending(i => i.DiscoveryDate);
            artifact.Month = vmRoomArtifact.Month;
            artifact.Year = vmRoomArtifact.Year;
            artifact.Artifacts = Artifact;
            artifact.TypeOfArtifact = vmRoomArtifact.TypeOfArtifact;
            artifact.TypeOfArtifacts = _context.TypeOfArtifact.ToList();
            return View(artifact);
        }
    }
}
