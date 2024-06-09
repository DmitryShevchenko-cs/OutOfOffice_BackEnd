using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.DAL.Entity;

public class ApprovalRequest : BaseEntity
{
    public Status Status { get; set; }

    public int ApproverId { get; set; }
    public BaseManagerEntity Approver { get; set; } = null!;
    
    public int LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; } = null!;

    public string Comment { get; set; } = null!;
}