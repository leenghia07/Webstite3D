namespace WebApp.Models.ViewModel
{
    public class VMRoomExhibitionMuseum
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public IEnumerable<Museum>? Museums { get; set; }
        public ExhibitionRoom ExhibitionRoom { get; set; }
        
        public IEnumerable<ExhibitionRoom> ExhibitionRooms { get; set; }

    }
}
