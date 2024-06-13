using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IApprovalRequestRepository : IBasicRepository<ApprovalRequest>
{
    Task<ApprovalRequest> ApproveRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task DeleteApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task UpdateApprovalAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
}