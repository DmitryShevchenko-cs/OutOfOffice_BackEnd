using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HrManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly IMapper _mapper;
    
    
    public HrManagerController(IManagerService managerService, IMapper mapper)
    {
        _managerService = managerService;
        _mapper = mapper;
    }

    //admin has to authorize
    [HttpPost]
    public async Task<IActionResult> CreateHrManager([FromBody] ManagerCreateModel manager, CancellationToken cancellationToken)
    {
        var adminId = User.GetUserId();
        var managerResult = await _managerService.CreateProjectManagerAsync(adminId,_mapper.Map<HrManagerModel>(manager), cancellationToken);
        return Ok(_mapper.Map<ManagerViewModel>(managerResult));
    }
}