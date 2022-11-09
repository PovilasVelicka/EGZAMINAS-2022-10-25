using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class PicturechangedtoProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                schema: "RegistrationSystem",
                table: "UserInfos",
                newName: "ProfilePicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                schema: "RegistrationSystem",
                table: "UserInfos",
                newName: "Photo");
        }
    }
}
