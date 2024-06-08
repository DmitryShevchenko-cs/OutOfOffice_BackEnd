namespace OutOfOffice.DAL.Entity.Employees;

public class ProjectManager : BaseManagerEntity
{
    public IEnumerable<Project> Projects { get; set; } = null!;
   
}