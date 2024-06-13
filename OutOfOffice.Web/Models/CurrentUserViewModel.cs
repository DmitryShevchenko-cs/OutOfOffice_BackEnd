using OutOfOffice.Web.Models.Enums;

namespace OutOfOffice.Web.Models;

public class CurrentUserViewModel
{
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public UserType UserType { get; set; }
}