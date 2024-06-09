using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IProjectRepository : IBasicRepository<Project>
{
    public Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    public Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
    public Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
}