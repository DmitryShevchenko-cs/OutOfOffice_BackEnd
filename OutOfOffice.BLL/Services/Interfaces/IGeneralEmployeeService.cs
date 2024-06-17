using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IGeneralEmployeeService : IBasicService<EmployeeModel>
{
    Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task<EmployeeModel> UpdateEmployeeAsync(EmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    Task DeactivateEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<List<EmployeeModel>> GetEmployeesAsync(int managerId, CancellationToken cancellationToken = default);
    
}