namespace OutOfOffice.DAL.Entity.Employees;

public class BaseEmployeeEntity : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }

    public int AuthorizationInfoId { get; set; }
    public AuthorizationInfo? AuthorizationInfo { get; set; } = null!;
    
}