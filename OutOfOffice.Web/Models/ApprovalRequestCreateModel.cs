
using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestCreateModel
{
    public ApprovalRequestStatus ApprovalRequestStatus { get; set; }
    public int ApproverId { get; set; }
    public int LeaveRequestId { get; set; }
    public string Comment { get; set; } = null!;
}