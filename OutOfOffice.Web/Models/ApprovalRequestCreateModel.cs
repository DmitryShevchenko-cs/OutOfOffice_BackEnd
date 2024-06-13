using OutOfOffice.BLL.Models.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestCreateModel
{
    public Status Status { get; set; }
    public int ApproverId { get; set; }
    public int LeaveRequestId { get; set; }
    public string Comment { get; set; } = null!;
}