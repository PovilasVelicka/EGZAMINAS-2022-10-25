using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class changepropertiestoclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "PersonalCode",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Emails",
                schema: "RegistrationSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirstNames",
                schema: "RegistrationSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LastNames",
                schema: "RegistrationSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalCodes",
                schema: "RegistrationSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                schema: "RegistrationSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "FirstNameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "LastNameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "PersonalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "PhoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Emails_EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "EmailId",
                principalSchema: "RegistrationSystem",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_FirstNames_FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "FirstNameId",
                principalSchema: "RegistrationSystem",
                principalTable: "FirstNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_LastNames_LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "LastNameId",
                principalSchema: "RegistrationSystem",
                principalTable: "LastNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_PersonalCodes_PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "PersonalCodeId",
                principalSchema: "RegistrationSystem",
                principalTable: "PersonalCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Phones_PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos",
                column: "PhoneId",
                principalSchema: "RegistrationSystem",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Emails_EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_FirstNames_FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_LastNames_LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_PersonalCodes_PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Phones_PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "Emails",
                schema: "RegistrationSystem");

            migrationBuilder.DropTable(
                name: "FirstNames",
                schema: "RegistrationSystem");

            migrationBuilder.DropTable(
                name: "LastNames",
                schema: "RegistrationSystem");

            migrationBuilder.DropTable(
                name: "PersonalCodes",
                schema: "RegistrationSystem");

            migrationBuilder.DropTable(
                name: "Phones",
                schema: "RegistrationSystem");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "EmailId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "FirstNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "LastNameId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "PersonalCodeId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                schema: "RegistrationSystem",
                table: "UserInfos");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalCode",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "RegistrationSystem",
                table: "UserInfos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
