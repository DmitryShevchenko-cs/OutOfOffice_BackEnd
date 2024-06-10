using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
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
    private readonly IManagerService _managerService;
    private readonly IEmployeeRepository _employeeRepository;
    
    public ProjectService(IMapper mapper, IProjectRepository projectRepository, IManagerService managerService, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _managerService = managerService;
        _employeeRepository = employeeRepository;
    }

    public async Task<ProjectModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var projectDb = await _projectRepository.GetByIdAsync(id, cancellationToken);
        
        if (projectDb is null)
            throw new ProjectNotFoundException($"Project with Id {id} not found");
        
        var projectModel = _mapper.Map<ProjectModel>(projectDb);
        return projectModel;
    }

    public async Task<ProjectModel> CreateProjectAsync(int projectManagerId, ProjectModel projectModel, CancellationToken cancellationToken = default)
    {
        //get ProjectManager or Admin
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");
        
        switch (managerDb)
        {
            case ProjectManager:
                projectModel.ProjectManagerId = managerDb.Id;
                await _projectRepository.CreateProjectAsync(_mapper.Map<Project>(projectModel), cancellationToken);
                return projectModel;
            case Admin:
                //add admin as a project manager, but we can change it in update method
                projectModel.ProjectManagerId = managerDb.Id;
                await _projectRepository.CreateProjectAsync(_mapper.Map<Project>(projectModel), cancellationToken);
                return projectModel;
            default:
                throw new ManagerException("Invalid manager type");
        }
    }

    public async Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        if (managerDb is ProjectManager or Admin)
        {
            var projectDb = await _projectRepository.GetByIdAsync(projectModel.Id, cancellationToken);
        
            if (projectDb is null)
                throw new ProjectNotFoundException($"Project with Id {projectModel.Id} not found");

            await _projectRepository.UpdateProjectAsync(_mapper.Map<Project>(projectModel), cancellationToken);
            return projectModel;
        }
        
        throw new ManagerException("Invalid manager type");
    }

    public async Task DeleteProjectAsync(int id, int projectManagerId, CancellationToken cancellationToken = default)
    {
        //get ProjectManager or Admin
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {projectManagerId} not found");

        if (managerDb is ProjectManager or Admin)
        {
            var projectDb = await _projectRepository.GetByIdAsync(id, cancellationToken);
        
            if (projectDb is null)
                throw new ProjectNotFoundException($"Project with Id {id} not found");

            await _projectRepository.DeleteProjectAsync(projectDb, cancellationToken);
        }
        else
            throw new ManagerException("Invalid manager type");
    }

    public async Task AddEmployeesInProject(int projectManagerId, int projectId, ICollection<int> employeeModelsIds, CancellationToken cancellationToken = default)
    {
        // get employee who is not a general employee
        var manager = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && !(r is Employee), cancellationToken);
        if (manager is null)
            throw new EmployeeNotFoundException($"Manger or Admin with Id {projectManagerId} not found");

        //get all employees which are GeneralEmployee
        var employees = await _employeeRepository.GetAll()
            .Where(r => employeeModelsIds.Contains(r.Id) && r is Employee).ToListAsync(cancellationToken);

        if (employees.Any())
        {
            var projectDb = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
            if (projectDb is null)
                throw new ProjectNotFoundException($"Project with Id {projectId} not found");
            
            var newEmployeesList = projectDb.Employees.Concat(employees).ToList();
            projectDb.Employees = _mapper.Map<List<Employee>>(newEmployeesList);
        }
        else
        {
            throw new EmployeeNotFoundException($"Employees not found");
        }
    }
    
}