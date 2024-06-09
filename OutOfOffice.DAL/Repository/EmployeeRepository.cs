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

    public IQueryable<Employee> GetAll()
    {
        return _officeDbContext.Employees.AsQueryable();
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Employees.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _officeDbContext.Employees.AddAsync(employee, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Employees.Remove(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Employees.Update(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}