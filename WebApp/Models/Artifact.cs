using WebApp.Models;
namespace WebApp.Models
{
    public class Artifact
    {
        public Guid Id { get; set; }
        public virtual Museum Museum { get; set; }
        public virtual TypeOfArtifact TypeOfArtifact { get; set; }
        public string NameArtifact { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? File3D { get; set; }
    }
}
