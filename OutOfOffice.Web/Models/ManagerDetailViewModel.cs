namespace OutOfOffice.Web.Models;

public class ManagerDetailViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public List<ProjectViewModel> Projects { get; set; }= null!;
    public List<EmployeeViewModel> Partners { get; set; } = null!;
}