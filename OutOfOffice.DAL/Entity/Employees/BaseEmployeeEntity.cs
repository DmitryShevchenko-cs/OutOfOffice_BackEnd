namespace OutOfOffice.DAL.Entity.Employees;

public class BaseEmployeeEntity : BaseEntity
{
    public string FullName { get; set; } = null!;
    public byte[]? Photo { get; set; }
}