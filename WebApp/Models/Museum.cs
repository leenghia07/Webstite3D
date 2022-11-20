using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Museum
    {
        public Guid Id { get; set; }
        public string MuseumName { get; set; }
        [Display(Name = "Ảnh")]
        public string? Image { get; set; }

        public string Address { get; set; }
    }
}
