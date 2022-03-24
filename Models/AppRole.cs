using Microsoft.AspNetCore.Identity;

namespace LoginRegisterUser.Models
{
    public class AppRole : IdentityRole
    {
        public static string[] Roles = new string[] {"User","Admin","Manager","SuperAdmin"};
    }
}
