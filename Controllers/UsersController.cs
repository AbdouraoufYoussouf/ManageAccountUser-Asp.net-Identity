using LoginRegisterUser.Data;
using LoginRegisterUser.Models;
using LoginRegisterUser.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterUser.Controllers
{
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
        // public ActionResult Index()
        // {
        //     var usersWithRoles = (from user in _context.Users
        //         select new
        //         {
        //             Username = user.UserName,
        //             Email = user.Email,
        //             RoleNames = (from UserRole in uS
        //                         join role in _context.Roles on UserRole.RoleId equals role.Id
        //                         select role.Name).ToList()
        //         }).ToList().Select(p => new UserViewModel()

        //         {
        //             UserName = p.Username,
        //             Email = p.Email,
        //             Roles = string.Join(",", p.RoleNames)
        //         });
        //     return View();
        //}
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModel = new List<UserViewModel>();
            foreach (AppUser user in users)
            {
                var thisViewModel = new UserViewModel();
                thisViewModel.Id = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userViewModel.Add(thisViewModel);
            }
            return View(userViewModel);
        }
        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }

    
}
