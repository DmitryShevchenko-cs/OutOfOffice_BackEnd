using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IProjectTypeService : IBasicService<ProjectType>
{
    Task<List<ProjectType>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<ProjectType> Create(int managerId, string projectName,
        CancellationToken cancellationToken = default);
    Task Delete(int managerId, int projectId,
        CancellationToken cancellationToken = default);
    Task Update(int managerId, ProjectType projectType,
        CancellationToken cancellationToken = default);
}