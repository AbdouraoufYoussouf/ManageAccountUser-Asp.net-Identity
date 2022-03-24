using LoginRegisterUser.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginRegisterUser.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("Users", "security"); //il prend en parametre l'entité et le shema de la BD
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            /// ajout des roles ce qui n'est pas une bonne idéé de les ajouter par ici car achaque migration ef va ajouter ça encore ce qui n'est pas pratique

            //builder.Entity<IdentityRole>()
            //    .HasData(
            //        new IdentityRole
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "Admin",
            //            NormalizedName = "Admin".ToUpper(),
            //            ConcurrencyStamp = Guid.NewGuid().ToString()
            //        },
            //        new IdentityRole
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "User",
            //            NormalizedName = "User".ToUpper(),
            //            ConcurrencyStamp = Guid.NewGuid().ToString()
            //        },
            //        new IdentityRole
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "SuperAdmin",
            //            NormalizedName = "SuperAdmin".ToUpper(),
            //            ConcurrencyStamp = Guid.NewGuid().ToString()
            //        },
            //        new IdentityRole
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "Manager",
            //            NormalizedName = "Manager".ToUpper(),
            //            ConcurrencyStamp = Guid.NewGuid().ToString()
            //        }
            //    );
        }
    }
}