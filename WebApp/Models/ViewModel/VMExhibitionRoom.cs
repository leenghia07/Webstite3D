using System.ComponentModel.DataAnnotations;
using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
namespace WebApp.Models.ViewModel
{
    public class VMExhibitionRoom
    {
        public ExhibitionRoom ExhibitionRoom  { get; set; }
        public IEnumerable<ExhibitionRoom> ExhibitionRooms { get; set; }

    }
}
