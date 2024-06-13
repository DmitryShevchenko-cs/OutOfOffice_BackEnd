namespace OutOfOffice.Web.Models;

public class LeaveRequestCreateModel
{
    public int AbsenceReasonId { get; set; }
    public int ApprovalRequestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
}