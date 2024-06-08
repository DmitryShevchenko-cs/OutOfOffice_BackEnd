namespace OutOfOffice.DAL.Entity.Employees;

public class BaseEmployeeEntity : BaseEntity
{
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }

    public int AuthorizationInfoId { get; set; }
    public AuthorizationInfo? AuthorizationInfo { get; set; } = null!;
    
}