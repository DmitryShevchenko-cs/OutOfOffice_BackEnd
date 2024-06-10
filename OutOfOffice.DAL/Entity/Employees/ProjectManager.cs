namespace OutOfOffice.DAL.Entity.Employees;

public class ProjectManager : BaseManagerEntity
{
    public ICollection<Project> Projects { get; set; } = null!;
   
}