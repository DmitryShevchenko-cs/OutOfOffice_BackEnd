using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface ILeaveRequestRepository : IBasicRepository<LeaveRequest>
{
    public Task CreateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    public Task DeleteLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    public Task UpdateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
}