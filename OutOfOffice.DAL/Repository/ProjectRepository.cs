using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
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
        return _officeDbContext.Projects.Include(i => i.Employees).Include(i => i.ProjectType).AsQueryable();
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Projects
            .Include(i => i.Employees)
            .Include(i => i.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.Projects.AddAsync(project, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
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
    public async Task AddEmployeesInProjectAsync(int projId, List<Employee> employees, CancellationToken cancellationToken = default)
    {
        var pr = await _officeDbContext.Projects.Where(r => r.Id == 1).SingleOrDefaultAsync(cancellationToken);
        pr!.Employees = employees;
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}