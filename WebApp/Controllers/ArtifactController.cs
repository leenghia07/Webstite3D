using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ArtifactController : Controller
    {
        private readonly MuseumDataContext _context;
        private readonly IWebHostEnvironment Environment;
        public ArtifactController(MuseumDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.Environment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var Artifact = new VMArtifact();
            Artifact.Artifacts = await _context.Aritifact
                                               .Include(i =>i.Museum)
                                               .Include(i => i.TypeOfArtifact)
                                               .AsNoTracking()
                                               .ToListAsync();
            return View(Artifact);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Artifact artifact, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string fileNameImage = Path.GetFileName(File.FileName);
                /*string fileName3D = Path.GetFileName(File.FileName);*/

                string path = Path.Combine(this.Environment.WebRootPath, "Images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                _context.Add(artifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                ViewBag.message = "thêm thành công";
            }
            ViewBag.messageError = "thêm thất bại";
            return View(artifact);
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
            if (EditArtifact == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(EditArtifact);
        }

        [HttpPost]
        public async Task<IActionResult> AddArtifact(Artifact artifact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var error = "Thêm thất bại";
                ModelState.AddModelError("", "Thêm không thành công");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArtifact(Artifact artifact)
        {
            if (ModelState.IsValid)
            {
                _context.Update(artifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Artifact = await _context.Aritifact.FindAsync(id);
            _context.Aritifact.Remove(Artifact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
