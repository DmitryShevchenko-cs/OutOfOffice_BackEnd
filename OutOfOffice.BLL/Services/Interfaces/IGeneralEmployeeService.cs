using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IGeneralEmployeeService : IBasicService<GeneralEmployeeModel>
{
    Task<GeneralEmployeeModel> CreateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task<GeneralEmployeeModel> UpdateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    
}