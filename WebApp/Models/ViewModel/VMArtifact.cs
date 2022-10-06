using System.ComponentModel.DataAnnotations;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMArtifact
    {
        [Required]
        public Artifact? Artifact { get; set; }
        public String FileImage { get; set; }
        public String File3D { get; set; }
        public Museum? Museum { get; set; }

        public IEnumerable<Artifact>? Artifacts { get; set; }
        public IEnumerable<TypeOfArtifact>? TypeOfArtifacts { get; set; }
        public IEnumerable<Museum>? Museums  { get; set; }

    }
}
