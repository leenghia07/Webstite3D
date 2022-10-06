using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
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
                ViewData["success"]= "Thêm thành công";
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                ViewData["error"]="thêm thất bại";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update( Museum museum)
        {
            if(ModelState.IsValid)
            {
                _MuseumDataContext.Update(museum);
                await _MuseumDataContext.SaveChangesAsync();
                ViewData["success"]= "Cập nhập thành công";
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, bool? saveChangesError = false)
        {
            var Museum = await _MuseumDataContext.Museum.FindAsync(id);
            _MuseumDataContext.Museum.Remove(Museum);
            await _MuseumDataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["success"]= "Xóa thành công";
        }
    }
}
