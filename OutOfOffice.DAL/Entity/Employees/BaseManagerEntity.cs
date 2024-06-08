namespace OutOfOffice.DAL.Entity.Employees;

public class BaseManagerEntity : BaseEmployeeEntity
{
    public IEnumerable<ApprovalRequest> ApprovalRequest { get; set; } = null!;
}