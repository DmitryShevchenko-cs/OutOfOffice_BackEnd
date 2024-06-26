using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.Web.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    
    public string FullName { get; set; } = null!;
    
    public SelectionViewModel Subdivision { get; set; }= null!;
    public SelectionViewModel Position { get; set; }= null!;
    
    public bool Status { get; set; }
    public int OutOfOfficeBalance { get; set; }
    
    public ManagerViewModel HrManager { get; set; } = null!;
}