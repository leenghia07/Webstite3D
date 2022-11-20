using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class RoomExhibitionMuseumController : Controller
    {
        private readonly MuseumDataContext _context;
        public RoomExhibitionMuseumController(MuseumDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            VMRoomExhibitionMuseum roomEM=new VMRoomExhibitionMuseum();
            var roomEMs = await _context.ExhibitionRoom
                                        .AsNoTracking()
                                        .ToListAsync();
            roomEM.ExhibitionRooms = roomEMs;
            return View(roomEM);
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            VMRoomExhibitionMuseum roomEM=new VMRoomExhibitionMuseum();
            var roomExhibitionMuseum = await _context.ExhibitionRoom.SingleAsync(i=> i.Id.Equals(Id));
            roomEM.ExhibitionRoom = roomExhibitionMuseum;
            return View(roomEM);
        }
    }
}
