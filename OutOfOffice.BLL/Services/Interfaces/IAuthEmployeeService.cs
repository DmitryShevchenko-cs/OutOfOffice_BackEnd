using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IAuthEmployeeService
{
    Task<BaseEmployeeModel> GetByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken = default);

    Task<BaseEmployeeModel> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    
    Task AddAuthorizationValueAsync(BaseEmployeeModel employeeModel, string refreshToken, DateTime? expiredDate = null, CancellationToken cancellationToken = default);

    Task LogOutAsync(int employeeId, CancellationToken cancellationToken = default);

    Task<BaseEmployeeEntity> GetUserById(int userId, CancellationToken cancellationToken = default);

}