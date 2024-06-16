using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Enums;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ApprovalRequestService : IApprovalRequestService
{
    private readonly IMapper _mapper;
    private readonly IApprovalRequestRepository _approvalRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ApprovalRequestService(IApprovalRequestRepository approvalRequestRepository,
        IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _approvalRequestRepository = approvalRequestRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ApprovalRequestModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var requestDb = await _approvalRequestRepository.GetByIdAsync(id, cancellationToken);

        if (requestDb is null)
            throw new ApprovalRequestNotFoundException($"Approval request with Id {id} not found");

        var approvalRequestModel = _mapper.Map<ApprovalRequestModel>(requestDb);
        return approvalRequestModel;
    }

    public async Task<List<ApprovalRequestModel>> GetApprovalRequests(int userId,
        CancellationToken cancellationToken = default)
    {
        var userDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == userId, cancellationToken);
        if (userDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {userId} not found");

        var requests = userDb switch
        {
            HrManager => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.ApproverId == userId)
                .ToListAsync(cancellationToken),

            ProjectManager => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.ApproverId == userId)
                .ToListAsync(cancellationToken),

            Employee => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.LeaveRequest.EmployeeId == userId)
                .ToListAsync(cancellationToken),

            Admin => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .ToListAsync(cancellationToken),

            _ => throw new EmployeeNotFoundException($"Employee with Id {userId} not found")
        };


        return _mapper.Map<List<ApprovalRequestModel>>(requests);

    }

    public async Task<ApprovalRequestModel> ApproveLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");
        
        var requestDb = await _approvalRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (requestDb is null)
            throw new ProjectNotFoundException($"Request with Id {requestId} not found");

        if (managerDb is Admin)
            requestDb.ApproverId = managerDb.Id;
        
        if(requestDb.ApproverId != managerId)
            throw new ProjectNotFoundException($"manger with Id {requestId} is not approver");
            
        var employee = requestDb.LeaveRequest.Employee;
        var daysOff = (requestDb.LeaveRequest.EndDate - requestDb.LeaveRequest.StartDate).Days;
        
        employee.OutOfOfficeBalance = employee.OutOfOfficeBalance >= daysOff
            ? employee.OutOfOfficeBalance - daysOff
            : throw new OutOfBalanceLimitException("Employee doesn't have enough days on balance");

        requestDb.ApprovalRequestStatus = ApprovalRequestStatus.Approved;
        requestDb.Comment = comment;

        await _employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
        await _approvalRequestRepository.UpdateApprovalAsync(requestDb, cancellationToken);

        return _mapper.Map<ApprovalRequestModel>(requestDb);
    }

    public async Task<ApprovalRequestModel> DeclineLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");
        
        var requestDb = await _approvalRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (requestDb is null)
            throw new ProjectNotFoundException($"Request with Id {requestId} not found");

        if (managerDb is Admin)
            requestDb.ApproverId = managerDb.Id;
        
        if(requestDb.ApproverId != managerId)
            throw new ProjectNotFoundException($"manger with Id {requestId} is not approver");
        
        requestDb.ApprovalRequestStatus = ApprovalRequestStatus.Decline;
        requestDb.Comment = comment;
        await _approvalRequestRepository.UpdateApprovalAsync(requestDb, cancellationToken);
        return _mapper.Map<ApprovalRequestModel>(requestDb);
    }
}