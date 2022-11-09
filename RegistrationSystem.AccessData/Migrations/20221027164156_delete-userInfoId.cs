using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class deleteuserInfoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
