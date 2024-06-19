using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.Web.Models;

public class ApprovalRequestUpdateModel
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
}