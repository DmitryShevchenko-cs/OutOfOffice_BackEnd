namespace OutOfOffice.DAL.Entity.Employees;

public class BaseManagerEntity : BaseEmployeeEntity
{
    public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = null!;
}