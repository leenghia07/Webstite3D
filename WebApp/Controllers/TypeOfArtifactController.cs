using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
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

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TypeOfArtifact typeofartifact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeofartifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Thêm không thành công");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TypeOfArtifact typeofartifact)
        {
            if (ModelState.IsValid)
            {
                _context.Update(typeofartifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var TypeOfArtifact = await _context.TypeOfArtifact.FindAsync(id);
            _context.Remove(TypeOfArtifact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
