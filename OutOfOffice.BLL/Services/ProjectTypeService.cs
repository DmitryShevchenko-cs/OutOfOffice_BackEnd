using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ProjectTypeService : IProjectTypeService
{
    private readonly IProjectTypeRepository _projectTypeRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectTypeService(IProjectTypeRepository projectTypeRepository, IEmployeeRepository employeeRepository)
    {
        _projectTypeRepository = projectTypeRepository;
        _employeeRepository = employeeRepository;
    }


    public async Task<ProjectType> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var projectType = await _projectTypeRepository.GetByIdAsync(id, cancellationToken);
        if (projectType is null)
            throw new ProjectTypeException($"Absence reason with id {id} not found");

        return projectType;
    }

    public async Task<List<ProjectType>> GetAllAsync(CancellationToken cancellationToken)
    {
        var projectType = await _projectTypeRepository.GetAll().ToListAsync(cancellationToken);
        return projectType;
    }

    public async Task<ProjectType> Create(int managerId, string projectName, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is HrManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");

        var projectTypeDb = await _projectTypeRepository.GetAll().Where(r => r.Name == projectName)
            .SingleOrDefaultAsync(cancellationToken);
        if (projectTypeDb != null)
            throw new ProjectTypeException($"ProjectType with name {projectName} created already");

        var projectType = await _projectTypeRepository.CreateProjectTypeAsync(new ProjectType()
        {
            Name = projectName,
        }, cancellationToken);

        return projectType;
    }

    public async Task Delete(int managerId, int projectId, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is HrManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");

        var projectType = await _projectTypeRepository.GetByIdAsync(projectId, cancellationToken);
        if (projectType is null)
            throw new ProjectTypeException($"ProjectType with id {projectId} not found");

        await _projectTypeRepository.DeleteProjectTypeAsync(projectType, cancellationToken);
    }

    public async Task Update(int managerId, ProjectType projectType, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is HrManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");

        var projectTypeCheck = await _projectTypeRepository.GetAll().Where(r => r.Name == projectType.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (projectTypeCheck != null)
            throw new ProjectTypeException($"ProjectType with name {projectType.Name} created already");
        
        var projectTypeDb = await _projectTypeRepository.GetByIdAsync(projectType.Id, cancellationToken);
        if (projectTypeDb is null)
            throw new ProjectTypeException($"ProjectType with id {projectType.Id} not found");

        projectTypeDb.Name = projectType.Name;

        await _projectTypeRepository.UpdateProjectTypeAsync(projectTypeDb, cancellationToken);

    }
}