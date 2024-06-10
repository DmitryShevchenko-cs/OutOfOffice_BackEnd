using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Models.Enums;
using OutOfOffice.DAL.Entity;

namespace OutOfOffice.BLL.Models;

public class ApprovalRequestModel : BaseModel
{
    public Status Status { get; set; }

    public int ApproverId { get; set; }
    public BaseManagerModel Approver { get; set; } = null!;
    
    public int LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; } = null!;

    public string Comment { get; set; } = null!;

}