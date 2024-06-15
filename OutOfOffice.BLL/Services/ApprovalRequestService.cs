using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Helpers;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ApprovalRequestService : IApprovalRequestService
{
    private readonly IMapper _mapper;
    private readonly IApprovalRequestRepository _approvalRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ApprovalRequestService(IApprovalRequestRepository approvalRequestRepository, IEmployeeRepository employeeRepository, IMapper mapper)
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

    public async Task<List<ApprovalRequestModel>> GetApprovalRequests(int userId, CancellationToken cancellationToken = default)
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
                .Where(r=> r.ApproverId == userId)
                .ToListAsync(cancellationToken),
            
            ProjectManager => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r=> r.ApproverId == userId)
                .ToListAsync(cancellationToken),
            
            Employee => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r=> r.LeaveRequest.EmployeeId == userId)
                .ToListAsync(cancellationToken),
            
            Admin => await _approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .ToListAsync(cancellationToken),
            
            _ => throw new EmployeeNotFoundException($"Employee with Id {userId} not found")
        };
        

        return _mapper.Map<List<ApprovalRequestModel>>(requests);

    }

    public async Task<ApprovalRequestModel> CreateApprovalRequestAsync(int managerId, ApprovalRequestModel approvalRequestModel,
        CancellationToken cancellationToken)
    {
        var manager = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (manager is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");

        approvalRequestModel.ApproverId = manager.Id;
        var request = await _approvalRequestRepository.ApproveRequestAsync(_mapper.Map<ApprovalRequest>(approvalRequestModel),
            cancellationToken);

        return _mapper.Map<ApprovalRequestModel>(request);
    }

    public async Task<ApprovalRequestModel> UpdateApprovalRequestAsync(int managerId, ApprovalRequestModel approvalRequestModel,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");
        
        var requestDb = await _approvalRequestRepository.GetByIdAsync(approvalRequestModel.Id, cancellationToken);
        if (requestDb is null)
            throw new ProjectNotFoundException($"Request with Id {approvalRequestModel.Id} not found");
        
        if (managerDb is Admin || managerDb is ProjectManager or HrManager && requestDb.ApproverId == managerDb.Id)
        {
            foreach (var propertyMap in ReflectionHelper.WidgetUtil<ApprovalRequestModel, ApprovalRequest>.PropertyMap)
            {
                var userProperty = propertyMap.Item1;
                var userDbProperty = propertyMap.Item2;

                var userSourceValue = userProperty.GetValue(approvalRequestModel);
                var userTargetValue = userDbProperty.GetValue(requestDb);

                if (userSourceValue != null && 
                    userProperty.Name != "ApproverId" &&
                    userProperty.Name != "LeaveRequestId" &&
                    !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
                {
                    userDbProperty.SetValue(requestDb, userSourceValue);
                }
            }
            
            await _approvalRequestRepository.UpdateApprovalAsync(requestDb, cancellationToken);
            return _mapper.Map<ApprovalRequestModel>(requestDb);
        }
        throw new ManagerException("You have no access");
    }

    public async Task DeleteApprovalRequestAsync(int managerId, int approvalRequestId, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");
        
        var requestId = await _approvalRequestRepository.GetByIdAsync(approvalRequestId, cancellationToken);

        if (requestId is null)
            throw new ProjectNotFoundException($"Project with Id {approvalRequestId} not found");

        await _approvalRequestRepository.DeleteApprovalAsync(requestId, cancellationToken);
    }
}