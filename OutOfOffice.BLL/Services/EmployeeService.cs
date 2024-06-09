using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;

namespace OutOfOffice.BLL.Services;

public class EmployeeService : IEmployeeService
{
    public Task<EmployeeModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}