using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Helpers;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class AuthEmployeeService : IAuthEmployeeService
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public AuthEmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<BaseEmployeeEntity> GetByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == login && i.isDeactivated == false, cancellationToken);
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with login {login} not found");
        
        if (!PasswordHelper.VerifyHashedPassword(employeeDb.Password, password))
        {
            throw new WrongLoginOrPasswordException("Wrong login or password");
        }
        
        return employeeDb;
    }

    public async Task<BaseEmployeeModel> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.AuthorizationInfo != null 
                && i.AuthorizationInfo.RefreshToken == refreshToken, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee  with this refresh token {refreshToken} not found");
        
        if (employeeDb!.AuthorizationInfo is not null && employeeDb.AuthorizationInfo.ExpiredDate <= DateTime.Now.AddDays(-1))
            throw new TimeoutException();

        var employeeModel = _mapper.Map<BaseEmployeeModel>(employeeDb);
        return employeeModel;
        
    }

    public async Task AddAuthorizationValueAsync(BaseEmployeeModel employeeModel, string refreshToken, DateTime? expiredDate = null,
        CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll().Include(r => r.AuthorizationInfo).FirstOrDefaultAsync(r => r.Id == employeeModel.Id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with this Id {employeeModel.Id} not found");

        if (employeeDb!.AuthorizationInfo is not null &&
            employeeDb.AuthorizationInfo.ExpiredDate <= DateTime.Now.AddDays(-1))
            await LogOutAsync(employeeDb.Id, cancellationToken);

        employeeDb.AuthorizationInfo = new AuthorizationInfo()
        {
            RefreshToken = refreshToken,
            ExpiredDate = expiredDate,
        };
        await _employeeRepository.UpdateEmployeeAsync(employeeDb, cancellationToken);
    }

    public async Task LogOutAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll().Include(r => r.AuthorizationInfo).FirstOrDefaultAsync(r => r.Id == employeeId, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with this Id {employeeId} not found");
        
        if (employeeDb!.AuthorizationInfo is not null)
        {
            employeeDb.AuthorizationInfo = null;
            await _employeeRepository.UpdateEmployeeAsync(employeeDb, cancellationToken);
        }
        else throw new NullReferenceException($"User with this token not found");
    }

    public async Task<BaseEmployeeModel> GetUserById(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await _employeeRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is null)
            throw new EmployeeNotFoundException($"User with this Id {userId} not found");
        return _mapper.Map<BaseEmployeeModel>(userDb);
    }
}