using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class cascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId1",
                unique: true,
                filter: "[UserInfoId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId",
                principalSchema: "RegistrationSystem",
                principalTable: "UserInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId1",
                principalSchema: "RegistrationSystem",
                principalTable: "UserInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserInfoId1",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId",
                principalSchema: "RegistrationSystem",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
