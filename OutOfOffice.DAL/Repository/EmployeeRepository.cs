using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class EmployeeRepository : IEmployeeRepository
{

    private readonly OfficeDbContext _officeDbContext;

    public EmployeeRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<BaseEmployeeEntity> GetAll()
    {
        return _officeDbContext.BaseEmployees
            .Include(r => ((Employee)r).Subdivision)
            .Include(r => ((Employee)r).Position)
            .AsQueryable();
    }
    public IQueryable<BaseManagerEntity> GetAllManagers()
    {
        return _officeDbContext.Managers.Include(r => r.ApprovalRequests).AsQueryable();
    }
    
    public IQueryable<Employee> GetAllEmployees()
    {
        return _officeDbContext.Employees
            .Include(r => r.Subdivision)
            .Include(r => r.Position)
            .Include(r => r.HrManager)
            .AsQueryable();
    }
    
    public IQueryable<HrManager> GetAllHrManagers()
    {
        return _officeDbContext.HrManagers
            .Include(r => r.Partners)
            .AsQueryable();
    }
    
    public IQueryable<ProjectManager> GetAllProjectManagers()
    {
        return _officeDbContext.ProjectManagers
            .Include(r => r.Projects)
            .AsQueryable();
    }

    public async Task<BaseEmployeeEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.BaseEmployees
            .Include(r => ((Employee)r).Subdivision)
            .Include(r => ((Employee)r).Position)
            .Include(r => ((Employee)r).Projects)
            .Include(r => ((Employee)r).LeaveRequests)
            .Include(r => ((Employee)r).HrManager)
            .Include(r => ((HrManager)r).Partners)
            .Include(r => ((ProjectManager)r).Projects).ThenInclude(r => r.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<BaseEmployeeEntity> AddEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.BaseEmployees.AddAsync(employee, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default)
    {
        _officeDbContext.BaseEmployees.Remove(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<BaseEmployeeEntity> UpdateEmployeeAsync(BaseEmployeeEntity employee, CancellationToken cancellationToken = default)
    {
        var entityEntry = _officeDbContext.BaseEmployees.Update(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }
    public async Task UpdateEmployeeAsync(List<BaseEmployeeEntity> employee, CancellationToken cancellationToken = default)
    {
        _officeDbContext.BaseEmployees.UpdateRange(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}