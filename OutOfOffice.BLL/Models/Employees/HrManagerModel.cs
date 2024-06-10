namespace OutOfOffice.BLL.Models.Employees;

public class HrManagerModel : BaseManagerModel
{
    public ICollection<EmployeeModel> Partners { get; set; } = null!;
}