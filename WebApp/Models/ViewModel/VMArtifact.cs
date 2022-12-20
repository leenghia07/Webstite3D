using System.ComponentModel.DataAnnotations;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMArtifact
    {
        [Required]
        public Artifact? Artifact { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public Guid? TypeOfArtifact { get; set; }
        public String? FileImage { get; set; }
        public String? File3D { get; set; }
        public Museum? Museum { get; set; }
        public int? SumArtifact { get; set; }
        public int? SumExhibitonRoom { get; set; }
        public IEnumerable<TypeOfArtifact>? TypeOfArtifacts { get; set; }
        public IEnumerable<Museum>? Museums  { get; set; }
        public IEnumerable<Artifact>? ArtifactSearch { get; set; }
        public IEnumerable<Artifact>? Artifacts { get; set; }
        public IEnumerable<ExhibitionRoom>? ExhibitionRooms { get; set; }
      

    }
}
