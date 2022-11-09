using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class deleteuserinfoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AccountId",
                unique: true);
        }
    }
}
