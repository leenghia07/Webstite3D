using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
namespace WebApp.Controllers
{
    public class HomeUserController : Controller
    {
        private readonly MuseumDataContext _context;
       
        public HomeUserController(MuseumDataContext context )
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
            article.Articles = listArticle;
            return View(article);
        }
    }
}
