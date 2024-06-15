using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.BLL.Models;

public class ApprovalRequestModel : BaseModel
{
    public ApprovalRequestStatus ApprovalRequestStatus { get; set; }

    public int ApproverId { get; set; }
    public BaseManagerModel Approver { get; set; } = null!;
    
    public int LeaveRequestId { get; set; }
    public LeaveRequestModel LeaveRequest { get; set; } = null!;

    public string Comment { get; set; } = null!;

}