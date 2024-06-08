using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Entity;

public class AuthorizationInfo : BaseEntity
{
    public string RefreshToken { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }
    
}