using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMTypeOfArticle
    {
        public TypeOfArticle TypeOfArticle { get; set; }
        public IEnumerable<TypeOfArticle> TypeOfArticles { get; set; }
    }

}
