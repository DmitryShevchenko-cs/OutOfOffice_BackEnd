using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Entity.Selections;

public class Subdivision : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<GeneralEmployee> Employees { get; set; }= null!;
    
}