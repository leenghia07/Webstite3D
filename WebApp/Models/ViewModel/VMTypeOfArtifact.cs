using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.Models.ViewModel
{
    public class VMTypeOfArtifact
    {
        [Required]
        public TypeOfArtifact? TypeOfArtifact { get; set; }
        public IEnumerable<TypeOfArtifact>? TypeOfArtifacts { get; set; }
    }
}
