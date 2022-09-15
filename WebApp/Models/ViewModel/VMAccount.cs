using System.ComponentModel.DataAnnotations;
using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace WebApp.Models.ViewModel
{
    public class VMAccount
    {
        public IdentityUser? Account { get; set; }
        public IEnumerable<IdentityUser>? Accounts { get; set; }
        public string LastName { get; set; }
        public string FisrtName { get; set; }

    }
}
