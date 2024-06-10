using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Entity.Selections;

public class Position : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<Employee> Employees { get; set; }= null!;
}