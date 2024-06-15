using OutOfOffice.Web.Models.Enums;

namespace OutOfOffice.Web.Models;

public class CurrentUserViewModel
{
    public string FullName { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public UserType UserType { get; set; }
}