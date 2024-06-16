using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(IMapper mapper, ILeaveRequestService leaveRequestService)
    {
        _mapper = mapper;
        _leaveRequestService = leaveRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateModel requestModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await _leaveRequestService.CreateLeaveRequestAsync(userId, requestModel.ApproverId, _mapper.Map<LeaveRequestModel>(requestModel) , cancellationToken);
        return Ok(_mapper.Map<LeaveRequestViewModel>(request));
    }
    
    [HttpDelete("{requestId:int}")]
    public async Task<IActionResult> DeleteLeaveRequest(int requestId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _leaveRequestService.DeleteLeaveRequestAsync(userId, requestId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateLeaveRequest([FromBody] LeaveRequestUpdateModel requestModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _leaveRequestService.UpdateLeaveRequestAsync(userId, _mapper.Map<LeaveRequestModel>(requestModel), cancellationToken);
        return Ok();
    }
    
    [HttpGet("{requestId:int}")]
    public async Task<IActionResult> GetLeaveRequest(int requestId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await _leaveRequestService.GetByRequestIdAsync(userId, requestId, cancellationToken);
        return Ok(_mapper.Map<LeaveRequestFullViewModel>(request));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllLeaveRequest(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await _leaveRequestService.GetAllAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<LeaveRequestFullViewModel>>(request));
    }
    
}