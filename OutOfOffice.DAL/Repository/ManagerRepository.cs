using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class ManagerRepository : IManagerRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public ManagerRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<BaseManagerEntity> GetAll()
    {
        return _officeDbContext.Managers.AsQueryable();
    }

    public async Task<BaseManagerEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Managers.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task AddManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default)
    {
        await _officeDbContext.Managers.AddAsync(manager, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Managers.Remove(manager);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateManagerAsync(BaseManagerEntity manager, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Managers.Update(manager);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}