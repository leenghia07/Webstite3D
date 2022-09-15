using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MuseumDataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(MuseumDataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var Account = new VMAccount();
            /*Account.Accounts = await _context*/
            return View(Account);
        }
    }
}
