using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IProjectRepository : IBasicRepository<Project>
{
    Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
}