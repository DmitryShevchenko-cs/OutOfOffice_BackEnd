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

public class GeneralEmployeeService : IGeneralEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GeneralEmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
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

    public async Task<GeneralEmployeeModel> CreateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default)
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
        return _mapper.Map<GeneralEmployeeModel>(employee);
    }

    public async Task<GeneralEmployeeModel> UpdateEmployeeAsync(GeneralEmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetByIdAsync(employeeModel.Id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {employeeModel.Id} not found");

        await _employeeRepository.UpdateEmployeeAsync(_mapper.Map<BaseEmployeeEntity>(employeeModel), cancellationToken);
        return employeeModel;
    }

    public async Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        await _employeeRepository.DeleteEmployeeAsync(employeeDb, cancellationToken);
    }
}