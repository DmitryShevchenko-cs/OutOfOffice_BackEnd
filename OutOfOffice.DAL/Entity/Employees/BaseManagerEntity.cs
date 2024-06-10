namespace OutOfOffice.DAL.Entity.Employees;

public class BaseManagerEntity : BaseEmployeeEntity
{
    public ICollection<ApprovalRequest> ApprovalRequest { get; set; } = null!;
}