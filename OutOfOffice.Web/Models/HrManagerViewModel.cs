namespace OutOfOffice.Web.Models;

public class HrManagerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public List<EmployeeViewModel> Partners { get; set; } = null!;
}