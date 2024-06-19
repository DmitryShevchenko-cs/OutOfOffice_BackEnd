using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class SubdivisionRepository : ISubdivisionRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public SubdivisionRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }
    
    public IQueryable<Subdivision> GetAll()
    {
        return _officeDbContext.Subdivisions.AsQueryable();
    }

    public async Task<Subdivision?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Subdivisions.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.Subdivisions.AddAsync(subdivision, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Subdivisions.Remove(subdivision);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Subdivisions.Update(subdivision);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}