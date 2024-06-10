namespace OutOfOffice.DAL.Entity.Employees;

public class AuthProps : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}