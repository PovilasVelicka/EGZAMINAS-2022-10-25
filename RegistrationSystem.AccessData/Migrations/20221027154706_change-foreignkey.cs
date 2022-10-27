using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class changeforeignkey : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AccountId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Accounts_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "RegistrationSystem",
                table: "UserInfos");

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
    }
}
