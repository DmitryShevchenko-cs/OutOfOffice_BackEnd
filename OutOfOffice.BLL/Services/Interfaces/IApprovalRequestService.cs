using OutOfOffice.BLL.Models;
using OutOfOffice.DAL.Entity.Enums;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IApprovalRequestService : IBasicService<ApprovalRequestModel>
{
    Task<List<ApprovalRequestModel>> GetApprovalRequests(int userId, CancellationToken cancellationToken = default);
    
    Task<ApprovalRequestModel> ApproveLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default);
    
    Task<ApprovalRequestModel> DeclineLeaveRequestAsync(int managerId,  int requestId, string comment,
        CancellationToken cancellationToken = default);
    
}