using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class removeaddressdeletecascadeuserinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Addresses_AddressId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Addresses_AddressId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AddressId",
                principalSchema: "RegistrationSystem",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Addresses_AddressId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Addresses_AddressId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "AddressId",
                principalSchema: "RegistrationSystem",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
