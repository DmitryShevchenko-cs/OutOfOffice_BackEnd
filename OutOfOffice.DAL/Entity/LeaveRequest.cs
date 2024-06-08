using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Enums;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Entity;

public class LeaveRequest : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public int AbsenceReasonId { get; set; }
    public AbsenceReason AbsenceReason { get; set; }= null!;
    
    public int ApprovalRequestId { get; set; }
    public ApprovalRequest ApprovalRequest { get; set; }= null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public string Comment { get; set; } = null!;

    
    
}