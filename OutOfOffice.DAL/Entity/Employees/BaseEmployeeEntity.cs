namespace OutOfOffice.DAL.Entity.Employees;

public class BaseEmployeeEntity : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;

    public int AuthorizationInfoId { get; set; }
    public AuthorizationInfo? AuthorizationInfo { get; set; }

    public bool isDeactivated { get; set; }
    
}