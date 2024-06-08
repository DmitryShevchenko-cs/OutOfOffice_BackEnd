namespace OutOfOffice.DAL.Entity.Employees;

public class HrManager : BaseManagerEntity
{
    public IEnumerable<Employee> Partners { get; set; } = null!;
}