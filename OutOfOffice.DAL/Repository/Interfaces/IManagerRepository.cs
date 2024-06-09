using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IManagerRepository : IBasicRepository<BaseManagerEntity>
{
    public Task AddManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default);
    public Task DeleteManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default);
    public Task UpdateManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default);
}