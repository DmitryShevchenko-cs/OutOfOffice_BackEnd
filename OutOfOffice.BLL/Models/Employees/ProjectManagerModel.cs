namespace OutOfOffice.BLL.Models.Employees;

public class ProjectManagerModel : BaseManagerModel
{
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}