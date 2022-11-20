using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public Guid? ArtifactId { get; set; }
        [ForeignKey("ArtifactId")]
        [Display(Name = "Hiện vật")]
        public virtual Artifact? Artifact { get; set; }
        public Guid? ExhibitionRoomId { get; set; }
        [Display(Name = "Phòng trưng bày")]
        [ForeignKey("ExhibitionRoomId")]
        public virtual ExhibitionRoom? ExhibitionRoom { get; set; }
        public Guid? TypeOfArticleId { get; set; }
        [Display(Name = "Loại bài viết")]
        [ForeignKey("TypeOfArticleId")]
        public virtual TypeOfArticle? TypeOfArticle { get; set; }
        [Display(Name = "Thứ tự")]
        public int Number { get; set; }
        [Display(Name = "Trạng thái")]
        public bool State { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime Date { get; set; }
    }
}
