namespace OutOfOffice.DAL.Entity.Selections;

public class ProjectType : BaseEntity
{
    public string TypeName { get; set; } = null!;
    public IEnumerable<Project> Projects { get; set; } = null!;
}