using LoginRegisterUser.Data;
using LoginRegisterUser.Models;
using LoginRegisterUser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterUser.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private readonly ApplicationDbContext _context;
       public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModel = new List<UserViewModel>();
            foreach (AppUser user in users)
            {
                var viwModel = new UserViewModel();
                viwModel.Id = user.Id;
                viwModel.Email = user.Email;
                viwModel.FirstName = user.FirstName;
                viwModel.LastName = user.LastName;
                viwModel.Roles = await GetUserRoles(user);
                userViewModel.Add(viwModel);
            }
            return View(userViewModel);
        }
        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


        // Manage des roles de l'user
        
        public async Task<IActionResult> ManageRole(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.LastName,
                Roles = roles.Select(role => new RoleViewModel
                {
                     RoleId = role.Id,
                     RoleName = role.Name,
                     IsSelected = _userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };
            return View(viewModel);
        }
    }

    
}
