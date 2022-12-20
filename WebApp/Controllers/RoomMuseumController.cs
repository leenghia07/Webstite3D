using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class RoomMuseumController : Controller
    {
        private readonly MuseumDataContext _Context;
        public RoomMuseumController(MuseumDataContext Context)
        {
            _Context = Context;
        }
        public async Task<ActionResult> Index()
        {

            var MuseumVM = new VMMuseum();
            var Museums = await _Context.Museum
                            .AsNoTracking()
                            .ToListAsync();
            MuseumVM.Museums = Museums;
            MuseumVM.MuseumsSelections = Museums;
            return View(MuseumVM);
        }
        public IActionResult Detail(Guid Id)
        {
            var vmArtifact = new VMArtifact();
            var Artifacts= _Context.Aritifact.Where(x => x.MuseumId.Equals(Id))
                                                     .Include(i => i.Museum)
                                                     .Include(i => i.TypeOfArtifact)
                                                     .ToList();
            var Museum = _Context.Museum.FirstOrDefault(w => w.Id.Equals(Id));
            var ExhibitionRooms= _Context.ExhibitionRoom.Where(x => x.MuseumId.Equals(Id))
                                                     .ToList();
            vmArtifact.Artifacts = Artifacts;
            vmArtifact.Museum = Museum;
            vmArtifact.ExhibitionRooms = ExhibitionRooms;
            return View(vmArtifact);
        }
        public async Task<IActionResult> Search(VMMuseum vmMuseum)
        {
            VMMuseum Museum = new VMMuseum();
            var Museums = _Context.Museum.Where(i => i.Id == vmMuseum.MuseumId);

            Museum.MuseumId = vmMuseum.MuseumId;
            Museum.Museums = Museums;
            Museum.MuseumsSelections = await _Context.Museum.ToListAsync();
            return View(Museum);
        }
    }
}
