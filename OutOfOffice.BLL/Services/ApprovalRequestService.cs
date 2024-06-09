using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ApprovalRequestService : IApprovalRequestService
{
    public Task<ApprovalRequestModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}