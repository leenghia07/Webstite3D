using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace WebApp.Controllers
{
    [Authorize(Roles = Permission.Manager+","+Permission.Employee)]
    public class MuseumController : Controller
    {
        private readonly MuseumDataContext _MuseumDataContext;
        private readonly IWebHostEnvironment _environment;
        public MuseumController(MuseumDataContext museumDataContext, IWebHostEnvironment environment)
        {
            _MuseumDataContext = museumDataContext;
            _environment = environment;
        }
        public async Task<ActionResult> Index()
        {
            
            var MuseumVM = new VMMuseum();  
            var Museums = await _MuseumDataContext.Museum
                            .AsNoTracking()
                            .ToListAsync();
            MuseumVM.Museums = Museums;
            return View(MuseumVM);
        }
        [HttpGet]
        public IActionResult getChart()
        {
            var NameMuseumList = new List<string>();
            var NumberArtifactList = new List<int>();
            var NumberExhList = new List<int>();
            var MuseumVM = new VMMuseum();
            var Museums = _MuseumDataContext.Museum
                        .ToList();
            foreach (var item in Museums)
            {
                NameMuseumList.Add(item.MuseumName);
                NumberArtifactList.Add(TotalArtifact(item.MuseumName));
                NumberExhList.Add(TotalExh(item.MuseumName));
            }
            MuseumVM.NameMuseums = NameMuseumList;
            MuseumVM.TotalArtifact = NumberArtifactList;
            MuseumVM.TotalExh = NumberExhList;
            return Json(MuseumVM);
        }
        public int TotalExh(string name)
        {
            var MuseumExh = _MuseumDataContext.ExhibitionRoom.Where(i => i.Museum.MuseumName == name);
            int Total = MuseumExh.Count();
            return Total;
        }
        public int TotalArtifact(string name)
        {
            var Museums = _MuseumDataContext.Museum.Where( i =>i.MuseumName.Equals(name));
            int Total = Museums.Count();
            return Total;
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var vmArtifact = new VMArtifact();
            vmArtifact.Artifacts = _MuseumDataContext.Aritifact.Where(x => x.MuseumId.Equals(Id))
                                                     .Include(i => i.Museum)
                                                     .Include(i => i.TypeOfArtifact)
                                                     .ToList();
            vmArtifact.Museum =  _MuseumDataContext.Museum
                                                    .FirstOrDefault(w => w.Id.Equals(Id));
            vmArtifact.ExhibitionRooms = _MuseumDataContext.ExhibitionRoom.Where(x => x.MuseumId.Equals(Id))
                                                     .ToList();
            return View(vmArtifact);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Museum museum)
        {
            string wwwPath = _environment.WebRootPath;
            string ContentPath = _environment.ContentRootPath;
            var File = HttpContext.Request.Form.Files;

            string PathImage = Path.Combine(wwwPath, "images");
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(PathImage))
                {
                    Directory.CreateDirectory(PathImage);
                }
                string FileNameImage = Path.GetFileName(File[0].FileName);
                using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
                {
                    File[0].CopyTo(stream);
                }
                museum.Image = FileNameImage;
                _MuseumDataContext.Add(museum);
                await _MuseumDataContext.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));   
            }
            return View(museum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update( Museum museum)
        {
            if(ModelState.IsValid)
            {
                _MuseumDataContext.Update(museum);
                await _MuseumDataContext.SaveChangesAsync();
                TempData["success"]= "Cập nhập thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(museum);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Museum = await _MuseumDataContext.Museum.FindAsync(id);
            _MuseumDataContext.Museum.Remove(Museum);
            await _MuseumDataContext.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
