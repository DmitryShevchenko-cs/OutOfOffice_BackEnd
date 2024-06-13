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

    // [HttpPost]
    // public async Task<IActionResult> CreateApprovalRequest([FromBody] ApprovalRequestCreateModel approve, CancellationToken cancellationToken = default)
    // {
    //     var userId = User.GetUserId();
    //     var approvedRequest = await _approvalRequestService.CreateApprovalRequestAsync(userId, _mapper.Map<ApprovalRequestModel>(approve),
    //         cancellationToken);
    //     return Ok(_mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    // }
    
    [HttpPut]
    public async Task<IActionResult> UpdateApprovalRequest([FromBody] ApprovalRequestUpdateModel approve, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _approvalRequestService.UpdateApprovalRequestAsync(userId, _mapper.Map<ApprovalRequestModel>(approve),
            cancellationToken);
        return Ok(_mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetApprovalRequest(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _approvalRequestService.GetApprovalRequests(userId, cancellationToken);
        return Ok(_mapper.Map<List<ApprovalRequestViewModel>>(approvedRequest));
    }
    
}