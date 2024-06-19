using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IManagerService
{
    Task<BaseEmployeeEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BaseManagerModel> CreateProjectManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task DeleteManagerAsync(int userId, int managerId, CancellationToken cancellationToken = default);
    Task<List<BaseManagerEntity>> GetAll(int adminId, CancellationToken cancellationToken = default);
    Task<List<HrManagerModel>> GetHrManagers(int adminId, CancellationToken cancellationToken = default);
    Task<List<ProjectManagerModel>> GetProjectManagers(int adminId, CancellationToken cancellationToken = default);
    
    Task<List<BaseManagerEntity>> GetApproversAsync(int userId, CancellationToken cancellationToken = default);
}