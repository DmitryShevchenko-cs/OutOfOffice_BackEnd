using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IProjectTypeRepository : IBasicRepository<ProjectType>
{
    Task<ProjectType> CreateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
    Task DeleteProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
    Task UpdateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
}