using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IProjectTypeService : IBasicService<ProjectType>
{
    Task<List<ProjectType>> GetAllAsync(CancellationToken cancellationToken);
}