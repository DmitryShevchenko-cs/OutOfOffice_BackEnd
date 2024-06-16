using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Helpers;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectService(IMapper mapper, IProjectRepository projectRepository,
        IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ProjectModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var projectDb = await _projectRepository.GetByIdAsync(id, cancellationToken);

        if (projectDb is null)
            throw new ProjectNotFoundException($"Project with Id {id} not found");

        var projectModel = _mapper.Map<ProjectModel>(projectDb);
        return projectModel;
    }

    public async Task<ProjectModel> CreateProjectAsync(int projectManagerId, ProjectModel projectModel,
        CancellationToken cancellationToken = default)
    {
        //get ProjectManager or Admin
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        projectModel.ProjectManagerId = managerDb.Id;

        var project =
            await _projectRepository.CreateProjectAsync(_mapper.Map<Project>(projectModel), cancellationToken);
        return _mapper.Map<ProjectModel>(project);
    }

    public async Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        var updateProjectManager = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectModel.ProjectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (updateProjectManager is null)
            throw new ManagerNotFoundException(
                $"Project manager or admin with Id {projectModel.ProjectManagerId} not found");

        var projectDb = await _projectRepository.GetByIdAsync(projectModel.Id, cancellationToken);
        if (projectDb is null)
            throw new ProjectNotFoundException($"Project with Id {projectModel.Id} not found");

        if (managerDb is Admin || managerDb is ProjectManager && projectDb.ProjectManagerId == managerDb.Id)
        {
            foreach (var propertyMap in ReflectionHelper.WidgetUtil<ProjectModel, Project>.PropertyMap)
            {
                var userProperty = propertyMap.Item1;
                var userDbProperty = propertyMap.Item2;

                var userSourceValue = userProperty.GetValue(projectModel);
                var userTargetValue = userDbProperty.GetValue(projectDb);

                if (userSourceValue != null &&
                    !Equals(userSourceValue, new DateTime()) &&
                    !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
                {
                    userDbProperty.SetValue(projectDb, userSourceValue);
                }
            }

            await _projectRepository.UpdateProjectAsync(projectDb, cancellationToken);
            return _mapper.Map<ProjectModel>(projectDb);
        }

        throw new ManagerException("You have no access");
    }

    public async Task DeleteProjectAsync(int projectId, int projectManagerId,
        CancellationToken cancellationToken = default)
    {
        //get ProjectManager or Admin
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is not (ProjectManager or Admin))
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        var projectDb = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (projectDb is null)
            throw new ProjectNotFoundException($"Project with Id {projectId} not found");

        await _projectRepository.DeleteProjectAsync(projectDb, cancellationToken);
    }

    public async Task DeactivateProjectAsync(int projectId, int projectManagerId,
        CancellationToken cancellationToken = default)
    {
        //get ProjectManager or Admin
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is not (ProjectManager or Admin))
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        var projectDb = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (projectDb is null)
            throw new ProjectNotFoundException($"Project with Id {projectId} not found");
        projectDb.isDeactivated = !projectDb.isDeactivated;

        await _projectRepository.UpdateProjectAsync(projectDb, cancellationToken);
    }

    public async Task<List<ProjectModel>> GetAllAsync(int userId, CancellationToken cancellationToken = default)
    {
        //only for ProjectManager or Admin
        var userDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == userId, cancellationToken);
        if (userDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {userId} not found");

        var projectsDb = userDb switch
        {
            HrManager => await _projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.Employees.Any(i => i.HrMangerId == userId))
                .ToListAsync(cancellationToken),

            ProjectManager => await _projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.ProjectManagerId == userId)
                .ToListAsync(cancellationToken),

            Employee => await _projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.Employees.Any(i => i.Id == userId))
                .ToListAsync(cancellationToken),

            Admin => await _projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .ToListAsync(cancellationToken),

            _ => throw new EmployeeNotFoundException($"Employee with Id {userId} not found")
        };

        return _mapper.Map<List<ProjectModel>>(projectsDb);
    }

    public async Task AddEmployeesInProjectAsync(int projectManagerId, int projectId,
        ICollection<int> employeeModelsIds, CancellationToken cancellationToken = default)
    {
        // get employee who is not a general employee
        var manager = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (manager is null)
            throw new EmployeeNotFoundException($"Manger or Admin with Id {projectManagerId} not found");

        //get all employees which are GeneralEmployee
        var employees = await _employeeRepository.GetAll().Include(r => ((Employee)r).Projects)
            .Where(r => employeeModelsIds.Contains(r.Id) && r is Employee).ToListAsync(cancellationToken);

        if (employees.Any())
        {
            var projectDb = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
            if (projectDb is null)
                throw new ProjectNotFoundException($"Project with Id {projectId} not found");

            foreach (var emp in employees.Cast<Employee>().Where(emp => !emp.Projects.Contains(projectDb)))
            {
                emp.Projects.Add(projectDb);
            }

            await _employeeRepository.UpdateEmployeeAsync(employees, cancellationToken);
        }
        else
        {
            throw new EmployeeNotFoundException($"Employees not found");
        }
    }
}