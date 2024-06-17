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
public class ApprovalRequestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IApprovalRequestService _approvalRequestService;

    public ApprovalRequestController(IMapper mapper, IApprovalRequestService approvalRequestService)
    {
        _mapper = mapper;
        _approvalRequestService = approvalRequestService;
    }
    
    [HttpPut("approve")]
    public async Task<IActionResult> ApproveRequest([FromBody] ApprovalRequestUpdateModel approve, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _approvalRequestService.ApproveLeaveRequestAsync(userId, approve.Id, approve.Comment,
            cancellationToken);
        return Ok(_mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    }
    
    [HttpPut("decline")]
    public async Task<IActionResult> DeclineRequest([FromBody] ApprovalRequestUpdateModel approve, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _approvalRequestService.DeclineLeaveRequestAsync(userId, approve.Id, approve.Comment,
            cancellationToken);
        return Ok(_mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetApprovalRequest(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _approvalRequestService.GetApprovalRequestsAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<ApprovalRequestViewModel>>(approvedRequest));
    }
}