using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.DAL.Repository;

public class ApprovalRequestRepository : IApprovalRequestRepository
{
    private readonly OfficeDbContext _officeDbContext;

    public ApprovalRequestRepository(OfficeDbContext officeDbContext)
    {
        _officeDbContext = officeDbContext;
    }


    public IQueryable<ApprovalRequest> GetAll()
    {
        return _officeDbContext.ApprovalRequests.AsQueryable();
    }

    public async Task<ApprovalRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.ApprovalRequests.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task ApproveRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        await _officeDbContext.ApprovalRequests.AddAsync(request, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.ApprovalRequests.Remove(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        _officeDbContext.ApprovalRequests.Update(request);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}