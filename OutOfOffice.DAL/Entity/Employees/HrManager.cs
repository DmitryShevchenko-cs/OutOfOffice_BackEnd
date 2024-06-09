namespace OutOfOffice.DAL.Entity.Employees;

public class HrManager : BaseManagerEntity
{
    public IEnumerable<GeneralEmployee> Partners { get; set; } = null!;
}