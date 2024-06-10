namespace OutOfOffice.DAL.Entity.Employees;

public class HrManager : BaseManagerEntity
{
    public ICollection<Employee> Partners { get; set; } = null!;
}