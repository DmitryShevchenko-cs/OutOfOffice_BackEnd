using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Entity;

public class AuthorizationInfo : BaseEntity
{
    public string RefreshToken { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }

    public int EmployeeId { get; set; }
    public BaseEmployeeEntity Employee { get; set; } = null!;

}