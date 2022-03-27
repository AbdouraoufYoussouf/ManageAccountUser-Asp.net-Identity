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
    [Authorize(Roles ="Admin,SuperAdmin")]
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
            var users = await _userManager.Users
            // .Where(user=>user.LastName!="Super Admin")
            .ToListAsync();
            var userViewModel = new List<UserViewModel>();
            foreach (AppUser user in users)
            {
                var viwModel = new UserViewModel();
                viwModel.Id = user.Id;
                viwModel.Email = user.Email;
                viwModel.UserName = user.UserName;
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
                UserName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.Select(role => new RoleViewModel
                {
                     RoleId = role.Id,
                     RoleName = role.Name,
                     
                     IsSelected = _userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRole(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if(user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                {   
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName); 
                }

                if(!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                {   
                    await _userManager.AddToRoleAsync(user, role.RoleName); 
                }   
            }
            /// Impossible to edit role Last Name is Super User
            foreach(var role in userRoles){
                if(!role.Equals("SuperAdmin") && user.LastName.Equals("Super Admin"))
                    {   
                        await _userManager.AddToRoleAsync(user, "SuperAdmin"); 
                    }
            }
            return RedirectToAction(nameof(Index));
        }

        // Add NEw USer

        public async Task<IActionResult> AddUser()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser
                 {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            if(await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already exist");
                    return View(model);
                }

            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Index","Users");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            return View(model);
            }
        }

        //Edit User

        public async Task<IActionResult> EditUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var viewModel = new EditUserViewModel{
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id
            };

            return View(viewModel);
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                
                if(user.Email.Equals("") || (user.Email.Equals(model.Email)))
                {
                    ModelState.AddModelError("Email",$"This {model.Email} is already assigned another User");
                }

                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);

                if(result.Succeeded){
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        // Delete USer

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId){
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Errors = $"{user.FirstName} cannot be found";
                return NotFound();
            }else{
                var result = await _userManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
