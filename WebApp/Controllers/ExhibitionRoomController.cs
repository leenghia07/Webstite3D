using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize]
    public class ExhibitionRoomController : Controller
    {
        private readonly MuseumDataContext _context;
        private readonly IWebHostEnvironment _environment;
        public ExhibitionRoomController(MuseumDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _environment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            VMExhibitionRoom exhibitionRoomVM = new VMExhibitionRoom();
            exhibitionRoomVM.ExhibitionRooms = await _context.ExhibitionRoom.OrderByDescending(i => i.Date).ToListAsync();
            exhibitionRoomVM.Museums = await _context.Museum.ToListAsync();
            return View(exhibitionRoomVM);
            }
        public async Task<IActionResult> Edit(Guid Id)
        {
            VMExhibitionRoom exhibitionRoomVM = new VMExhibitionRoom();
            exhibitionRoomVM.ExhibitionRoom = await _context.ExhibitionRoom.SingleOrDefaultAsync(w => w.Id == Id);
            exhibitionRoomVM.Museums = await _context.Museum.ToListAsync();
            return View(exhibitionRoomVM);
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            VMExhibitionRoom exhibitionRoomVM = new VMExhibitionRoom();
            var Room = await _context.ExhibitionRoom.FirstOrDefaultAsync(i => i.Id.Equals(Id));
            if(Room == null)
            {
                TempData["Error"] = "Lỗi! Không có file 3D phòng này";
                return RedirectToAction(nameof(Index));
            }
            exhibitionRoomVM.ExhibitionRoom = Room;
            return View(exhibitionRoomVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExhibitionRoom exhibitionRoom)
        {
            if(ModelState.IsValid)
            {
                string wwwPath = _environment.WebRootPath;
                string ContentPath = _environment.ContentRootPath;
                var File = HttpContext.Request.Form.Files;
                if (File.Count > 0)
                {
                    string PathImage = Path.Combine(wwwPath, "images");
                    if (!Directory.Exists(PathImage))
                    {
                        Directory.CreateDirectory(PathImage);
                    }
                    if (File.Count == 1)
                    {
                        string FileName = Path.GetFileName(File[0].FileName);
                        using (FileStream stream = new FileStream(Path.Combine(PathImage, FileName), FileMode.Create))
                        {
                            File[0].CopyTo(stream);
                        }
                        switch (File[0].Name)
                        {
                            case "FileImage":
                                exhibitionRoom.Image = FileName;
                                exhibitionRoom.File3D = null;
                                break;
                            case "File3D":
                                exhibitionRoom.Image = null;
                                exhibitionRoom.File3D = FileName;
                                break;
                            default:
                                break;
                        }
                    }
                    if (File.Count == 2)
                    {
                        string FileNameImage = Path.GetFileName(File[0].FileName);
                        if (!string.IsNullOrEmpty(FileNameImage))
                        {
                            using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
                            {
                                File[0].CopyTo(stream);
                            }
                            exhibitionRoom.Image = FileNameImage;
                        }
                        else
                        {
                            exhibitionRoom.Image = null;
                        }
                        string Path3D = Path.Combine(wwwPath, "File3D");
                        if (!Directory.Exists(Path3D))
                        {
                            Directory.CreateDirectory(Path3D);
                        }
                        string FileName3D = Path.GetFileName(File[1].FileName);
                        if (!string.IsNullOrEmpty(FileName3D))
                        {
                            using (FileStream stream = new FileStream(Path.Combine(Path3D, FileName3D), FileMode.Create))
                            {
                                File[1].CopyTo(stream);
                            }
                            exhibitionRoom.Image = null;
                            exhibitionRoom.File3D = FileName3D;
                        }
                        else
                        {
                            exhibitionRoom.File3D = null;
                        }
                    }
                }
                else
                {
                    exhibitionRoom.Image = null;
                    exhibitionRoom.File3D = null;
                }
                _context.Add(exhibitionRoom);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Thêm không thành công";
            return View(exhibitionRoom);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ExhibitionRoom exhibitionRoom)
        {
           /* var exhibitionRoom = vmexhibitionRoom.ExhibitionRoom;*/
            var GetExhRoom = await _context.ExhibitionRoom.AsNoTracking()
                                                          .SingleAsync(w => w.Id == exhibitionRoom.Id);
            string wwwPath = _environment.WebRootPath;
            string ContentPath = _environment.ContentRootPath;
            var File = HttpContext.Request.Form.Files;
            if (File.Count > 0)
            {
                string PathImage = Path.Combine(wwwPath, "images");
                if (!Directory.Exists(PathImage))
                {
                    Directory.CreateDirectory(PathImage);
                }
                if (File.Count == 1)
                {
                    string FileName = Path.GetFileName(File[0].FileName);
                    using (FileStream stream = new FileStream(Path.Combine(PathImage, FileName), FileMode.Create))
                    {
                        File[0].CopyTo(stream);
                    }
                    switch (File[0].Name)
                    {
                        case "FileImage":
                            exhibitionRoom.Image = FileName;
                            exhibitionRoom.File3D = GetExhRoom.File3D;
                            break;
                        case "File3D":
                            exhibitionRoom.Image = GetExhRoom.Image;
                            exhibitionRoom.File3D = FileName;
                            break;
                        default:
                            break;
                    }
                }
                if (File.Count == 2)
                {
                    string FileNameImage = Path.GetFileName(File[0].FileName);
                    if (!string.IsNullOrEmpty(FileNameImage))
                    {
                        using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
                        {
                            File[0].CopyTo(stream);
                        }
                        exhibitionRoom.Image = FileNameImage;
                    }
                    else
                    {
                        exhibitionRoom.Image = GetExhRoom.Image;
                    }
                    string Path3D = Path.Combine(wwwPath, "File3D");
                    if (!Directory.Exists(Path3D))
                    {
                        Directory.CreateDirectory(Path3D);
                    }
                    string FileName3D = Path.GetFileName(File[1].FileName);
                    if (!string.IsNullOrEmpty(FileName3D))
                    {
                        using (FileStream stream = new FileStream(Path.Combine(Path3D, FileName3D), FileMode.Create))
                        {
                            File[1].CopyTo(stream);
                        }
                        exhibitionRoom.Image = GetExhRoom.Image;
                        exhibitionRoom.File3D = FileName3D;
                    }
                    else
                    {
                        exhibitionRoom.File3D = GetExhRoom.File3D;
                    }
                }
            }
            else
            {
                exhibitionRoom.Image = GetExhRoom.Image;
                exhibitionRoom.File3D = GetExhRoom.File3D;
                exhibitionRoom.MuseumId = GetExhRoom.MuseumId;
            }
            _context.Update(exhibitionRoom);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhập thành công";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var GetExRoom = await _context.ExhibitionRoom.FindAsync(Id);
            _context.ExhibitionRoom.Remove(GetExRoom);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa phòng trưng bày thành công";
            return RedirectToAction(nameof(Index));
        }

    }
}
