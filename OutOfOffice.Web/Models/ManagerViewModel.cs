namespace OutOfOffice.Web.Models;

public class ManagerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public string Role { get; set; } = null!;
}