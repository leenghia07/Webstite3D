namespace WebApp.Models.ViewModel
{
    public class VMRoomArtifact
    {
        public Artifact Artifact { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public Guid? TypeOfArtifact { get; set; }
        public IEnumerable<Artifact>? Artifacts { get; set; }
        public IEnumerable<TypeOfArtifact>? TypeOfArtifacts { get; set; }
    }
}
