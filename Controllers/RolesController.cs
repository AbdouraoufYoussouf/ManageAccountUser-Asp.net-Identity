using LoginRegisterUser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterUser.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    //[Authorize(Roles ="Manager")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> AddRole(RoleAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",await _roleManager.Roles.ToListAsync());
            }
            if(await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", $"{model.Name} is Exist ,Enter our");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
            await _roleManager.CreateAsync(new IdentityRole(model.Name));
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string RoleName){
            var role = await _roleManager.FindByNameAsync(RoleName);
            var result = await _roleManager.DeleteAsync(role);
            
            return View();
        }
    }
}
