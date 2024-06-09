using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IEmployeeRepository : IBasicRepository<BaseEmployeeEntity>
{
    Task AddEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);
    Task UpdateEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);
}