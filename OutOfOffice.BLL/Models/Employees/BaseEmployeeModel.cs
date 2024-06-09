namespace OutOfOffice.BLL.Models.Employees;

public class BaseEmployeeModel : BaseModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }

    public int AuthorizationInfoId { get; set; }
    public AuthorizationInfoModel? AuthorizationInfo { get; set; } = null!;
}