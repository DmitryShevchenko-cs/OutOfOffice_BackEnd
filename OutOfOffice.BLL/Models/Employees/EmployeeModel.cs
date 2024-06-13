using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Models.Employees;

public class EmployeeModel : BaseEmployeeModel
{
    public int? SubdivisionId { get; set; }
    public Subdivision Subdivision { get; set; }= null!;
    
    public int? PositionId { get; set; }
    public Position Position { get; set; }= null!;
    
    public bool Status { get; set; }
    public int OutOfOfficeBalance { get; set; }

    public int? HrMangerId { get; set; }
    public HrManagerModel HrManager { get; set; } = null!;
    
    public ICollection<LeaveRequestModel> LeaveRequests { get; set; } = null!;
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}