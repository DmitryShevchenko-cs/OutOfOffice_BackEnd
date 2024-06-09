using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IEmployeeRepository : IBasicRepository<Employee>
{
    public Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
}