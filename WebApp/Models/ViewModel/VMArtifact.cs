using System.ComponentModel.DataAnnotations;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMArtifact
    {
        [Required]
        public Artifact? Artifact { get; set; }
        public IEnumerable<Artifact>? Artifacts { get; set; }
    }
}
