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
                                             .OrderBy(i => i.Number)
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
            var articles = await _context.Article.ToListAsync();
            var check =  _context.Article.SingleOrDefault(i => i.Number == article.Number && i.TypeOfArticleId == article.TypeOfArticleId);
            if(check != null)
            {
                TempData["Error"] = "vị trí này đã có bài viết";
            }
            else
            {
                article.State = true;
                _context.Add(article);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(VMArticle vmArticle)
        {
            int numberArticleAfter = vmArticle.Article.Number;
            var getTypeArticle =await _context.TypeOfArticle.FindAsync(vmArticle.Article.TypeOfArticleId);
            var articleBefore = await _context.Article.FindAsync(vmArticle.Article.Id);

            int numberArticleBefore = articleBefore.Number;
            var check = _context.Article.SingleOrDefault(i => i.Number == numberArticleAfter && i.TypeOfArticleId == vmArticle.Article.TypeOfArticleId);
            var articleArtifact = _context.Article.SingleOrDefault(i => i.Number == numberArticleAfter && i.TypeOfArticleId == vmArticle.Article.TypeOfArticleId);
            var articleNow = await _context.Article.FindAsync(vmArticle.Article.Id);
            if(articleArtifact != null)
            {
                if (getTypeArticle.Id == vmArticle.Article.TypeOfArticleId)
                {

                    if (vmArticle.Article.ArtifactId != null)
                    {
                        articleArtifact.Number = numberArticleBefore;
                        articleNow.Number = numberArticleAfter;
                    }
                    else
                    {
                        articleArtifact.Number = numberArticleBefore;
                        articleNow.Number = numberArticleAfter;
                    }
                    articleNow.State = vmArticle.Article.State;
                }
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhập thành công";
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var Article = await _context.Article.FindAsync(Id);
            _context.Remove(Article);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }
      /*  public async Task<IActionResult> Search(VMArticle vmArticle)
        {
            return
        }*/

    }
}
