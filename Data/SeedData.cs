using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterUser.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Roles.Any()) { return; }
                    
                context.Roles.AddRange(
                    new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "Admin".ToUpper()
                },
                    new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SuperAdmin",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "SuperAdmin".ToUpper()
                },
                    new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "User".ToUpper()
                });
                context.SaveChanges();
            }
        }
    }
}
