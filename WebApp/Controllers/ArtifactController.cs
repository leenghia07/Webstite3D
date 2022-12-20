using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebApp.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class ArtifactController : Controller
    {
        private readonly MuseumDataContext _context;
        private readonly IWebHostEnvironment _environment;
        public ArtifactController(MuseumDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _environment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var Artifact = new VMArtifact();
            Artifact.Artifacts = await _context.Aritifact
                                               .Include(i => i.Museum)
                                               .Include(i => i.TypeOfArtifact)
                                               .AsNoTracking()
                                               .OrderByDescending(i => i.DiscoveryDate)
                                               .ToListAsync();
            Artifact.Museums = await _context.Museum.ToListAsync();
            Artifact.TypeOfArtifacts = await _context.TypeOfArtifact.ToListAsync();
            return View(Artifact);
        }

        public async Task<IActionResult> Create()
        {
            var Artifact = new VMArtifact();
            Artifact.Museums = await _context.Museum.ToListAsync();
            Artifact.TypeOfArtifacts = await _context.TypeOfArtifact.ToListAsync();
            return View(Artifact);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var DetailArtifact= new VMArtifact();
            DetailArtifact.Artifact= await _context.Aritifact
                                                   .Include(i => i.Museum)
                                                   .Include(i => i.TypeOfArtifact)
                                                   .SingleOrDefaultAsync(w => w.Id == id);

            if (DetailArtifact == null)
            {
                TempData["error"] = "Lỗi";
                return RedirectToAction(nameof(Index));
            }
            return View(DetailArtifact);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var EditArtifact = new VMArtifact();
            EditArtifact.Artifact = await _context.Aritifact
                                                   .Include(i => i.Museum)
                                                   .Include(i => i.TypeOfArtifact)
                                                   .SingleOrDefaultAsync(w => w.Id == id);
            EditArtifact.Museums = await _context.Museum.ToListAsync();
            EditArtifact.TypeOfArtifacts = await _context.TypeOfArtifact.ToListAsync();
            if (EditArtifact == null)
            {
                TempData["error"] = "Lỗi";
                return RedirectToAction(nameof(Index));
            }
            return View(EditArtifact);
        }
        public async Task<IActionResult> createArtifact(Artifact artifact)
        {
             string wwwPath = _environment.WebRootPath;
             string ContentPath = _environment.ContentRootPath;
             var File = HttpContext.Request.Form.Files;
            
             string PathImage = Path.Combine(wwwPath, "images");
             if (!Directory.Exists(PathImage))
             {
                 Directory.CreateDirectory(PathImage);
             }
             string FileNameImage = Path.GetFileName(File[0].FileName);
             using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
             {
                File[0].CopyTo(stream);
             }

            string Path3D = Path.Combine(wwwPath, "File3D");
            if (!Directory.Exists(Path3D))
            {
                Directory.CreateDirectory(Path3D);
            }
            if (File[1] != null)
            {
                string FileName3D = Path.GetFileName(File[1].FileName);
                using (FileStream stream = new FileStream(Path.Combine(Path3D, FileName3D), FileMode.Create))
                {
                    File[1].CopyTo(stream);
                }
                artifact.File3D = FileName3D;

            }
            else
            {
                artifact.File3D = null;
            }
            artifact.Image = FileNameImage;
            _context.Add(artifact);
             await _context.SaveChangesAsync();
            TempData["success"] = "Thêm thành công";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArtifact(Artifact artifact)
        {
            var GetArtifact= await _context.Aritifact.AsNoTracking()
                                                     .SingleOrDefaultAsync(w => w.Id == artifact.Id);
            string wwwPath = _environment.WebRootPath;
            string ContentPath = _environment.ContentRootPath;
            var File = HttpContext.Request.Form.Files;
            if(File.Count > 0)
            {
                string PathImage = Path.Combine(wwwPath, "images");
                if (!Directory.Exists(PathImage))
                {
                    Directory.CreateDirectory(PathImage);
                }
                if(File.Count == 1)
                {
                    string FileNameImage = Path.GetFileName(File[0].FileName);
                    using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
                    {
                        File[0].CopyTo(stream);
                    }
                    artifact.Image = FileNameImage;
                    artifact.File3D = GetArtifact.File3D;
                }
                if(File.Count == 2)
                {
                    string FileNameImage = Path.GetFileName(File[0].FileName);
                    if (!string.IsNullOrEmpty(FileNameImage))
                    {
                        using (FileStream stream = new FileStream(Path.Combine(PathImage, FileNameImage), FileMode.Create))
                        {
                            File[0].CopyTo(stream);
                        }
                        artifact.Image = FileNameImage;
                    }
                    else
                    {
                        artifact.Image = GetArtifact.Image;
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
                        artifact.Image = GetArtifact.Image;
                        artifact.File3D = FileName3D;
                    }
                    else
                    {
                        artifact.File3D = GetArtifact.File3D;
                    }
                }
            }
            else
            {
                artifact.Image = GetArtifact.Image;
                artifact.File3D = GetArtifact.File3D;
            }

            _context.Update(artifact);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhập thành công";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Artifact = await _context.Aritifact.FindAsync(id);
            _context.Aritifact.Remove(Artifact);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa hiện vật thành công";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Search(VMArtifact vmArtifact)
        {
            VMArtifact artifact = new VMArtifact();
            string data = vmArtifact.Year + '/' + vmArtifact.Month + '/' + 01;
            DateTime myDate = DateTime.Parse(data);
            var Month = new DateTime(myDate.Year,myDate.Month,01);
            var FistDatOfMonth = Month.ToString("yyyy/MM/dd");
            var lastDayOfMonth = Month.AddMonths(1).AddSeconds(-1).ToString("yyyy/MM/dd");
            DateTime FistDate = DateTime.Parse(FistDatOfMonth);
            DateTime LastDate = DateTime.Parse(lastDayOfMonth);
            var Artifact =  _context.Aritifact.Where(i => i.DiscoveryDate >= FistDate)
                                              .Where(t => t.DiscoveryDate <= LastDate)
                                              .Where(y => y.TypeOfArtifactId == vmArtifact.TypeOfArtifact)
                                              .OrderByDescending(i => i.DiscoveryDate);
            artifact.Month = vmArtifact.Month;
            artifact.Year = vmArtifact.Year;
            artifact.Artifacts = Artifact;
            artifact.TypeOfArtifact = vmArtifact.TypeOfArtifact;
            artifact.TypeOfArtifacts = _context.TypeOfArtifact.ToList();
            return View(artifact);
        }
    }
}
