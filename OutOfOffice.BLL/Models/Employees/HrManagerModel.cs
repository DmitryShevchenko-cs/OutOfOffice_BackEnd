namespace OutOfOffice.BLL.Models.Employees;

public class HrManagerModel : BaseManagerModel
{
    public ICollection<GeneralEmployeeModel> Partners { get; set; } = null!;
}