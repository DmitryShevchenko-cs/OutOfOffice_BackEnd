using OutOfOffice.BLL.Models.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestUpdateModel
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public string Comment { get; set; } = null!;
}