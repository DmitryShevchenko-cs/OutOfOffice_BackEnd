using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IEmployeeRepository : IBasicRepository<BaseEmployeeEntity>
{
    Task<BaseEmployeeEntity> AddEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);
    Task<BaseEmployeeEntity> UpdateEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default);

    Task UpdateEmployeeAsync(List<BaseEmployeeEntity> employee,
        CancellationToken cancellationToken = default);
}