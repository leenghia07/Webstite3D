namespace WebApp.Models.ViewModel
{
    public class VMRoomArtifact
    {
        public Artifact Artifact { get; set; }
        public IEnumerable<Artifact>? Artifacts { get; set; }
    }
}
