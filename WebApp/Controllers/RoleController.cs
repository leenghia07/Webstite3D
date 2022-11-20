using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core;
namespace WebApp.Controllers
{
    [Authorize(Roles = Permission.Manager)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        public async Task<IActionResult> CreatePerMission(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            TempData["success"] = "Thêm quyền thành công";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var role= _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            return View(role);
        }
        public async Task<IActionResult> Update(IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                IdentityRole roleEdit= _roleManager.Roles.Where(r => r.Id == role.Id).FirstOrDefault();
                roleEdit.Name = role.Name;
                IdentityResult result = await _roleManager.UpdateAsync(roleEdit);
                if (result.Succeeded)
                {
                    TempData["success"] = "Cập nhập quyền thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Error"] = "Lỗi";
                }
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole roleDelete = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (roleDelete != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(roleDelete);
                if (result.Succeeded)
                {
                    TempData["success"] = "Cập nhập quyền thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Error"] = "Lỗi";
                }    
            }
            return View("Index", roleDelete);
        }
    }
}
