using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class VMUser
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy:0}")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }

        public string SDT { get; set; }
        public string PerMission { get; set; }

    }
}
