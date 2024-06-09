namespace OutOfOffice.Web.Models;

public class EmployeeAuthorizeModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsNeedToRemember { get; set; }
}