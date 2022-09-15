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
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var MuseumVM = new VMMuseum();
            MuseumVM.Museum = await _MuseumDataContext.Museum.FindAsync(id);
            if (MuseumVM == null)
            {
                return NotFound();
            }
            return View(MuseumVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Museum museum)
        {
            if (ModelState.IsValid)
            {
                _MuseumDataContext.Add(museum);
                await _MuseumDataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                ViewBag.message = "Thêm thành công";
            }
            else
            {
                ViewBag.messageError = "Thêm thất bại";
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
                return RedirectToAction(nameof(Index));
                ViewBag.message = "Cập nhập thành công";
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
            ViewBag.message = "Xóa thành công";
        }
    }
}
