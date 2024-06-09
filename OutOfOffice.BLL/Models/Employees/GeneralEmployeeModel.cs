using OutOfOffice.BLL.Models.Selections;

namespace OutOfOffice.BLL.Models.Employees;

public class GeneralEmployeeModel : BaseEmployeeModel
{
    public int? SubdivisionId { get; set; }
    public SubdivisionModel Subdivision { get; set; }= null!;
    
    public int? PositionId { get; set; }
    public PositionModel Position { get; set; }= null!;
    
    public bool Status { get; set; }
    public decimal OutOfOfficeBalance { get; set; }

    public int? HrMangerId { get; set; }
    public HrManagerModel HrManager { get; set; } = null!;
    
    public IEnumerable<LeaveRequestModel> LeaveRequests { get; set; } = null!;
    public IEnumerable<ProjectModel> Projects { get; set; } = null!;
}