using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Core;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MuseumDataContext _context;
        private readonly UserDatacontext _usercontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(MuseumDataContext context, RoleManager<IdentityRole> roleManager, UserDatacontext usercontext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _usercontext = usercontext;

        }
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> Index()
        {
            List<VMUser> modellist = new List<VMUser>();
            if (_userManager.Users.ToList().Count != 0)
            {
                _userManager.Users.ToList().ForEach(async u =>
                {
                    /*var roles = await _userManager.GetRolesAsync(u);*/
                    VMUser model = new VMUser();
                    model.Id = u.Id;
                    model.Image = u.Image;
                    model.FirstName = u.FisrtName;
                    model.LastName = u.LastName;
                    model.BirthDay = u.BirthDate;
                    model.Email = u.Email;
                    model.Sex = u.Sex;
                    model.SDT = u.PhoneNumber;
                    /*model.PerMission = string.Join(",", roles.ToList());*/
                    model.PerMission = string.Join(",",_userManager.GetRolesAsync(u).Result);
                    modellist.Add(model);
                });
                int total = await _userManager.Users.CountAsync();
                int totalMen = await _userManager.Users.Where(x=> x.Sex.Equals("Nam")).CountAsync();
                int totalWomen = await _userManager.Users.Where(x => x.Sex.Equals("Nữ")).CountAsync();
                ViewData["total"] = total;
                ViewData["totalMen"] = totalMen;
                ViewData["totalWomen"] = totalWomen;

            }
            return View(modellist);
        }
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> Detail(string Id)
        {
            VMAccount user = new VMAccount();
            var userInfo = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == Id);
            user.Account = userInfo;
            var roleUser = await _userManager.GetRolesAsync(userInfo);
            user.Role = @roleUser[0].ToString();
            if (user.Account == null) return View("Error");
            return View(user);
        }
        public async Task<IActionResult> ProFile()  
        {
            return View();
        }
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> Create()   
        {
            var listRole = _roleManager.Roles.ToList();
            ViewData["Role"] = listRole;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> CreateInFo(VMAccount vmAccount)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            Guid guid = Guid.NewGuid();
            applicationUser.Id = guid.ToString();
            applicationUser.FisrtName = vmAccount.Account.FisrtName;
            applicationUser.Image = vmAccount.Account.Image;
            applicationUser.LastName = vmAccount.Account.LastName;
            applicationUser.BirthDate = vmAccount.Account.BirthDate;
            applicationUser.PhoneNumber = vmAccount.Account.PhoneNumber;
            applicationUser.PlaceOfBirth = vmAccount.Account.PlaceOfBirth;
            applicationUser.Address = vmAccount.Account.Address;
            applicationUser.Sex = vmAccount.Account.Sex;
            applicationUser.Email = vmAccount.Account.Email;
            applicationUser.UserName = vmAccount.Account.UserName;
            applicationUser.NormalizedUserName = vmAccount.Account.Email;
            applicationUser.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(applicationUser, vmAccount.Account.PasswordHash);
            var role = await _roleManager.FindByIdAsync(vmAccount.PermissionId);
            if (result.Succeeded)
            {
                var addRole = await _userManager.AddToRoleAsync(applicationUser, role.Name);
                if(addRole.Succeeded)
                {
                    TempData["success"] = "Thêm thành công";
                   return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Thêm không thành công";
            }
            return View(vmAccount.Account);
        }
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> Edit(string Id)
        {
            VMAccount vmAccount = new VMAccount();
            var UserInFo = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == Id);
            var role = await _userManager.GetRolesAsync(UserInFo);
            var listRole = _roleManager.Roles.ToList();
            vmAccount.Account = UserInFo;
            vmAccount.ListRole = new List<SelectListItem>();
            foreach(var item in listRole)
            {
                vmAccount.ListRole.Add(new SelectListItem { Text = item.Name, Value = item.Id });
                foreach(var item2 in vmAccount.ListRole)
                {
                    if(role != null)
                    {
                        if (item2.Text == role[0].ToString())
                        {
                            item2.Selected = true;
                            break;
                        }
                    }
                }
            }
            if (UserInFo == null) return View("Error");
            return View(vmAccount);
        }
        public async Task<IActionResult> ChangePass(string Id)
        {
            return View();
        }
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> UpdateUser(VMAccount user)
        {

            var applicationUser = await _userManager.FindByIdAsync(user.Account.Id);
            if (applicationUser == null)
            {
                TempData["Error"] = "Không tìm thấy user";
                return View(user);
            }

            var roleUser = await _userManager.GetRolesAsync(applicationUser);
            if (roleUser != null)
            {
                await _userManager.RemoveFromRolesAsync(applicationUser, roleUser);
            }
         

            applicationUser.FisrtName = user.Account.FisrtName;
            applicationUser.LastName = user.Account.LastName;
            applicationUser.BirthDate = user.Account.BirthDate;
            applicationUser.PhoneNumber = user.Account.PhoneNumber;
            applicationUser.Sex = user.Account.Sex; 
            applicationUser.Email = user.Account.Email;
            applicationUser.NormalizedEmail = user.Account.Email;
            applicationUser.UserName = user.Account.Email;
            applicationUser.Address = user.Account.Address;
            applicationUser.PlaceOfBirth = user.Account.PlaceOfBirth;

            
            var role = await _roleManager.FindByIdAsync(user.PermissionId);

            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
            if (result.Succeeded)
            {
                var addRole = await _userManager.AddToRoleAsync(applicationUser, role.Name);
                if (addRole.Succeeded)
                {
                    TempData["success"] = "Cập nhập thông tin thành công";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewData["Error"] = "Lỗi";
            }
            return View(user.Account);
        }
        [HttpPost]
        [Authorize(Roles = Permission.Manager)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["success"] = "Xóa nhân viên thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Xóa nhân viên thất bại";

                }
            }
            else
            {
                TempData["Error"] = "Lỗi";
            }
            return View("Index", _userManager.Users);
        }
        
    }
}
