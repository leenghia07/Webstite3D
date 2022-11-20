using System.ComponentModel.DataAnnotations;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMMuseum
    {
        [Required]
        public Museum? Museum { get; set; }
        public string test{ get; set; }

        public IEnumerable<Museum>? Museums { get; set; }
    }
}
