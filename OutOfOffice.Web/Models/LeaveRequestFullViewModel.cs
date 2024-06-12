namespace OutOfOffice.Web.Models;

public class LeaveRequestFullViewModel
{
    public int Id { get; set; }
    public EmployeeViewModel Employee { get; set; } = null!;
    public AbsenceReasonViewModel AbsenceReason { get; set; } = null!;
    public ApprovalRequestViewModel ApprovalRequest { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
}