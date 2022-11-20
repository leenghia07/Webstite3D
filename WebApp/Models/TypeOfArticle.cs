using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class TypeOfArticle
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên loại bài viết")]
        [Display(Name = "Tên loại bài viết")]
        public string NameType { get; set; }
    }
}
