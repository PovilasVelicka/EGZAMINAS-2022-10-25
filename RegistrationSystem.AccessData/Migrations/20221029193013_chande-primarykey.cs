using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class chandeprimarykey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Accounts_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Accounts_Id",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "Id",
                principalSchema: "RegistrationSystem",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Accounts_Id",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "RegistrationSystem",
                table: "UserInfos",
                newName: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Accounts_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AccountId",
                principalSchema: "RegistrationSystem",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
