namespace OutOfOffice.Web.Models;

public class ProjectManagerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public List<ProjectViewModel> Projects { get; set; }= null!;
}