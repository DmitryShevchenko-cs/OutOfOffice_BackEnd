
namespace OutOfOffice.BLL.Models;

public class AuthorizationInfo : BaseModel
{
    public string RefreshToken { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }
}