using WebApp.Models;

namespace WebApp.Models.ViewModel
{
    public class VMArtifactRoom
    {
        public IEnumerable<ExhibitionRoom> ExhibitionRooms { get; set; }
        public IEnumerable<Museum> Museums { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
    }
}
