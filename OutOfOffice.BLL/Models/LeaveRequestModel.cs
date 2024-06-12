using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Models;

public class LeaveRequestModel : BaseModel
{
    public int EmployeeId { get; set; }
    public EmployeeModel Employee { get; set; } = null!;
    
    public int AbsenceReasonId { get; set; }
    public AbsenceReason AbsenceReason { get; set; }= null!;
    
    public int ApprovalRequestId { get; set; }
    public ApprovalRequestModel? ApprovalRequest { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Comment { get; set; } = null!;
}