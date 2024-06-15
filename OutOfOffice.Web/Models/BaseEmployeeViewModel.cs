namespace OutOfOffice.Web.Models;

public class BaseEmployeeViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
}