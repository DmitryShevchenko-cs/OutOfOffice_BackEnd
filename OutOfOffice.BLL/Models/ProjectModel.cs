using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Models;

public class ProjectModel : BaseModel
{
    public int ProjectManagerId { get; set; }
    public ProjectManagerModel? ProjectManager { get; set; }
    
    public int ProjectTypeId { get; set; }
    public SelectionModel ProjectType { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
    
    public ICollection<EmployeeModel> Employees { get; set; } = null!;
    public bool Status { get; set; }
}