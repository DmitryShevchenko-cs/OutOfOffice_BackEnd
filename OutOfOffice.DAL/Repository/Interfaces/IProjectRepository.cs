using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IProjectRepository : IBasicRepository<Project>
{
    Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);

    Task AddEmployeesInProjectAsync(int projId, List<Employee> employees,
        CancellationToken cancellationToken = default);
}