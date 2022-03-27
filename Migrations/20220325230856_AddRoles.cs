using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginRegisterUser.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Admin", "ADMIN" },
                    { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "SuperAdmin", "SUPERADMIN" },
                    { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Manager", "MANAGER" },
                    { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "User", "USER" }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Roles}");
        }
    }
}
