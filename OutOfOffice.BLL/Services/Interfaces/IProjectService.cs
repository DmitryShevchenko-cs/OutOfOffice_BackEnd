using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IProjectService : IBasicService<ProjectModel>
{
    Task<ProjectModel> CreateProjectAsync(int projectManagerId, ProjectModel projectModel, CancellationToken cancellationToken = default);
    Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(int projectId, int projectManagerId, CancellationToken cancellationToken = default);

    Task AddEmployeesInProjectAsync(int projectManagerId, int projectId, ICollection<int> employeeModelsIds,
        CancellationToken cancellationToken = default);

    Task DeactivateProjectAsync(int projectId, int projectManagerId, CancellationToken cancellationToken = default);

    Task<List<ProjectModel>> GetAllByProjectManagerIdAsync(int managerId,
        CancellationToken cancellationToken = default);

    Task<List<ProjectModel>> GetAllByHrManagerIdAsync(int managerId, CancellationToken cancellationToken = default);
    Task<List<ProjectModel>> GetAllAsync(int adminId, CancellationToken cancellationToken = default);

}