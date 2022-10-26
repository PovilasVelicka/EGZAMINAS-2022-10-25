using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class changnulableUserInfoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UserInfos_UserInfoId",
                schema: "RegistrationSystem",
                table: "Accounts",
                column: "UserInfoId",
                principalSchema: "RegistrationSystem",
                principalTable: "UserInfos",
                principalColumn: "Id");
        }
    }
}
