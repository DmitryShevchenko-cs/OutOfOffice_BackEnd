using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public ProjectRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<Project> GetAll()
    {
        return _officeDbContext.Projects.AsQueryable();
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Projects
            .Include(i => i.Employees)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _officeDbContext.Projects.AddAsync(project, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Projects.Remove(project);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Projects.Update(project);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}