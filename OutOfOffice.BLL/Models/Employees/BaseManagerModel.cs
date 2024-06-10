namespace OutOfOffice.BLL.Models.Employees;

public class BaseManagerModel : BaseEmployeeModel
{
    public ICollection<ApprovalRequestModel> ApprovalRequest { get; set; } = null!;
}