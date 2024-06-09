using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AuthorizationInfo_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Managers_ApproverId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeesId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_ApprovalRequests_ApprovalRequestId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Managers_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_ApprovalRequestId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "BaseEmployees");

            migrationBuilder.AddColumn<int>(
                name: "LeaveRequestId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorizationInfoId",
                table: "BaseEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HrMangerId",
                table: "BaseEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "BaseEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OutOfOfficeBalance",
                table: "BaseEmployees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "BaseEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "BaseEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "BaseEmployees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubdivisionId",
                table: "BaseEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEmployees",
                table: "BaseEmployees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuthorizationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizationInfos_BaseEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "BaseEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEmployees_HrMangerId",
                table: "BaseEmployees",
                column: "HrMangerId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEmployees_Id",
                table: "BaseEmployees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEmployees_PositionId",
                table: "BaseEmployees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEmployees_SubdivisionId",
                table: "BaseEmployees",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationInfos_EmployeeId",
                table: "AuthorizationInfos",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_BaseEmployees_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_EmployeeProject_BaseEmployees_EmployeesId",
                table: "EmployeeProject",
                column: "EmployeesId",
                principalTable: "BaseEmployees",
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
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

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
                name: "FK_EmployeeProject_BaseEmployees_EmployeesId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_BaseEmployees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "AuthorizationInfos");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEmployees",
                table: "BaseEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BaseEmployees_HrMangerId",
                table: "BaseEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BaseEmployees_Id",
                table: "BaseEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BaseEmployees_PositionId",
                table: "BaseEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BaseEmployees_SubdivisionId",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "AuthorizationInfoId",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "HrMangerId",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "OutOfOfficeBalance",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BaseEmployees");

            migrationBuilder.DropColumn(
                name: "SubdivisionId",
                table: "BaseEmployees");

            migrationBuilder.RenameTable(
                name: "BaseEmployees",
                newName: "Managers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HrMangerId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    SubdivisionId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutOfOfficeBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Managers_HrMangerId",
                        column: x => x.HrMangerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Subdivisions_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "Subdivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovalRequestId",
                table: "LeaveRequests",
                column: "ApprovalRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HrMangerId",
                table: "Employees",
                column: "HrMangerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Id",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SubdivisionId",
                table: "Employees",
                column: "SubdivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Managers_ApproverId",
                table: "ApprovalRequests",
                column: "ApproverId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeesId",
                table: "EmployeeProject",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_ApprovalRequests_ApprovalRequestId",
                table: "LeaveRequests",
                column: "ApprovalRequestId",
                principalTable: "ApprovalRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Managers_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
