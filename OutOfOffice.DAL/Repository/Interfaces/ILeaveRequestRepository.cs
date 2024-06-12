using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface ILeaveRequestRepository : IBasicRepository<LeaveRequest>
{
    Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    Task DeleteLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    Task UpdateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
}