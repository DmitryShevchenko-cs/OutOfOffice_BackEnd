using OutOfOffice.BLL.Models;
namespace OutOfOffice.BLL.Services.Interfaces;

public interface IApprovalRequestService : IBasicService<ApprovalRequestModel>
{
    Task<List<ApprovalRequestModel>> GetApprovalRequests(int userId, CancellationToken cancellationToken = default);
    
    Task<ApprovalRequestModel> CreateApprovalRequestAsync(int managerId, ApprovalRequestModel approvalRequestModel,
        CancellationToken cancellationToken);

    Task<ApprovalRequestModel> UpdateApprovalRequestAsync(int managerId, ApprovalRequestModel approvalRequestModel,
        CancellationToken cancellationToken = default);

    Task DeleteApprovalRequestAsync(int managerId, int approvalRequestId, CancellationToken cancellationToken = default);
}