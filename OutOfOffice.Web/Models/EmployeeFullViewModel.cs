namespace OutOfOffice.Web.Models;

public class EmployeeFullViewModel : EmployeeViewModel
{
    public List<ProjectViewModel> Projects { get; set; } = null!;
}