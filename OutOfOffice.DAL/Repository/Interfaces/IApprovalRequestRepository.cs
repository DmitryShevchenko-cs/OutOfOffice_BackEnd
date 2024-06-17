using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IApprovalRequestRepository : IBasicRepository<ApprovalRequest>
{
    Task<ApprovalRequest> CreateApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task DeleteApproveAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task UpdateApproveAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
}