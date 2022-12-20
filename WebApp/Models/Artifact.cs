using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models;
namespace WebApp.Models
{
    public class Artifact
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Chưa chọn bảo tàng")]
        public Guid MuseumId { get; set; }
        [ForeignKey("MuseumId")]
        [Display(Name = "Bảo tàng")]
        public virtual Museum Museum { get; set; }
        public Guid TypeOfArtifactId { get; set; }

        /*[Required(ErrorMessage = "Chưa chọn loại hiện vật")]*/
        [Display(Name = "Loại bảo hiện vật")]
        [ForeignKey("TypeOfArtifactId")]
        public virtual TypeOfArtifact TypeOfArtifact { get; set; }
        /*[Required(ErrorMessage = "Chưa nhập tên hiện vật")]*/
        [Display(Name = "Tên hiện vật")]
        public string NameArtifact { get; set; }
        [Display(Name = "File ảnh")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Chưa nhập mô tả")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "File 3D")]
        public string? File3D { get; set; }
        [Display(Name = "Ngày phát hiện")]
        public DateTime DiscoveryDate { get; set; }

        public static implicit operator Artifact(int v)
        {
            throw new NotImplementedException();
        }
    }
}
