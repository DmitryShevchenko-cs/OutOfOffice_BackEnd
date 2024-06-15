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

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
        IEmployeeRepository employeeRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<LeaveRequestModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id, cancellationToken);

        if (leaveRequest is null)
            throw new LeaveRequestNotFoundException($"Leave request with Id {id} not found");

        var requestModel = _mapper.Map<LeaveRequestModel>(leaveRequest);
        return requestModel;
    }

    public async Task<LeaveRequestModel> CreateLeaveRequestAsync(int employeeId, int approverId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken)
    {
        var employeeDb = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId && r is Employee, cancellationToken);
        if (employeeDb is not Employee)
        {
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        }

        leaveRequestModel.EmployeeId = employeeId;
        leaveRequestModel.ApprovalRequest = new ApprovalRequestModel
        {
            ApproverId = approverId,
            Comment = ""
        };
        var requestDb = await _leaveRequestRepository.CreateLeaveRequestAsync(_mapper.Map<LeaveRequest>(leaveRequestModel),
                cancellationToken);
        return _mapper.Map<LeaveRequestModel>(requestDb);
    }

    public async Task<LeaveRequestModel> UpdateLeaveRequestAsync(int employeeId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId && r is Employee, cancellationToken);
        if (employeeDb is not Employee)
        {
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        }

        var leaveRequestDb = await _leaveRequestRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == leaveRequestModel.Id && r.EmployeeId == employeeId, cancellationToken);
        if (leaveRequestDb is null)
            throw new LeaveRequestNotFoundException($"Leave request with Id {leaveRequestModel.Id} not found");

        foreach (var propertyMap in ReflectionHelper.WidgetUtil<LeaveRequestModel, LeaveRequest>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;

            var userSourceValue = userProperty.GetValue(leaveRequestModel);
            var userTargetValue = userDbProperty.GetValue(leaveRequestDb);

            if (userProperty.Name != "EmployeeId" && 
                !Equals(userSourceValue, new DateTime()) &&
                userSourceValue != null && 
                !ReferenceEquals(userSourceValue, "") &&
                !userSourceValue.Equals(userTargetValue))
            {
                userDbProperty.SetValue(leaveRequestDb, userSourceValue);
            }
        }

        await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequestDb, cancellationToken);
        return _mapper.Map<LeaveRequestModel>(leaveRequestDb);
    }

    public async Task DeleteLeaveRequestAsync(int employeeId, int leaveRequestId, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId && r is Employee, cancellationToken);
        if (employeeDb is not Employee)
        {
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        }
        
        var leaveRequestDb = await _leaveRequestRepository.GetAll().Include(r => r.Employee)
            .SingleOrDefaultAsync(r => r.EmployeeId == employeeId && r.Id == leaveRequestId, cancellationToken);
        if (leaveRequestDb is null)
            throw new LeaveRequestNotFoundException($"Leave request with Id {leaveRequestId} not found");
        
        await _leaveRequestRepository.DeleteLeaveRequestAsync(leaveRequestDb, cancellationToken);
    }

    public async Task<List<LeaveRequestModel>> GetAllEmployeesRequestAsync(int userId, CancellationToken cancellationToken = default)
    {
        var leaveRequestsDb = await _leaveRequestRepository.GetAll().Include(r => r.Employee).Include(r => r.ApprovalRequest).ThenInclude(i => i!.Approver)
            .Where(r => r.EmployeeId == userId).ToListAsync(cancellationToken);
        
        return _mapper.Map<List<LeaveRequestModel>>(leaveRequestsDb);
    }

    /// <summary>
    /// user can get his leaveRequest by id
    /// </summary>
    public async Task<LeaveRequestModel> GetByRequestIdAsync(int employeeId, int requestId, CancellationToken cancellationToken = default)
    {
        var leaveRequestsDb = await _leaveRequestRepository.GetAll().Include(r => r.Employee)
            .SingleOrDefaultAsync(r => r.EmployeeId == employeeId && r.Id == requestId, cancellationToken);
        
        return _mapper.Map<LeaveRequestModel>(leaveRequestsDb);
    }

    /// <summary>
    /// manager or admin can get all leaveRequest
    /// </summary>
    public async Task<List<LeaveRequestModel>> GetAllAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        var userDb = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId, cancellationToken);
        if (userDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        
        
        var leaveRequestsDb = userDb switch
        {
            HrManager => await _leaveRequestRepository.GetAll()
                .Where(r => r.Employee.HrMangerId == employeeId)
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            ProjectManager => await _leaveRequestRepository.GetAll()
                .Where(r => r.Employee.Projects.Any(i => i.ProjectManagerId == employeeId))
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            Employee => await _leaveRequestRepository.GetAll()
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            Admin => await _leaveRequestRepository.GetAll()
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            _ => throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found")
        };
        
        
        return _mapper.Map<List<LeaveRequestModel>>(leaveRequestsDb);
    }
}