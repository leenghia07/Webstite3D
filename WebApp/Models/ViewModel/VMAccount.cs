using System.ComponentModel.DataAnnotations;
using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.ViewModel
{
    public class VMAccount
    {
        public ApplicationUser? Account { get; set; }
        public IEnumerable<IdentityUser>? Accounts { get; set; }
        /*    public string LastName { get; set; }
            public string FisrtName { get; set; }
            }*/
        public string PassWord {get; set;}
        public string PermissionId { get; set; }
        public string Role { get; set; }

        public List<SelectListItem> ListRole { get; set; }
    }
}
