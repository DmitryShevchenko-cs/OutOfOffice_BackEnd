using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IApprovalRequestRepository : IBasicRepository<ApprovalRequest>
{
    public Task ApproveRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    public Task DeleteApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    public Task UpdateApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
}