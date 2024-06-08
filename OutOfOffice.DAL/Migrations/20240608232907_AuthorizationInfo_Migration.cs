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
                name: "FK_LeaveRequests_ApprovalRequests_ApprovalRequestId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_ApprovalRequestId",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<int>(
                name: "AuthorizationInfoId",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorizationInfoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeaveRequestId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthorizationInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_AuthorizationInfoId",
                table: "Managers",
                column: "AuthorizationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AuthorizationInfoId",
                table: "Employees",
                column: "AuthorizationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AuthorizationInfo_AuthorizationInfoId",
                table: "Employees",
                column: "AuthorizationInfoId",
                principalTable: "AuthorizationInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AuthorizationInfo_AuthorizationInfoId",
                table: "Managers",
                column: "AuthorizationInfoId",
                principalTable: "AuthorizationInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AuthorizationInfo_AuthorizationInfoId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AuthorizationInfo_AuthorizationInfoId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "AuthorizationInfo");

            migrationBuilder.DropIndex(
                name: "IX_Managers_AuthorizationInfoId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AuthorizationInfoId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "AuthorizationInfoId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "AuthorizationInfoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovalRequestId",
                table: "LeaveRequests",
                column: "ApprovalRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_ApprovalRequests_ApprovalRequestId",
                table: "LeaveRequests",
                column: "ApprovalRequestId",
                principalTable: "ApprovalRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
