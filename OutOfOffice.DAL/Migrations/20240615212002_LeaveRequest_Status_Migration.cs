using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequest_Status_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ApprovalRequests",
                newName: "ApprovalRequestStatus");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "ApprovalRequestStatus",
                table: "ApprovalRequests",
                newName: "Status");
        }
    }
}
