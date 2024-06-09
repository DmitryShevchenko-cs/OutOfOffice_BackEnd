using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ProjectService : IProjectService
{
    public Task<ProjectModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}