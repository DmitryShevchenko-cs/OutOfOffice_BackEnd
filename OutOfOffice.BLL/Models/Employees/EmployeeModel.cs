using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Models.Employees;

public class EmployeeModel : BaseEmployeeModel
{
    public int? SubdivisionId { get; set; }
    public SelectionModel Subdivision { get; set; }= null!;
    
    public int? PositionId { get; set; }
    public SelectionModel Position { get; set; }= null!;
    
    public bool Status { get; set; }
    public int OutOfOfficeBalance { get; set; }

    public int? HrManagerId { get; set; }
    public HrManagerModel? HrManager { get; set; }
    
    public ICollection<LeaveRequestModel> LeaveRequests { get; set; } = null!;
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}