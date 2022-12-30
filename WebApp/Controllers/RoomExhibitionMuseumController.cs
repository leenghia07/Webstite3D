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
                                        .Include(i => i.Museum)
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
        public async Task<IActionResult> Search(VMRoomExhibitionMuseum vmExhRoom)
        {
            VMRoomExhibitionMuseum ExhRoom = new VMRoomExhibitionMuseum();
            string data = vmExhRoom.Year + '/' + vmExhRoom.Month + '/' + 01;
            DateTime myDate = DateTime.Parse(data);
            var Month = new DateTime(myDate.Year, myDate.Month, 01);
            var FistDatOfMonth = Month.ToString("yyyy/MM/dd");
            var lastDayOfMonth = Month.AddMonths(1).AddSeconds(-1).ToString("yyyy/MM/dd");
            DateTime FistDate = DateTime.Parse(FistDatOfMonth);
            DateTime LastDate = DateTime.Parse(lastDayOfMonth);
            var ExhRooms = _context.ExhibitionRoom.Where(i => i.Date >= FistDate)
                                              .Where(t => t.Date <= LastDate)
                                              .OrderByDescending(i => i.Date);
            ExhRoom.Month = vmExhRoom.Month;
            ExhRoom.Year = vmExhRoom.Year;
            ExhRoom.ExhibitionRooms = ExhRooms;
            return View(ExhRoom);
        }
    }
}
