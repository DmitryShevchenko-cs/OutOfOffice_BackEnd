using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;

namespace OutOfOffice.BLL.Services;

public class LeaveRequestService : ILeaveRequestService
{
    public Task<LeaveRequestModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}