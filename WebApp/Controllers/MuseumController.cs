using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.Core;

namespace WebApp.Controllers
{
    [Authorize(Roles = Permission.Manager+","+Permission.Employee)]
    public class MuseumController : Controller
    {
        private readonly MuseumDataContext _MuseumDataContext;
        public MuseumController(MuseumDataContext museumDataContext)
        {
            _MuseumDataContext = museumDataContext;
        }
        public async Task<ActionResult> Index()
        {
            var MuseumVM = new VMMuseum();
            MuseumVM.Museums = await _MuseumDataContext.Museum
                            .AsNoTracking()
                            .ToListAsync();
            return View(MuseumVM);
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
            return View(vmArtifact);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Museum museum)
        {
            if (ModelState.IsValid)
            {
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
