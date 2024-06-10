using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Name_Changes_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationInfos_Employees_EmployeeId",
                table: "AuthorizationInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_HrMangerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_GeneralEmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "GeneralEmployeeProject");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_GeneralEmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GeneralEmployeeId",
                table: "LeaveRequests");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "BaseEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SubdivisionId",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_SubdivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PositionId",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Id",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_HrMangerId",
                table: "BaseEmployees",
                newName: "IX_BaseEmployees_HrMangerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEmployees",
                table: "BaseEmployees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_BaseEmployees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "BaseEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_BaseEmployees_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationInfos_BaseEmployees_EmployeeId",
                table: "AuthorizationInfos",
                column: "EmployeeId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrMangerId",
                table: "BaseEmployees",
                column: "HrMangerId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEmployees_Positions_PositionId",
                table: "BaseEmployees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEmployees_Subdivisions_SubdivisionId",
                table: "BaseEmployees",
                column: "SubdivisionId",
                principalTable: "Subdivisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_BaseEmployees_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_BaseEmployees_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationInfos_BaseEmployees_EmployeeId",
                table: "AuthorizationInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEmployees_BaseEmployees_HrMangerId",
                table: "BaseEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEmployees_Positions_PositionId",
                table: "BaseEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEmployees_Subdivisions_SubdivisionId",
                table: "BaseEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_BaseEmployees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEmployees",
                table: "BaseEmployees");

            migrationBuilder.RenameTable(
                name: "BaseEmployees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_SubdivisionId",
                table: "Employees",
                newName: "IX_Employees_SubdivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_PositionId",
                table: "Employees",
                newName: "IX_Employees_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_Id",
                table: "Employees",
                newName: "IX_Employees_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEmployees_HrMangerId",
                table: "Employees",
                newName: "IX_Employees_HrMangerId");

            migrationBuilder.AddColumn<int>(
                name: "GeneralEmployeeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GeneralEmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralEmployeeProject", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_GeneralEmployeeProject_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralEmployeeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_GeneralEmployeeId",
                table: "LeaveRequests",
                column: "GeneralEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralEmployeeProject_ProjectsId",
                table: "GeneralEmployeeProject",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Employees_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationInfos_Employees_EmployeeId",
                table: "AuthorizationInfos",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_HrMangerId",
                table: "Employees",
                column: "HrMangerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionId",
                table: "Employees",
                column: "SubdivisionId",
                principalTable: "Subdivisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_GeneralEmployeeId",
                table: "LeaveRequests",
                column: "GeneralEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
