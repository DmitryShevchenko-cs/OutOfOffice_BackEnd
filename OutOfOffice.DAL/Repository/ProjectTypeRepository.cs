using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class ProjectTypeRepository : IProjectTypeRepository
{
    
    private readonly OfficeDbContext _officeDbContext;

    public ProjectTypeRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<ProjectType> GetAll()
    {
        return _officeDbContext.ProjectTypes.AsQueryable();
    }

    public async Task<ProjectType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.ProjectTypes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<ProjectType> CreateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.ProjectTypes.AddAsync(projectType, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        _officeDbContext.ProjectTypes.Remove(projectType);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        _officeDbContext.ProjectTypes.Update(projectType);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}