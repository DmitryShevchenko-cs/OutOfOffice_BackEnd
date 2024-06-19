using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class AbsenceReasonRepository : IAbsenceReasonRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public AbsenceReasonRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<AbsenceReason> GetAll()
    {
        return _officeDbContext.AbsenceReasons.AsQueryable();
    }

    public async Task<AbsenceReason?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.AbsenceReasons.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<AbsenceReason> CreateAbsenceReason(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.AbsenceReasons.AddAsync(request, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.AbsenceReasons.Remove(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.AbsenceReasons.Update(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}