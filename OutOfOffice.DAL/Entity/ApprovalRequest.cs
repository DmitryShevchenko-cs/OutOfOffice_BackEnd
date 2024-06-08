using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Entity;

public class ApprovalRequest : BaseEntity
{

    public BaseManagerEntity Approver { get; set; } = null!;
    
    public LeaveRequest LeaveRequest { get; set; } = null!;

    public string Comment { get; set; } = null!;
}