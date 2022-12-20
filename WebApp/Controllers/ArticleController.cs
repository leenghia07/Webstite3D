using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly MuseumDataContext _context;
        public ArticleController(MuseumDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            VMArticle article = new VMArticle();
            var listArticle = await _context.Article
                                             .Include(i => i.Artifact)
                                             .Include(i => i.ExhibitionRoom)
                                             .Include(i => i.TypeOfArticle)
                                            .ToListAsync();
            var listTypeArticles= await _context.TypeOfArticle.ToListAsync();
            var listArtifacts= await _context.Aritifact.ToListAsync();
            var listExhibitionRooms= await _context.ExhibitionRoom.ToListAsync();
            article.Artifacts = listArtifacts;
            article.ExhibitionRooms = listExhibitionRooms;
            article.Articles = listArticle;
            article.TypeOfArticles = listTypeArticles;
            return View(article);
        }
        public async Task<IActionResult> Create(Article article)
        {
            
            article.State = true;
            _context.Add(article);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thêm thành công";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(VMArticle article)
        {
          /*  if (article.Type == "1")
            {

            }
            if (article.Type == "2")
            {

            }
            if (article.Type == "3")
            {

            }*/
            return View(article);
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var Article = await _context.Article.FindAsync(Id);
            _context.Remove(Article);
            await _context.SaveChangesAsync();
            ViewData["success"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(VMArticle vmArticle)
        {
            return
        }

    }
}
