namespace OutOfOffice.BLL.Models;

public class AuthorizationInfoModel
{
    public string RefreshToken { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }
}