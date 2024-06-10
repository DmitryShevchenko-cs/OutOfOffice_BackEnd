using OutOfOffice.BLL.Models.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestViewModel
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public ManagerViewModel Approver { get; set; } = null!;
    public string Comment { get; set; } = null!;
}