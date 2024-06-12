using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class LeaveRequestRepository : ILeaveRequestRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public LeaveRequestRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }

    public IQueryable<LeaveRequest> GetAll()
    {
        return _officeDbContext.LeaveRequests
            .Include(r => r.AbsenceReason)
            .Include(r => r.ApprovalRequest)
            .AsQueryable();
    }

    public async Task<LeaveRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.LeaveRequests.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        var entityEntry =  await _officeDbContext.LeaveRequests.AddAsync(request, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.LeaveRequests.Remove(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.LeaveRequests.Update(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}