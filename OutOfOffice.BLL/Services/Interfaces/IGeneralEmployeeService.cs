using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IGeneralEmployeeService : IBasicService<GeneralEmployeeModel>
{
    Task CreateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default);
    
}