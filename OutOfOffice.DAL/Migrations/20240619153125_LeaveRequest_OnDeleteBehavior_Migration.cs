using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequest_OnDeleteBehavior_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ApprovalRequestId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "BaseEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_BaseEmployees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalRequestId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "BaseEmployees",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
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
    }
}
