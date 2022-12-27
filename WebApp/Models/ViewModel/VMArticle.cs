using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class VMArticle
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public Guid? TypeOfArticle { get; set; }
        [Display(Name = "Bài viết")]
        public Article? Article { get; set; }
        [Display(Name = "Phòng trưng bày")]
        public ExhibitionRoom? ExhibitionRoom { get; set; }
        [Display(Name = "Hiện vật")]
        public Artifact? Artifact { get; set; }

        public IEnumerable<Article>? Articles { get; set; }
        public IEnumerable<TypeOfArticle> TypeOfArticles { get; set; }
        public IEnumerable<ExhibitionRoom> ExhibitionRooms { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
        public string? Type { get; set; }
    }
}
