using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class TypeOfArticleController : Controller
    {
        private readonly MuseumDataContext _context;
        public TypeOfArticleController(MuseumDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            VMTypeOfArticle typeOfArticle = new VMTypeOfArticle();
            var listTypeArticle = await _context.TypeOfArticle.ToListAsync();
            typeOfArticle.TypeOfArticles = listTypeArticle;
            return View(typeOfArticle);
        }
        [HttpPost]

        public async Task<IActionResult> Create(TypeOfArticle typeOfArticle)
        {
            if(ModelState.IsValid)
            {
                _context.Add(typeOfArticle);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["success"] = "Thêm thất bại";
            return View(typeOfArticle);
        }
        [HttpPost]

        public async Task<IActionResult> Update(TypeOfArticle typeOfArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Update(typeOfArticle);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhập thông tin thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["success"] = "Cập nhập thông tin thất bại";
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Delete(Guid id)
        {
            var typeOfArticle = await _context.TypeOfArticle.FindAsync(id);
            _context.Remove(typeOfArticle);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
