using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginRegisterUser.Migrations
{
    public partial class AsignedAllroleAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles] (UserId , RoleId) SELECT '8eb91b93-08ee-46f9-ad09-c1ec5791d629', Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = '8eb91b93-08ee-46f9-ad09-c1ec5791d629'");
        }
    }
}
