namespace OutOfOffice.Web.Models;

public class LeaveRequestViewModel
{
    public int Id { get; set; }
    public AbsenceReasonViewModel AbsenceReasonModel { get; set; } = null!;
    public ApprovalRequestViewModel ApprovalRequest { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
}