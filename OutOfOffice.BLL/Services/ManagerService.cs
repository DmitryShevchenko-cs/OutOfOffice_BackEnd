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

public class ManagerService : IManagerService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public ManagerService(IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<BaseManagerModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (managerDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");
        
        var managerModel = _mapper.Map<BaseManagerModel>(managerDb);
        return managerModel;
    }

    public async Task<BaseManagerModel> CreateProjectManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {

        var adminDb = await _employeeRepository.GetByIdAsync(adminId, cancellationToken);
        if (adminDb is not Admin)
            throw new ManagerException("Invalid manager type");
        
        var managerDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == managerModel.Login, cancellationToken);
        if (managerDb is not null)
        {
            if (managerDb.Login == managerModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }

        BaseManagerEntity manager;
        switch (managerModel)
        {
            case ProjectManagerModel:
            {
                manager = _mapper.Map<ProjectManager>(managerModel);
                break;
            }
            case HrManagerModel:
            {
                manager = _mapper.Map<HrManager>(managerModel);
                break;
            }
            default:
                throw new Exception();
        }
        manager.Password = PasswordHelper.HashPassword(manager.Password);
        var addedManager = await _employeeRepository.AddEmployeeAsync(manager, cancellationToken);
        return _mapper.Map<BaseManagerModel>(addedManager);
    }


    /// <param name="managerId">person who want to update (admin or himself manager)</param>
    public async Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {
        var updater = await _employeeRepository.GetAll().Where(r => r.Id == managerId && (r is BaseManagerEntity || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (updater is null)
            throw new EmployeeNotFoundException($"Employee with Id {managerModel.Id} not found");

        if (updater is Admin || (updater is BaseManagerEntity && updater.Id == managerModel.Id))
        {
            var managerDb = await _employeeRepository.GetByIdAsync(managerModel.Id, cancellationToken);
            
            foreach (var propertyMap in ReflectionHelper.WidgetUtil<BaseManagerModel, BaseManagerEntity>.PropertyMap)
            {
                var userProperty = propertyMap.Item1;
                var userDbProperty = propertyMap.Item2;

                var userSourceValue = userProperty.GetValue(managerModel);
                var userTargetValue = userDbProperty.GetValue(managerDb);

                if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
                {
                    userDbProperty.SetValue(managerDb, userSourceValue);
                }
            }
                
            managerDb!.Password = string.IsNullOrEmpty(managerModel.Password)
                ? managerDb.Password
                : PasswordHelper.HashPassword(managerModel.Password);
                
            var updatedManager = await _employeeRepository.UpdateEmployeeAsync(managerDb, cancellationToken);
            return _mapper.Map<BaseManagerModel>(updatedManager);
        }
        throw new ManagerException("Invalid manager type");
    }

    public async Task DeleteManagerAsync(int adminId, int managerId, CancellationToken cancellationToken = default)
    {
        var adminDb = await _employeeRepository.GetByIdAsync(adminId, cancellationToken);
        if (adminDb is not Admin)
            throw new ManagerException("Invalid manager type");
        
        var managerDb = await _employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (managerDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {managerId} not found");

        await _employeeRepository.DeleteEmployeeAsync(managerDb, cancellationToken);
    }

    public async Task<List<BaseManagerEntity>> GetAll(int adminId, CancellationToken cancellationToken = default)
    {
        var adminDb = await _employeeRepository.GetByIdAsync(adminId, cancellationToken);
        if (adminDb is not Admin)
            throw new ManagerException("Invalid manager type");
        
        var managersDb = await _employeeRepository.GetAllManagers().ToListAsync(cancellationToken);
        return _mapper.Map<List<BaseManagerEntity>>(managersDb);
    }

    public async Task<List<BaseManagerEntity>> GetApproversAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => (r is Employee) && r.Id == userId, cancellationToken);
        if (userDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {userId} not found");


        var hrManagersTask = await _employeeRepository.GetAllManagers()
            .OfType<HrManager>()
            .Where(r => r.Partners.Any(i => i.Id == userId))
            .ToListAsync(cancellationToken);

        var projectManagersTask = await _employeeRepository.GetAllManagers()
            .OfType<ProjectManager>()
            .Where(r => r.Projects.Any(j => j.Employees.Any(d => d.Id == userId)))
            .ToListAsync(cancellationToken);
        
        var approvers = hrManagersTask.Concat<BaseManagerEntity>(projectManagersTask).ToList();

        return approvers;

    }
}