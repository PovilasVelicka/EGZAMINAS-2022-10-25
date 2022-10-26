using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class changedfildsmovephototouserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "RegistrationSystem",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Salt",
                schema: "RegistrationSystem",
                table: "Accounts",
                newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "RegistrationSystem",
                table: "Accounts",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                schema: "RegistrationSystem",
                table: "Accounts",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "RegistrationSystem",
                table: "Accounts",
                newName: "Password");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "RegistrationSystem",
                table: "Accounts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
