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

    public async Task<EmployeeModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");
        
        var employeeModel = _mapper.Map<EmployeeModel>(employeeDb);
        return employeeModel;
    }

    public async Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var creator = await _employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        var employeeDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == employeeModel.Login, cancellationToken);
        if (employeeDb is not null)
        {
            if (employeeDb.Login == employeeModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }
        
        employeeModel.Password = PasswordHelper.HashPassword(employeeModel.Password);
        if (creator is HrManager)
            employeeModel.HrMangerId = creator.Id;
        employeeDb = await _employeeRepository.AddEmployeeAsync(_mapper.Map<Employee>(employeeModel), cancellationToken);
        return _mapper.Map<EmployeeModel>(await _employeeRepository.GetByIdAsync(employeeDb.Id, cancellationToken));
    }

    public async Task<EmployeeModel> UpdateEmployeeAsync(EmployeeModel employeeModel, CancellationToken cancellationToken = default)
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

    public async Task<List<EmployeeModel>> GetEmployeeAsync(int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await _employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        var employees = await _employeeRepository.GetAll().Include(r => ((Employee)r).Projects).Where(r => r is Employee).ToListAsync(cancellationToken);
        var employeesModels = _mapper.Map<List<BaseEmployeeModel>>(employees);
        return _mapper.Map<List<EmployeeModel>>(employeesModels);
    }
}