using System.ComponentModel.DataAnnotations;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMMuseum
    {
        [Required]
        public Museum? Museum { get; set; }
        public Guid? MuseumId { get; set; }
        public List<string> NameMuseums { get; set; }
        public List<int> TotalArtifact { get; set; }
        public List<int> TotalExh { get; set; }
        
        public IEnumerable<Museum>? MuseumsSelections { get; set; }

        public IEnumerable<Museum>? Museums { get; set; }
    }
}
