using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestViewModel
{
    public int Id { get; set; }
    public ApprovalRequestStatus ApprovalRequestStatus { get; set; }
    public ManagerViewModel Approver { get; set; } = null!;
    public LeaveRequestViewModel LeaveRequest { get; set; } = null!;
    public string Comment { get; set; } = null!;
}