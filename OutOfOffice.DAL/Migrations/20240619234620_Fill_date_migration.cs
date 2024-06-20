using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutOfOffice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fill_date_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrMangerId",
                table: "BaseEmployees");

            migrationBuilder.RenameColumn(
                name: "HrMangerId",
                table: "BaseEmployees",
                newName: "HrManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_HrMangerId",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_HrManagerId");

            migrationBuilder.InsertData(
                table: "AbsenceReasons",
                columns: new[] { "Id", "ReasonDescription" },
                values: new object[,]
                {
                    { 1, "Sick" },
                    { 2, "Vacation" }
                });

            migrationBuilder.InsertData(
                table: "BaseEmployees",
                columns: new[] { "Id", "AuthorizationInfoId", "Discriminator", "FullName", "Login", "Password", "isDeactivated" },
                values: new object[] { 1, 0, "Admin", "ADMIN", "admin", "AMnqUjltBJxY6WDypk7qmQ4ftQh1k+IqhlM/FBD7jvhadR3QJVo9EzherHLrK70AQw==", false });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Software Engineer" },
                    { 2, "Frontend Developer" },
                    { 3, "Backend Developer" },
                    { 4, "Full Stack Developer" },
                    { 5, "QA Engineer" },
                    { 6, "UI/UX Designer" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Web App" },
                    { 2, "Data Migration" },
                    { 3, "Cloud Computing Project" },
                    { 4, "Blockchain" }
                });

            migrationBuilder.InsertData(
                table: "Subdivisions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Development" },
                    { 2, "Operations" },
                    { 3, "Support" },
                    { 4, "Security" },
                    { 5, "QA" },
                    { 6, "Data" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrManagerId",
                table: "BaseEmployees",
                column: "HrManagerId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrManagerId",
                table: "BaseEmployees");

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BaseEmployees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "HrManagerId",
                table: "BaseEmployees",
                newName: "HrMangerId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_HrManagerId",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_HrMangerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrMangerId",
                table: "BaseEmployees",
                column: "HrMangerId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
