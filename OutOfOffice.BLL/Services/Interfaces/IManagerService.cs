using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IManagerService : IBasicService<BaseManagerModel>
{
    Task<BaseManagerModel> CreateProjectManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task DeleteManagerAsync(int adminId, int managerId, CancellationToken cancellationToken = default);
}