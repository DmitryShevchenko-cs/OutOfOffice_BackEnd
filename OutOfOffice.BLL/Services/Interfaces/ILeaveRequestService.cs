using OutOfOffice.BLL.Models;
namespace OutOfOffice.BLL.Services.Interfaces;

public interface ILeaveRequestService : IBasicService<LeaveRequestModel>
{
    Task<LeaveRequestModel> CreateLeaveRequestAsync(int employeeId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken);

    Task<LeaveRequestModel> UpdateLeaveRequestAsync(int employeeId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken = default);

    Task DeleteLeaveRequestAsync(int employeeId, int leaveRequestId, CancellationToken cancellationToken = default);

    Task<List<LeaveRequestModel>> GetAllEmployeesRequestAsync(int employeeId, CancellationToken cancellationToken = default);

    Task<LeaveRequestModel> GetById(int employeeId, int requestId, CancellationToken cancellationToken = default);
}