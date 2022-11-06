using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class moveaddresspropertiestoclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_StreetName",
                schema: "RegistrationSystem",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "UI_CityName",
                schema: "RegistrationSystem",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "AppartmentNumber",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Cities");

            migrationBuilder.EnsureSchema(
                name: "AddressProperties");

            migrationBuilder.EnsureSchema(
                name: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "Streets",
                schema: "RegistrationSystem",
                newName: "Streets",
                newSchema: "AddressProperties");

            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "RegistrationSystem",
                newName: "Phones",
                newSchema: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "PersonalCodes",
                schema: "RegistrationSystem",
                newName: "PersonalCodes",
                newSchema: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "LastNames",
                schema: "RegistrationSystem",
                newName: "LastNames",
                newSchema: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "FirstNames",
                schema: "RegistrationSystem",
                newName: "FirstNames",
                newSchema: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "Emails",
                schema: "RegistrationSystem",
                newName: "Emails",
                newSchema: "UserInfoProperties");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "RegistrationSystem",
                newName: "Cities",
                newSchema: "AddressProperties");

            migrationBuilder.AddColumn<int>(
                name: "AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "AddressProperties",
                table: "Streets",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "AddressProperties",
                table: "Cities",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AppartmentNumbers",
                schema: "AddressProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppartmentNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseNumbers",
                schema: "AddressProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseNumbers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "AppartmentNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "HouseNumberId");

            migrationBuilder.CreateIndex(
                name: "UI_AddressProperties_Street",
                schema: "AddressProperties",
                table: "Streets",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_UserInfoProperties_Phone",
                schema: "UserInfoProperties",
                table: "Phones",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_UserInfoProperties_PersonalCode",
                schema: "UserInfoProperties",
                table: "PersonalCodes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_UserInfoProperties_LastName",
                schema: "UserInfoProperties",
                table: "LastNames",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_UserInfoProperties_FirstName",
                schema: "UserInfoProperties",
                table: "FirstNames",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_UserInfoProperties_Email",
                schema: "UserInfoProperties",
                table: "Emails",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_AddressProperties_City",
                schema: "AddressProperties",
                table: "Cities",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_AddressProperties_AppartmenNumber",
                schema: "AddressProperties",
                table: "AppartmentNumbers",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_AddressProperties_HouseNumber",
                schema: "AddressProperties",
                table: "HouseNumbers",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AppartmentNumbers_AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "AppartmentNumberId",
                principalSchema: "AddressProperties",
                principalTable: "AppartmentNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_HouseNumbers_HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "HouseNumberId",
                principalSchema: "AddressProperties",
                principalTable: "HouseNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AppartmentNumbers_AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_HouseNumbers_HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "AppartmentNumbers",
                schema: "AddressProperties");

            migrationBuilder.DropTable(
                name: "HouseNumbers",
                schema: "AddressProperties");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "UI_AddressProperties_Street",
                schema: "AddressProperties",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "UI_UserInfoProperties_Phone",
                schema: "UserInfoProperties",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "UI_UserInfoProperties_PersonalCode",
                schema: "UserInfoProperties",
                table: "PersonalCodes");

            migrationBuilder.DropIndex(
                name: "UI_UserInfoProperties_LastName",
                schema: "UserInfoProperties",
                table: "LastNames");

            migrationBuilder.DropIndex(
                name: "UI_UserInfoProperties_FirstName",
                schema: "UserInfoProperties",
                table: "FirstNames");

            migrationBuilder.DropIndex(
                name: "UI_UserInfoProperties_Email",
                schema: "UserInfoProperties",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "UI_AddressProperties_City",
                schema: "AddressProperties",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "AppartmentNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HouseNumberId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "AddressProperties",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "AddressProperties",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "Streets",
                schema: "AddressProperties",
                newName: "Streets",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "UserInfoProperties",
                newName: "Phones",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "PersonalCodes",
                schema: "UserInfoProperties",
                newName: "PersonalCodes",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "LastNames",
                schema: "UserInfoProperties",
                newName: "LastNames",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "FirstNames",
                schema: "UserInfoProperties",
                newName: "FirstNames",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "Emails",
                schema: "UserInfoProperties",
                newName: "Emails",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "AddressProperties",
                newName: "Cities",
                newSchema: "RegistrationSystem");

            migrationBuilder.AddColumn<string>(
                name: "AppartmentNumber",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Streets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UI_StreetName",
                schema: "RegistrationSystem",
                table: "Streets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_CityName",
                schema: "RegistrationSystem",
                table: "Cities",
                column: "Name",
                unique: true);
        }
    }
}
