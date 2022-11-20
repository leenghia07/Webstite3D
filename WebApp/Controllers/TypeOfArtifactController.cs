using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using WebApp.Core;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = Permission.Manager + "," + Permission.Employee)]
    public class TypeOfArtifactController : Controller
    {
        private readonly MuseumDataContext _context;
        public TypeOfArtifactController(MuseumDataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var typeArtifactVM = new VMTypeOfArtifact();
            typeArtifactVM.TypeOfArtifacts = await _context.TypeOfArtifact
                            .AsNoTracking()
                            .ToListAsync();
            return View(typeArtifactVM);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            var vmArtifact = new VMArtifact();
            vmArtifact.Artifacts = _context.Aritifact.Where(x => x.TypeOfArtifactId.Equals(Id))
                                                     .Include(i => i.Museum)
                                                     .Include(i => i.TypeOfArtifact)
                                                     .ToList();
            return View(vmArtifact);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TypeOfArtifact typeofartifact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeofartifact);
                await _context.SaveChangesAsync();
                ViewData["success"]= "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["error"]= "Thêm thất bại";
            }
            return View(typeofartifact);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TypeOfArtifact typeofartifact)
        {
            if (ModelState.IsValid)
            {
                _context.Update(typeofartifact);
                await _context.SaveChangesAsync();
                ViewData["success"] = "Cập nhập thông tin thành công";
                return RedirectToAction(nameof(Index));
            }
            ViewData["success"] = "Cập nhập thông tin thất bại";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var TypeOfArtifact = await _context.TypeOfArtifact.FindAsync(id);
            _context.Remove(TypeOfArtifact);
            await _context.SaveChangesAsync();
            ViewData["success"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
