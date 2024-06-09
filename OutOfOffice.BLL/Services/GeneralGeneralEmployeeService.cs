using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Helpers;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class GeneralGeneralEmployeeService : IGeneralEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GeneralGeneralEmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<GeneralEmployeeModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");
        
        var employeeModel = _mapper.Map<GeneralEmployeeModel>(employeeDb);
        return employeeModel;
    }

    public async Task CreateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == employeeModel.Login, cancellationToken);

        if (employeeDb is not null)
        {
            if (employeeDb.Login == employeeModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }
        
        var employee = _mapper.Map<GeneralEmployee>(employeeModel);
        employee.Password = PasswordHelper.HashPassword(employee.Password);
        await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);
    }
}