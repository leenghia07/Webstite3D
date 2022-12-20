using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApp.Models
{
    public class ExhibitionRoom
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Chưa nhập tên phòng")]
        [Display(Name = "Tên phòng trưng bày")]
        public string NameRoom { get; set; }
        [Display(Name = "Ảnh")]
        public string? Image { get; set; }
        public Guid MuseumId { get; set; }
        [ForeignKey("MuseumId")]
        [Display(Name = "Bảo tàng")]
        public virtual Museum Museum { get; set; }
        [Display(Name = "File 3D")]
        public string? File3D { get; set; }
        [Display(Name = "Nội dung")]
      /*  [Required(ErrorMessage = "Chưa nhập mô tả phòng")]*/
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy:0}")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng")]
        [Required(ErrorMessage ="Chưa chọn ngày")]
        public DateTime Date { get; set; }
    }
}
