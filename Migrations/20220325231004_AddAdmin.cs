using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginRegisterUser.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [FirstName], [LastName], [ProfilPicture], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8eb91b93-08ee-46f9-ad09-c1ec5791d629', N'Admin', N'Super Admin', NULL, N'Admin', N'ADMIN', N'superadmin@gmail.com', N'SUPERADMIN@GMAIL.COM',0 , N'AQAAAAEAACcQAAAAEJPj173euCcK0NbgDYE5xGjS7ZmfLEWvC6kUYrHpMNIdKvXDGQ2eahktnmHQR5dekw==', N'RS3KK6JWFBY2V7IIAL7FBU6Y3HMS6V24', N'5051db9d-89f7-4a5b-b88f-add2114a4563', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = '8eb91b93-08ee-46f9-ad09-c1ec5791d629' ");
        }
    }
}
