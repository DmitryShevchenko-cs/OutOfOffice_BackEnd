using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Entity;

public class Project : BaseEntity
{
    public int ProjectManagerId { get; set; }
    public ProjectManager? ProjectManager { get; set; } = null!;
    
    public ProjectType ProjectType { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
    public bool Status { get; set; }
    
    public ICollection<Employee> Employees { get; set; } = null!;
    
    public bool isDeactivated { get; set; }
    
}