namespace OutOfOffice.DAL.Entity.Employees;

public class HrManager : BaseManagerEntity
{
    public ICollection<GeneralEmployee> Partners { get; set; } = null!;
}