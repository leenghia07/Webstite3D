using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Areas.Identity.Data;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MuseumDataContext _context;
        private readonly UserDatacontext _usercontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(MuseumDataContext context, UserDatacontext usercontext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _usercontext = usercontext;
        }

        public async Task<IActionResult> Index()
        {
            return View(_userManager.Users);
        }
        public async Task<IActionResult> Detail(string Id)
        {
            var UserInFo = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == Id);
            if (UserInFo == null) return View("Error");
            return View(UserInFo);
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInFo(ApplicationUser user)
        {
            
            return View();
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var UserInFo = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == Id);
            if (UserInFo == null) return View("Error");
            return View(UserInFo);
        }
        public async Task<IActionResult> ChangePass(string Id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser user)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);
            if (applicationUser == null)
            {
                ViewData["Error"] = "Không tìm thấy user";
                return View(user);
            }

            applicationUser.FisrtName = user.FisrtName;
            applicationUser.LastName = user.LastName;
            applicationUser.BirthDate = user.BirthDate;
            applicationUser.PhoneNumber = user.PhoneNumber;
            applicationUser.Sex = user.Sex;
            applicationUser.Email = user.Email;
            applicationUser.Address = user.Address;
            applicationUser.PlaceOfBirth = user.PlaceOfBirth;


            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                ViewData["Error"] = "Lỗi";
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ViewBag["Error"] = "Lỗi";
            }
            else
            {
                ViewBag["Error"] = "Lỗi";
            }
            return View("Index", _userManager.Users);
        }
    }
}
